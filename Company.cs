using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherPrintingApp
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }

    }
    public class CompanyList
    {
        public List<Company> Companies { get; set; }
    }
}
