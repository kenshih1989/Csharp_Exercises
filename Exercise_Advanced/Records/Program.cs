using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Records
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The One-Liner
            Product product = new Product("ProductA", 5.20m, "Grocery");
            Console.WriteLine(product);

            //2. Value-Based Equality 
            CoffeeClass coffeeClass1 = new CoffeeClass("Black", "Large");
            CoffeeClass coffeeClass2 = new CoffeeClass("Black", "Large");

            CoffeeRecord coffeeRecord1 = new CoffeeRecord("Black", "Small");
            CoffeeRecord coffeeRecord2 = new CoffeeRecord("Black", "Small");

            Console.WriteLine($"Is {nameof(coffeeClass1)} same as {nameof(coffeeClass2)}: {coffeeClass1 == coffeeClass2}");
            Console.WriteLine($"Is {nameof(coffeeRecord1)} same as {nameof(coffeeRecord2)}: {coffeeRecord1 == coffeeRecord2}");
            //CoffeeClass: Lives on the Heap. Comparing two objects checks if they point to the same memory address.
            //GPSLocation: Lives on the Stack. Comparing them checks the actual bits and bytes of the data.

            //3. Non-destructive Mutation
            User normalUser = new User("NormalUser", "NormalUser@Example.com", false);
            User adminUser = normalUser with { IsAdmin = true };
            Console.WriteLine(normalUser);
            Console.WriteLine(adminUser);

            //4. Record Inheritance & Primary Constructors
            ElectricCar electricCar1 = new ElectricCar("Toyota", "Altis", 2);
            ElectricCar electricCar2 = electricCar1 with { Model = "BMW", BatteryCapacity = 1 };
            Console.WriteLine(electricCar1);
            Console.WriteLine(electricCar2);

            //5. The "Record Struct" Performance Challenge
            GPSLocation gPSLocation1 = new GPSLocation(25.15, 12.69);
            Console.WriteLine(gPSLocation1);
            //Error as it only can be assigned in an object initializer
            //gPSLocation1.Latitude = 55.89; 
            // Update the value with With Expression
            gPSLocation1 = gPSLocation1 with { Latitude = 55.89 };
            Console.WriteLine(gPSLocation1);
        }
    }

    //1. The One-Liner
    public record Product(string Name, decimal Price, string Category);

    //2. Value-Based Equality
    class CoffeeClass
    {
        public string Name { get; init; }
        public string Size { get; init; }

        public CoffeeClass(string name, string size)
        {
            this.Name = name;
            this.Size = size;
        }
    }

    public record CoffeeRecord(string Name, string Size);

    public record User(string Username, string Email, bool IsAdmin);

    //4. Record Inheritance & Primary Constructors
    public record Vehicle(string Make, string Model);
    public record ElectricCar(string Make, string Model, int BatteryCapacity) : Vehicle(Make, Model);

    //5. The "Record Struct" Performance Challenge
    public readonly record struct GPSLocation(double Latitude, double Longitude);
}
