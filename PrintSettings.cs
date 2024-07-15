using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherPrintingApp
{
    public class PrintSettings
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public float NameX { get; set; }
        public float NameY { get; set; }
        public float DateX { get; set; }
        public float DateY { get; set; }
        public float AmountX { get; set; }
        public float AmountY { get; set; }
        public float WordsX { get; set; }
        public float WordsY { get; set; }
        public float FontSize { get; set; }

        public float CompanyNameX { get; set; }
        public float CompanyNameY { get; set; }
        public float AddressX { get; set; }
        public float AddressY { get; set; }
        public float ContactX { get; set; }
        public float ContactY { get; set; }
        public float HeadingX { get; set; }
        public float HeadingY { get; set; }

    }
}
