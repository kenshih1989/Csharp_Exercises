using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function_Extension
{
    // Rule 1:Static Class
    public static class StringExtensions
    {
        // Rule 2:Static Method + Rule 3: 'this' keyword
        public static string ToShout(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "!!!";
            return s.ToUpper() + "!!!";
        }

        // Rule 2: Static Method + Rule 3: 'this' keyword
        // Removes any leading or trailing whitespace
        public static string ToClean(this string rawInput)
        {
            return rawInput.Trim();
        }
        // Rule 2: Static Method + Rule 3: 'this' keyword
        // Adds an "@" symbol to the front
        public static string ToUserTag(this string rawInput)
        {
            return "@" + rawInput;
        }
        // Rule 2: Static Method + Rule 3: 'this' keyword
        // Wraps the string in brackets to make it look like a notification.
        public static string WithAlert(this string rawInput)
        {
            return "[ " + rawInput + " ]";
        }
    }
}
