using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_Extension
{
    //Rule 1: Static class
    public static class NumericExtensions
    {
        // Rule 2: Static Method + Rule 3: 'this' keyword
        public static int DivideBy(this int i, int divisor)
        {
            return divisor == 0 ? 0 : i / divisor;
        }

        // Rule 2: Static Method + Rule 3: 'this' keyword
        public static string ToPrice(this double d)
        {
            return d.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }

        // Rule 2: Static Method + Rule 3: 'this' keyword
        public static bool IsBetween(this int i, int min, int max)
        {
            return i >= min && i <= max;
        }
    }
}
