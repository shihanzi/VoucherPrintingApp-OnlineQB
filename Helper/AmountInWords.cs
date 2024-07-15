using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherPrintingApp.Helper
{
    internal class AmountInWords
    {
        public static string NumberToWords(decimal number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            // Extract the whole part and the cents part
            int wholePart = (int)number;
            int centsPart = (int)((number - wholePart) * 100);

            // Convert the whole part
            words = ConvertWholeNumberToWords(wholePart);

            // Append cents if there are any
            if (centsPart > 0)
            {
                if (!string.IsNullOrEmpty(words))
                    words += " and ";  // Adding 'and' before the cents part

                words += ConvertWholeNumberToWords(centsPart) + " cents";
            }

            return words;
        }

        private static string ConvertWholeNumberToWords(int number)
        {
            if (number == 0)
                return "";

            string words = "";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if ((number / 1000000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertWholeNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";  // Correct placement of 'and'

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    int tensIndex = number / 10;
                    int unitsIndex = number % 10;

                    // Ensure the indices are within bounds
                    if (tensIndex < tensMap.Length)
                        words += tensMap[tensIndex];

                    if (unitsIndex > 0 && unitsIndex < unitsMap.Length)
                        words += "-" + unitsMap[unitsIndex];
                }
            }

            return words.Trim();
        }


        //    public static string NumberToWords(decimal number)
        //    {
        //        if (number == 0)
        //            return "zero";

        //        if (number < 0)
        //            return "minus " + NumberToWords(Math.Abs(number));

        //        string words = "";

        //        // Extract the whole part and the cents part
        //        int wholePart = (int)number;
        //        int centsPart = (int)((number - wholePart) * 100);

        //        // Convert the whole part
        //        words = ConvertWholeNumberToWords(wholePart);

        //        // Append cents if there are any
        //        if (centsPart > 0)
        //        {
        //            if (!string.IsNullOrEmpty(words))
        //                words += " and ";  // Adding 'and' before the cents part

        //            words += ConvertWholeNumberToWords(centsPart) + " cents";
        //        }

        //        return words;
        //    }

        //    private static string ConvertWholeNumberToWords(int number)
        //    {
        //        if (number == 0)
        //            return "";

        //        string words = "";

        //        if ((number / 1000000) > 0)
        //        {
        //            words += ConvertWholeNumberToWords(number / 1000000) + " million ";
        //            number %= 1000000;
        //        }

        //        if ((number / 1000) > 0)
        //        {
        //            words += ConvertWholeNumberToWords(number / 1000) + " thousand ";
        //            number %= 1000;
        //        }

        //        if ((number / 100) > 0)
        //        {
        //            words += ConvertWholeNumberToWords(number / 100) + " hundred ";
        //            number %= 100;
        //        }

        //        if (number > 0)
        //        {
        //            if (words != "")
        //                words += "and ";  // Correct placement of 'and'

        //            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        //            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        //            if (number < 20)
        //                words += unitsMap[number];
        //            else
        //            {
        //                words += tensMap[number / 10];
        //                if ((number % 10) > 0)
        //                    words += "-" + unitsMap[number % 10];
        //            }
        //        }
        //        return words.Trim();
        //    }
    }

}
