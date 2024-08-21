using DocumentFormat.OpenXml.Office.CoverPageProps;
using Newtonsoft.Json;
using System.IO;

namespace VoucherPrintingApp
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        public string SelectedCompanyName { get; private set; }
        public string SelectedCompanyAddress { get; private set; }

        private List<Company> Companies;

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            Voucher voucher = new Voucher(this);
            voucher.Show();
        }

        private List<Company> LoadCompanies()
        {
            string jsonPath = @"C:\easypack\CompanyData.json";

            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("The file does not exist.");
                return new List<Company>(); // Return an empty list if the file doesn't exist
            }

            var json = File.ReadAllText(jsonPath);

            if (string.IsNullOrWhiteSpace(json))
            {
                MessageBox.Show("The JSON file is empty or only contains whitespace.");
                return new List<Company>(); // Return an empty list if the JSON is empty
            }

            var companyList = JsonConvert.DeserializeObject<CompanyList>(json);
            return companyList?.Companies ?? new List<Company>();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            Companies  = LoadCompanies();

            if (Companies == null || Companies.Count == 0)
            {
                MessageBox.Show("No companies loaded.");
                return;
            }
            foreach (var name in Companies)
            {
                cmb_Company.Items.Add(name.CompanyName);
            }
            if (cmb_Company.Items.Count > 0)
            {
                cmb_Company.SelectedIndex = 0;
            }
        }

        private void cmb_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cmb_Company.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < Companies.Count)
            {
                lbl_Address.Text = Companies[selectedIndex].Address;
            }
        }

        public (string companyName,string companyAddress) SelectedDetails()
        {
            Companies = LoadCompanies();
            int selectedIndex = cmb_Company.SelectedIndex;
            if(selectedIndex >= 0 && selectedIndex <= Companies.Count)
            {
                string companyName = Companies[selectedIndex].CompanyName;
                string companyAddress = Companies[selectedIndex].Address;
                return (companyName, companyAddress);
            }       
            return (string.Empty,string.Empty);
        }
    }
}
