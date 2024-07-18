using ClosedXML.Excel;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using VoucherPrintingApp.Helper;

namespace VoucherPrintingApp
{
    public partial class Voucher : Form
    {
        string filePath = @"C:\easypack\text.xlsx";
        public Voucher()
        {
            InitializeComponent();
            LoadDataIntoDataGridView(filePath);
            InitializeDataGridView();
            // Set in form initialization or designer
            dgv_Voucher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Voucher.MultiSelect = true;  // Allows multiple rows to be selected
        }

        Dictionary<string, List<DataRow>> transactionDetails = new Dictionary<string, List<DataRow>>();
        private void LoadDataIntoDataGridView(string filePath)
        {
            DataTable dt = new DataTable();
            HashSet<string> uniqueNums = new HashSet<string>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Num", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Memo/Description", typeof(string));
                dt.Columns.Add("Account", typeof(string));
                dt.Columns.Add("Debit", typeof(string));
                dt.Columns.Add("Credit", typeof(string));
                dt.Columns.Add("CreditWords", typeof(decimal));

                foreach (IXLRow row in worksheet.RowsUsed().Skip(4))
                {
                    string num = row.Cell(5).GetValue<string>().Trim();
                    DataRow dataRow = dt.NewRow();
                    dataRow["Date"] = row.Cell(4).GetValue<string>();
                    dataRow["Num"] = num;
                    dataRow["Name"] = row.Cell(6).GetValue<string>();
                    dataRow["Memo/Description"] = row.Cell(7).GetValue<string>();
                    dataRow["Account"] = row.Cell(8).GetValue<string>();
                    dataRow["Debit"] = row.Cell(9).GetValue<string>();

                    string creditStr = row.Cell(10).GetValue<string>();
                    decimal creditValue;

                    if (creditStr == "--" || !decimal.TryParse(creditStr, out creditValue))
                    {
                        creditValue = 0; // Default to zero for invalid values
                        creditStr = "0";
                    }

                    dataRow["Credit"] = creditValue;
                    //dataRow["CreditWords"] = AmountInWords.NumberToWords(creditValue);

                    if (!transactionDetails.ContainsKey(num))
                    {
                        transactionDetails[num] = new List<DataRow>();
                    }
                    transactionDetails[num].Add(dataRow);

                    if (!uniqueNums.Contains(num))
                    {
                        uniqueNums.Add(num);
                        dt.Rows.Add(dataRow.ItemArray);
                    }
                }
            }
            dgv_Voucher.DataSource = dt;
            dgv_Voucher.Columns["Debit"].Visible = false;
            dgv_Voucher.Columns["CreditWords"].Visible = false;
        }
        private void InitializeDataGridView()
        {
            // Assuming dgv_Voucher is your DataGridView
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Selected";
            checkBoxColumn.Width = 70;
            checkBoxColumn.Name = "checkBoxColumn";
            dgv_Voucher.Columns.Insert(0, checkBoxColumn); // Inserts at the first position
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (dgv_Voucher.SelectedRows.Count > 0)
            {
                DataTable selectedTransactions = GetSelectedTransactionsData();
                if (selectedTransactions.Rows.Count > 0)
                {
                    XtraReport report = new VouReport(); // Your custom report
                    report.DataSource = selectedTransactions;

                    // This might be necessary depending on how your report is designed
                    report.DataMember = ""; // Typically this is the name of the DataTable if using DataSet

                    ReportPrintTool printTool = new ReportPrintTool(report);
                    printTool.ShowPreviewDialog(); // Show preview dialog
                }
                else
                {
                    MessageBox.Show("The selected data has no rows to print.");
                }
            }
            else
            {
                MessageBox.Show("Please select at least one transaction to print.");
            }
        }

        private DataTable GetSelectedTransactionsData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Num", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Account", typeof(string));
            dataTable.Columns.Add("Debit", typeof(decimal));
            dataTable.Columns.Add("Credit", typeof(decimal));
            dataTable.Columns.Add("CreditWords", typeof(string));

            if (dgv_Voucher.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_Voucher.SelectedRows)
                {
                    string num = Convert.ToString(row.Cells["Num"].Value);
                    if (transactionDetails.ContainsKey(num))
                    {
                        bool creditWordsSet = false; // Flag to track if CreditWords has been set
                        decimal totalDebit = 0; // Variable to sum the debit values

                        foreach (DataRow detailRow in transactionDetails[num])
                        {
                            DataRow newRow = dataTable.NewRow();
                            newRow["Date"] = detailRow["Date"];
                            newRow["Num"] = detailRow["Num"];
                            newRow["Name"] = detailRow["Name"];
                            newRow["Description"] = detailRow["Memo/Description"];
                            newRow["Account"] = detailRow["Account"];

                            // Handling Debit values
                            string debitStr = detailRow["Debit"].ToString();
                            if (debitStr == "--" || !decimal.TryParse(debitStr, out decimal debitValue))
                            {
                                newRow["Debit"] = 0; // Default to zero for invalid values
                            }
                            else
                            {
                                newRow["Debit"] = debitValue;
                                totalDebit += debitValue; // Add to the total debit sum
                            }

                            // Handling Credit values
                            string creditStr = detailRow["Credit"].ToString();
                            decimal creditValue = 0;
                            if (!creditWordsSet && creditStr != "--" && decimal.TryParse(creditStr, out creditValue))
                            {
                                newRow["Credit"] = creditValue;
                                //newRow["CreditWords"] = AmountInWords.NumberToWords(totalDebit); // Use totalDebit for words
                                creditWordsSet = true; // Set the flag to prevent overriding
                            }
                            else
                            {
                                newRow["Credit"] = 0; // Or handle appropriately if multiple credits
                                newRow["CreditWords"] = AmountInWords.NumberToWords(totalDebit);
                                //newRow["CreditWords"] = creditWordsSet ? "" : "zero"; // Set "zero" only if CreditWords hasn't been set
                            }

                            dataTable.Rows.Add(newRow);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No rows selected in DataGridView.");
            }

            return dataTable;
        }



        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
