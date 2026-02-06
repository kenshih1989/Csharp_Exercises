
namespace Functions_and_Scopes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Temperature Converter
            Console.WriteLine("0 Celsius equal to " + CelsiusToFahrenheit(0) + " Fahrenheit");
            Console.WriteLine("25 Celsius equal to " + CelsiusToFahrenheit(25) + " Fahrenheit");
            Console.WriteLine("100 Celsius equal to " + CelsiusToFahrenheit(100) + " Fahrenheit");

            //2. The Global vs Local Scope
            double taxRate = 0.1; //this is a global variable for Main method
            double price = 100.0;
            Console.WriteLine("Total price with local tax rate: " + CalculateTotalPrice(price)); //120

            //3. The "Ref" Parameter Swap 
            int myNumber = 10;
            DoubleValue(ref myNumber);
            Console.WriteLine("Doubled value: " + myNumber); //20

            //4.The Username Validator
            string username = "User1234";
            if (IsValidUsername(username))
            {
                Console.WriteLine("Access Granted");
            }
            else
            {
                Console.WriteLine("Invalid Entry.");
            }

            //5. Area Overloading
            Console.WriteLine("Area of square (7): " + GetArea(7)); //49
            Console.WriteLine("Area of rectangle (5, 10): " + GetArea(5, 10)); //50

        }

        static double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

        static double CalculateTotalPrice(double price)
        {
            double taxRate = 0.2;
            return price + (price * taxRate); // Uses local taxRate
        }

        static int DoubleValue(ref int myNumber)
        {
            return myNumber *= 2;
        }

        static bool IsValidUsername(string username)
        {
            if (username.Length < 8 || username.Length > 15) return false;
            if (username.Contains(" ")) return false;
            if (!char.IsUpper(username[0])) return false;

            return true;
        }

        static double GetArea(int v)
        {
            return v * v;
        }

        static double GetArea(int length, int width)
        {
            return length * width;
        }
    }
}
