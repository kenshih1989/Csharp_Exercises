using System.Net.Http.Headers;

namespace Anonymous_Types
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basic Profile
            var car = new { brand = "Tesla", model = "Model 3", year = 2024 };
            Console.WriteLine($"I drive a {car.year} {car.brand} {car.model}");

            //2.  Property Name Inference
            string city = "Tokyo";
            int population = 140000000;
            var location = new { city, population };
            Console.WriteLine(location);

            //3.  Read-Only Constraint
            var bankAccount = new { AccountNumber= 253792, Balance = 895.20 };
            //bankAccount.Balance = 900.50; //Get error as property of annoymous type is read-only
            Console.WriteLine(bankAccount);

            //4. Non-LINQ Collections
            var products = new[]
            {
                new {name = "P1",price=25.40m },
                new {name = "P2",price=3.60m },
                new {name = "P3",price=2.70m }
            };
            var totalPrice = 0.00m;
            foreach (var product in products)
            {
                totalPrice += product.price;
            }
            Console.WriteLine($"Total price of the products is: {totalPrice.ToString("C")}");

            //5. The "With" Expression (C# 10+)
            var originalPoint = new {x=10,y=20};
            var movedPoint = originalPoint with { y = 50 };
            Console.WriteLine($"Original Point: { originalPoint}");
            Console.WriteLine($"Moved Point: {movedPoint}");
        }
    }
}
