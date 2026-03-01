using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Tuples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Geometry Calculator
            int length = 5;
            int width = 8;
            var rectangleDetails = CalculateRectangle(length, width);

            Console.WriteLine($"Area of rectangle: {rectangleDetails.area}");
            Console.WriteLine($"Perimeter of rectangle: {rectangleDetails.perimeter}");

            //2. The "Min-Max" Array Finder
            int[] myInt1 = new int[] { };
            int[] myInt2 = new int[] { 4, 7, 2, 5 };

            var result1 = GetLimits(myInt1);
            var result2 = GetLimits(myInt2);

            Console.WriteLine($"Result 1: Min:{result1.minimun} Max:{result1.maximum}");
            Console.WriteLine($"Result 2: Min:{result2.minimun} Max:{result2.maximum}");

            //3. Quick Swap Logic
            int a = 10;
            int b = 20;
            Console.WriteLine($"Before Swap: a = {a},b = {b}");
            (a, b) = (b,a);
            Console.WriteLine($"After Swap: a = {a},b = {b}");

            //4. Parsing Sensitive Data (Deconstruction)
            var (fullName, email, _) = GetUserData();
            Console.WriteLine($"Full Name: {fullName}");
            Console.WriteLine($"Email: {email}");

            //5. List of Tuples (Inventory Management)
            var stores = new List<(string Name, int Qty, decimal Price)>
            { 
                ("P1",5,5.8m),
                ("P3",3,2.3m),
                ("P4",8,4.9m)
            };

            foreach (var item in stores)
            {
                Console.WriteLine($"Product:{item.Name} Total Value: {(item.Qty* item.Price).ToString("C")}");
            }

        }

        static (int area, int perimeter) CalculateRectangle(int length, int width)
        {
            return (length * width, 2 * (length + width));
        }

        static (int minimun, int maximum) GetLimits(int[] input)
        {
            if (input.Length == 0)
            {
                return (0, 0);
            }

            int max = input[0];
            int min = input[0];
            foreach (var number in input)
            {
                if (number < min)
                    min = number;

                if (number > max)
                    max = number;
            }

            return (min, max);
        }

        static (string FullName, string Email, string SSN) GetUserData()
        {
            return ("John Smith", "JS@email.com", "SSN0001");
        }
    }
}
