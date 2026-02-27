using System.Drawing;
using System.Numerics;

namespace Ref_Out_In
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The ref Keyword
            //1. The Swapper
            int a = 5;
            int b = 8;
            Swap(ref a, ref b);
            Console.WriteLine($"Value of a: {a}");
            Console.WriteLine($"Value of b: {b}");

            //2. The Bank Teller
            decimal accountBalance = 850;
            Withdraw(ref accountBalance, 700);
            Console.WriteLine($"Amount left:{accountBalance}");

            //3. String Sanitizer
            string text = "The Ref Keyword    ";
            CleanText(ref text);
            Console.WriteLine($"Text after CleanText method: {text}");

            //4. The Wrapper
            int myValue = 5;
            int max = 10;
            int min = 7;
            WrapValue(ref myValue, min, max);
            Console.WriteLine($"My value is : {myValue}");

            //5. Array Element Updater
            int[] myArray = new int[] { 1, 2, 3, 4 };
            DoubleElement(ref myArray);
            Console.Write($"Value after double up is: ");
            foreach (int value in myArray)
            {
                Console.Write($"{value} ");
            }

            //The out keyword
            //1. The Divider
            Divide(10, 3, out int q, out int r);
            Console.WriteLine($"Dividend: 10 Divisor:3 Quotient:{q} Remainder:{r}");

            //2. Coordinate Finder
            GetPoint(out int x, out int y);
            Console.WriteLine($"Coordinate x and y : ({x},{y})");

            //3. Area and Perimeter:
            RectangleCalculator(9, 6, out int perimeter, out int area);
            Console.WriteLine($"Rectangle perimeter: {perimeter}");
            Console.WriteLine($"Rectangle area: {area}");

            //4. Login Validator
            ValidateLogin("", out string errorMessage);
            Console.WriteLine($"Valid login: {errorMessage}");

            //5. The Multi-Parser
            MultiParser("0", out int output1, out double output2, out bool output3);
            Console.WriteLine($"{nameof(output1)}: {output1}");
            Console.WriteLine($"{nameof(output2)}: {output2}");
            Console.WriteLine($"{nameof(output3)}: {output3}");

            //The in keyword
            //1. The logger
            string message = "Hello there!";
            Log(message);

            //2. Vector Magnitude
            Vector3 v3 = new Vector3(1.2f, 2.2f, 4.5f);
            CalculateMagnitude(v3);

            //3. Tax Calculator
            decimal price = 10.0m;
            decimal tax = 5m;
            Console.WriteLine($"Price before applying tax is {price}");
            Console.WriteLine($"Price after apply tax is {ApplyTax(price, tax)}");

            //4. Config Reader
            AppConfig appConfig = new AppConfig("Config Name 1", "5.32", "Configuration setting for Server");
            DisplaySetting(appConfig);

            //5. Distance Checker
            Point target = new Point(4, 5);
            Point player = new Point(7, 8);
            float range = 15f;
            Console.WriteLine($"Is the player {player} close to Target {target} : {IsWithinRange(target, player, range)}");
        }

        static void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        static void Withdraw(ref decimal balance, decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawed {amount}");
            }
            else
            {
                Console.WriteLine("Insufficient balance to withdraw");
            }
        }

        static void CleanText(ref string input)
        {
            input = input.Replace(" ", "").Trim().ToLower();
        }

        static void WrapValue(ref int value, int min, int max)
        {
            if (value > max)
                value = min;
            else if (value < min)
                value = max;
        }

        static void DoubleElement(ref int[] element)
        {
            for (int i = 0; i < element.Length; i++)
            {
                element[i] *= 2;
            }
        }

        static void Divide(int dividend, int divisor, out int quotient, out int remainder)
        {
            quotient = dividend / divisor;
            remainder = dividend % divisor;
        }

        static void GetPoint(out int x, out int y)
        {
            Console.Write($"Enter coordinate x:");
            x = int.Parse(Console.ReadLine()); ;
            Console.Write($"Enter coordinate y:");
            y = int.Parse(Console.ReadLine()); ;
        }

        static void RectangleCalculator(int length, int width, out int perimeter, out int area)
        {
            perimeter = (width + length) * 2;
            area = width * length;
        }

        static void ValidateLogin(string input, out string errorMessage)
        {
            if (String.IsNullOrEmpty(input))
                errorMessage = "Username required";
            else
                errorMessage = null;
        }

        static void MultiParser(string input, out int output1, out double output2, out bool output3)
        {
            Int32.TryParse(input, out output1);
            Double.TryParse(input, out output2);
            Boolean.TryParse(input, out output3);
        }

        static void Log(in string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }

        static double CalculateMagnitude(in Vector3 vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }

        static decimal ApplyTax(decimal price, in decimal taxRate)
        {
            return price * taxRate + price;
        }

        static void DisplaySetting(in AppConfig config)
        {
            Console.WriteLine($"Config Name: {config.AppConfigName}");
            Console.WriteLine($"Config Version: {config.AppConfigVersion}");
            Console.WriteLine($"Config Description: {config.AppConfigDescription}");
        }

        static bool IsWithinRange(in Point target, in Point player, float range)
        {
            double result = Math.Sqrt(Math.Pow(target.X - player.X, 2) + Math.Pow(target.Y - player.Y, 2));

            return result < range;
        }
    }

    struct AppConfig
    {
        public string AppConfigName { get; set; }
        public string AppConfigVersion { get; set; }
        public string AppConfigDescription { get; set; }

        public AppConfig(string appConfigName, string appConfigVersion, string appConfigDescription)
        {
            this.AppConfigName = appConfigName;
            this.AppConfigVersion = appConfigVersion;
            this.AppConfigDescription = appConfigDescription;
        }
    }
}
