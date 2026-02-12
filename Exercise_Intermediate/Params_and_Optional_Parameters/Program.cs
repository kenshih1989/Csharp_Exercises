




using System.Diagnostics;

namespace Params_and_Optional_Parameters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Params Keyword
            //The Summer
            int firstNumber = 3;
            int secondNumber = 4;
            int thridNumber = 5;

            int sumResult = Summer(firstNumber, secondNumber, thridNumber, 10, 20, 30);
            Console.WriteLine($"The sum is: {sumResult}");

            //The Sentence Builder
            string sentence = BuildSentence("Hello", "world!", "This", "is", "a", "test.");
            Console.WriteLine(sentence);

            //The Multi-Type Challenge
            double multiplier = 2.5;
            double[] values = { 1.0, 2.0, 3.0 };
            List<double> multipliedValues = Multiply(multiplier, values);
            Console.Write("Multiplied values:");
            foreach (double value in multipliedValues)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();

            //The logger
            Log("INFO", "This is a log message.", "It started when log started.");
            Log("WARNING", "This is a warning message.", "It just warn the user but the program still can be implemented.");
            Log("ERROR", "This is an error message.", "This mean the program can't be executed successfully");

            //Validation Check
            //A params parameter must be the last parameter in a formal parameter list.
            // Correction for the Method like below:
            // Invalid: InvalidMethod(params int[] nums, string name) --> This will throw an error
            // Valid: ValidMethod(string name, params int[] nums)


            //2. Optional Parameters
            // The Greeting Function
            GreetUser("Alice","Miss.");
            GreetUser("John","Mr.");
            GreetUser("");

            // Price Calculator 
            double price = 100.0;
            double totalPrice = CalculateTotal(100);
            Console.WriteLine($"Total price with default tax and no discount: {totalPrice}");
            totalPrice = CalculateTotal(100, 0.08);
            Console.WriteLine($"Total price with 8% tax and no discount: {totalPrice}");
            totalPrice = CalculateTotal(100, 0.08, 10);
            Console.WriteLine($"Total price with 8% tax and $10 discount: {totalPrice}");
            // Named Arguments
            totalPrice = CalculateTotal(100, discount: 15);
            Console.WriteLine($"Total price with default tax and $15 discount: {totalPrice}");

            // The Shape Maker
            CreateRectangle(3);
            CreateRectangle(10);

            // Order of Operation
            // Short answer is positional ambiguity.
            // If the compiler allowed you to mix optional and required parameters in any order,
            // it wouldn't be able to figure out which value belongs to which variable when you call the method.
        }



        public static int Summer(params int[] input)
        {
            int sum = 0;
            foreach (int number in input)
            {
                sum += number;
            }
            return sum;
        }

        private static string BuildSentence(params string[] words)
        {
            string sentence = string.Join(" ", words);
            return sentence;
        }

        private static List<double> Multiply(double multiplier, params double[] values)
        {
            List<double> result = new List<double>();
            foreach (double value in values)
            {
                result.Add(value* multiplier);
            }
            return result;
        }

        private static void Log(string level, params string[] messages)
        {
            Console.WriteLine($"[{level}]");
            foreach (string message in messages)
            {
                Console.WriteLine($" - {message}");
            }
        }

        private static void GreetUser(string name, string title = "Guest")
        {
            if (name != null)
            {
                Console.WriteLine($"Hello,{title}{name}");
            }
            else
            {
                Console.WriteLine("Hello, Guest");
            }
        }
        public static double CalculateTotal(double price, double taxRate = 0.05, double discount = 0)
        {
            double total = price + (price * taxRate) - discount;
            return total;
        }

        public static void CreateRectangle(double width, double height = 10)
        {
            if (width <= 0 || height <= 0)
            {
                Console.WriteLine("Width and height must be positive numbers.");
                return;
            }
            else if(width == height)
            {
                Console.WriteLine("Creating a square with side length: " + width);
            }
            else
            {
                Console.WriteLine($"Creating a rectangle with width: {width} and height: {height}");
            }
        }
    }
}
