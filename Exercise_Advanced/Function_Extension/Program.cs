using System.Diagnostics;

namespace Function_Extension
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The "Shouting" String
            string myString = "hello";
            Console.WriteLine($"The original string: {myString}");
            Console.WriteLine($"The string with function extension: {myString.ToShout()}");

            //2. Currency Formatter
            double price = 19.5;
            Console.WriteLine($"The original double value: {price}");
            Console.WriteLine($"The double value in Currency format with function extension: {price.ToPrice()}");

            //3. Integer Boundaries
            int min = 0;
            int max = 10;
            int testedInteger = 5;
            Console.WriteLine($"Is {testedInteger} stay in between {min} and {max} : {testedInteger.IsBetween(min, max)}");

            //4. List Wrapper
            List<string> myList = new List<string>() { "Item1", "Item2", "Item3", "Item4", "Item5" };
            myList.PrintAll();

            //5. The "Safe" Division
            int divisor = 0;
            int divider = 10;
            Console.WriteLine($"{divider} divide by {divisor} is {divider.DivideBy(divisor)}");

            //6. The User Name Formatter
            string rawInput = " gemini_user ";
            Console.WriteLine($"Initial input: {rawInput}");
            Console.WriteLine($"Input after using chaining extension: {rawInput.ToClean().ToUserTag().WithAlert().ToUpper()}");

        }
    }

    // Rule 1:Static Class
    public static class VoidExtensions
    {
        // Rule 2: Static Method + Rule 3: 'this' keyword
        public static void PrintAll(this IEnumerable<string> s) 
        {
            foreach (string item in s)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
