namespace Expression_bodied_Members
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Area of a circle with radius 5 is: {CalculateCircleArea(5)}");
            
            Product product = new Product();
            product.Price = 10;
            product.Discount = 3;
            Console.WriteLine($"The {nameof(Product.FinalPrice)} is {product.FinalPrice}");

            User user = new User("ADMIN");
            user.PrintGreeting();
            user.Email = " Admin@example.com ";
            Console.WriteLine($"The user email is {user.Email}");

            Car car = new Car();
            car.Make = "Honda";
            car.Model = "Civic";
            Console.WriteLine($"The car is {car}");
        }

        //1. The Basic Method
        static public double CalculateCircleArea(double radius) => Math.PI * Math.Pow(radius, 2);
    }

    //2. The Read-Only Property
    public class Product
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public decimal FinalPrice {
            get => Price - Discount;  
         }
    }

    //3. Constructor and Local Function
    public class User
    {
        private string _username;

        public User(string name) => this._username = name.ToLower();

        public string GetMessage() => $"Hello,{_username}";
        public void PrintGreeting() => Console.WriteLine(GetMessage());

        //4. The Property "Set" Accessor
        private string _email;
        public string Email
        {
            get => _email;
            set => _email = value.Trim();
        }
    }

    //5. Overriding Methods
    public class Car
    {
        public string Make {  get; set; }
        public string Model { get; set; }

        public override string ToString() => $"{Make} {Model}";
    }
}
