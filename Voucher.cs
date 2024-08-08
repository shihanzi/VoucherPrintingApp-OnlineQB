using ClosedXML.Excel;
using DevExpress.XtraReports.UI;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using System.IO;
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

            DataGridViewCheckBoxColumn selectedColumn = new DataGridViewCheckBoxColumn();
            selectedColumn.Name = "Selected";
            selectedColumn.HeaderText = "Selected";
            selectedColumn.FalseValue = false;
            selectedColumn.TrueValue = true;
            dgv_Voucher.Columns.Add(selectedColumn);
            dgv_Voucher.Columns["Selected"].DisplayIndex = 0;
            dgv_Voucher.AllowUserToAddRows = false;

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
                    if (string.IsNullOrEmpty(row.Cell(6).GetValue<string>()))
                    {
                        continue; // Skip this row if the "Name" column is null or empty
                    }
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
         

        }

        public XtraReport LoadReportFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                XtraReport report = new XtraReport();
                report.LoadLayout(filePath);
                return report;
            }
            else
            {
                throw new FileNotFoundException($"Report file not found: {filePath}");
            }
        }


        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataTable selectedTransactions = GetSelectedTransactionsData();
            if (dgv_Voucher.SelectedRows.Count > 0)
            {
                //DataTable selectedTransactions = GetSelectedTransactionsData();
                if (selectedTransactions.Rows.Count > 0)
                {
                    string reportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    XtraReport report;
                    if (selectedTransactions.Rows.Count <= 10)
                    {
                        string reportPathA5 = Path.Combine(reportDirectory, "VouReportA5.repx");
                        report = new VouReportA5(); // Use A5 report if 10 or fewer rows
                    }
                    else
                    {
                        string reportPathA4 = Path.Combine(reportDirectory, "VouReport.repx");
                        report = new VouReportA5(); // Use A4 report if more than 10 rows
                    }

                    report.DataSource = selectedTransactions;
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
            dataTable.Columns.Add("VouNumber", typeof(string));
            dataTable.Columns.Add("Memo", typeof(string));
            dataTable.Columns.Add("TotalDebit", typeof(decimal));
            dataTable.Columns.Add("TransactionID", typeof(string));

            if (dgv_Voucher.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_Voucher.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = row.Cells["Selected"] as DataGridViewCheckBoxCell;
                    if (checkBox != null && Convert.ToBoolean(checkBox.Value))
                    {
                        string num = Convert.ToString(row.Cells["Num"].Value);
                        if (transactionDetails.ContainsKey(num))
                        {
                            bool creditWordsSet = false;
                            decimal totalDebit = 0;

                            foreach (DataRow detailRow in transactionDetails[num])
                            {
                                DataRow newRow = dataTable.NewRow();
                                newRow["Date"] = detailRow["Date"];
                                newRow["Num"] = detailRow["Num"];
                                newRow["Name"] = detailRow["Name"];
                                newRow["Description"] = detailRow["Memo/Description"];
                                newRow["Account"] = detailRow["Account"];
                                newRow["TransactionID"] = detailRow["Num"];

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
                                    newRow["TotalDebit"] = totalDebit;
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
                                    newRow["CreditWords"] = AmountInWords.NumberToWords(totalDebit) + " rupees only";
                                    //newRow["CreditWords"] = creditWordsSet ? "" : "zero"; // Set "zero" only if CreditWords hasn't been set
                                }

                                string memo = detailRow["Memo/Description"].ToString();
                                string[] parts = memo.Split('#');
                                if (parts.Length == 2)
                                {
                                    newRow["VouNumber"] = parts[0];
                                    newRow["Memo"] = parts[1];
                                }
                                else
                                {
                                    newRow["VouNumber"] = "";
                                    newRow["Memo"] = memo;
                                }


                                dataTable.Rows.Add(newRow);
                            }
                        }
                    }
                }
            }
            return dataTable;
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable selectedTransactions = GetSelectedTransactionsData();
            if (dgv_Voucher.SelectedRows.Count > 0)
            {
                //DataTable selectedTransactions = GetSelectedTransactionsData();
                if (selectedTransactions.Rows.Count > 0)
                {
                    string reportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    XtraReport report;
                    if (selectedTransactions.Rows.Count <= 10)
                    {
                        string reportPathA5 = Path.Combine(reportDirectory, "VouReportA5.repx");
                        report = new VouReportA5(); // Use A5 report if 10 or fewer rows
                    }
                    else
                    {
                        string reportPathA4 = Path.Combine(reportDirectory, "VouReport.repx");
                        report = new VouReportA5(); // Use A4 report if more than 10 rows
                    }

                    report.DataSource = selectedTransactions;
                    ReportPrintTool printTool = new ReportPrintTool(report);
                    printTool.Print(); // Show preview dialog
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

        private void Voucher_Load(object sender, EventArgs e)
        {
            dgv_Voucher.ClearSelection();
        }
    }
}
