using System.Reflection.Metadata.Ecma335;
using System.Timers;

namespace Switch_Patern_Matching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Shape Shifter (Property Pattern)
            Shape myFirstShape = new Rectangle(4.3, 7.3);
            Shape mySecondShape = new Circle(5.0);
            Shape myThridShape = new Square(8.0);
            Shape myFourthShape = new Shape();

            Console.WriteLine($"Area of the shape: {GetArea(myFirstShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(mySecondShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(myThridShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(myFourthShape)}");

            //2. The Cinema Usher (Relational & Logical Patterns)
            Console.WriteLine(GetTicketCategory(2));
            Console.WriteLine(GetTicketCategory(8));
            Console.WriteLine(GetTicketCategory(16));
            Console.WriteLine(GetTicketCategory(50));
            Console.WriteLine(GetTicketCategory(68));
            Console.WriteLine(GetTicketCategory(-4));

            //3. The Delivery Logistics (Tuple Pattern)
            Console.WriteLine(CalculateShipping("International", 15.8));
            Console.WriteLine(CalculateShipping("International", 2.4));
            Console.WriteLine(CalculateShipping("Domestic", 7.5));
            Console.WriteLine(CalculateShipping("Domestic", 2.0));
            Console.WriteLine(CalculateShipping("Neighbourhood", 2.0));

            //4. The Smart Thermostat (Positional Pattern)
            TemperatureReading(259, true);
            TemperatureReading(10, true);
            TemperatureReading(105, false);
            TemperatureReading(25, false);

            //5. The File Processor (List Patterns)
            int[] myIntArray1 = new int[] { };
            int[] myIntArray2 = new int[1] {7};
            int[] myIntArray3 = new int[5] {1,2,3,4,5 };
            int[] myIntArray4 = new int[7] {5,4,8,6,2,1,0 };

            Console.WriteLine(AnalyzeData(myIntArray1));
            Console.WriteLine(AnalyzeData(myIntArray2));
            Console.WriteLine(AnalyzeData(myIntArray3));
            Console.WriteLine(AnalyzeData(myIntArray4));

        }
        static double GetArea(object shape)
        {
            switch (shape)
            {
                case Rectangle rectangle:
                    return rectangle.Width * rectangle.Height;
                case Circle circle:
                    return circle.Radius * Math.PI;
                case Square square:
                    return square.Side * square.Side;
                default:
                    return 0;

            }
        }

        static string GetTicketCategory(int age)
        {
            //switch (age)
            //{
            //    case >= 0 and <= 2:
            //        return "Infant (Free)";
            //    case >= 3 and <= 12:
            //        return "Child";
            //    case >= 13 and <= 19:
            //        return "Teenager";
            //    case >= 20 and <= 64:
            //        return "Adult";
            //    case >= 65:
            //        return "Senior";
            //    default:
            //        return "Invalid age number";
            //}

            //C# (8.0+)
            string result = (age) switch
            {
                (>= 0 and <= 2) => "Infant (Free)",
                (>= 3 and <= 12) => "Child",
                (>= 13 and <= 19) => "Teenager",
                (>= 20 and <= 64) => "Adult",
                (>= 65) => "Senior",
                _ => "Invalid age number",
            };
            return result;
        }

        static decimal CalculateShipping(string zone, double weight)
        {
            // Use tuple patterns that match the (zone, weight) tuple.
            //switch (zone, weight)
            //{
            //    case ("International", > 10):
            //        return 50.00m;
            //    case ("International", <= 10):
            //        return 25.00m;
            //    case ("Domestic", > 5):
            //        return 10.00m;
            //    case ("Domestic", <= 5):
            //        return 5.00m;
            //    default:
            //        return 0.0m;
            //}

            //C# (8.0+)
            var result = (zone, weight) switch
            {
                ("International", > 10) => 50.00m,
                ("International", <= 10) => 25.00m,
                ("Domestic", > 5) => 10.00m,
                ("Domestic", <= 5) => 5.00m,
                (_, _) => 0.0m,
            };

            return result;
        }

        static void TemperatureReading(double temp, bool isFahrenheit)
        {
            double calculatedTemperature;
            calculatedTemperature = isFahrenheit ? (temp - 32) * 5 / 9 : temp;

            // Use a tuple pattern with a boolean literal for the first element
            // and a relational pattern for the second element.
            switch (isFahrenheit, calculatedTemperature)
            {
                case (true, >= 100):
                    Console.WriteLine($"Celsius temperature is above 100: {calculatedTemperature} ");
                    break;
                case (true, < 100):
                    Console.WriteLine($"Standard temperature in celsius: {calculatedTemperature}");
                    break;
                case (false, >= 100):
                    Console.WriteLine($"Celsius temperature is above 100: {calculatedTemperature} ");
                    break;
                case (false, < 100):
                    Console.WriteLine($"Standard temperature in celsius: {calculatedTemperature}");
                    break;
                default:
                    Console.WriteLine($"{temp}");
                    break;
            }
        }

        public static string AnalyzeData(int[] numbers) => numbers switch
        {
            [] => "No data",                          // Matches empty
            [var single] => $"Single value: {single}", // Matches exactly one element
            [1, 2, ..] => "Starts with 1 and 2",       // Matches 1, 2 followed by anything (spread)
            [.., 0] => "Sequence ends in zero",        // Matches anything ending in 0
            _ => "General sequence"                    // Default
        };
        
    }



    public class Shape
    {

    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            this.Radius = radius;
        }
    }

    public class Square : Shape
    {
        public double Side { get; set; }
        public Square(double side)
        {
            this.Side = side;
        }
    }


}
