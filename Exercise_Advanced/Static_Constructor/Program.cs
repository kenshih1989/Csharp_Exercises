using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Static_Constructor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Global Counter
            Product product1 = new Product();
            Product product2 = new Product();
            Product product3 = new Product();
            Console.WriteLine($"The product id for product 1 is {product1.GetProductId()}");
            Console.WriteLine($"The product id for product 2 is {product2.GetProductId()}");
            Console.WriteLine($"The product id for product 3 is {product3.GetProductId()}");

            //2. Application Settings Loader
            Console.WriteLine("Printing the setting of App Config");
            Console.WriteLine($"Theme: {AppConfig.Theme}");
            Console.WriteLine($"MaxLoginAttemps: {AppConfig.MaxLoginAttempts}");
            Console.WriteLine($"LastBootTime: {AppConfig.LastBootTime}");
            Thread.Sleep(1000);
            Console.WriteLine("Printing the setting of App Config after 1 second");
            Console.WriteLine($"Theme: {AppConfig.Theme}");
            Console.WriteLine($"MaxLoginAttemps: {AppConfig.MaxLoginAttempts}");
            Console.WriteLine($"LastBootTime: {AppConfig.LastBootTime}");

            //3. The Math Utility Constant
            CircleCalculator circleCalculator1 = new CircleCalculator();
            CircleCalculator circleCalculator2 = new CircleCalculator();
            CircleCalculator circleCalculator3 = new CircleCalculator();
            CircleCalculator circleCalculator4 = new CircleCalculator();
            CircleCalculator circleCalculator5 = new CircleCalculator();
            Console.WriteLine($"PrecomputedValue for the {nameof(CircleCalculator)} is {CircleCalculator.PrecomputedValue}");

            //4. Validating Static Data
            SystemValidator systemValidator1 = new SystemValidator();
            Console.WriteLine($"The status of today is {systemValidator1.ShowStatus()}");

            //5. Static vs Instance Order
            Console.WriteLine($"The log trace before the instance created: {Logger.LogTrace}");
            Logger logger = new Logger();
            Console.WriteLine($"The log trace after the instance created: {Logger.LogTrace}");

        }
    }

    class Product
    {
        public static int NextId { get; private set; }
        private int ProductId { get; }

        static Product()
        {
            Random random = new Random();
            NextId = random.Next(1000, 2000);
            Console.WriteLine($"Randomly generated a value for the NextID: {NextId}");
        }

        public Product()
        {
            this.ProductId = NextId;
            NextId++;
        }

        public int GetProductId()
        {
            return this.ProductId;
        }

    }

    static class AppConfig
    {
        public static string Theme;
        public static int MaxLoginAttempts;
        public static DateTime LastBootTime;

        static AppConfig()
        {
            Theme = "Dark";
            MaxLoginAttempts = 5;
            LastBootTime = DateTime.Now;
            Console.WriteLine("App config initialized.");
        }
    }

    class CircleCalculator
    {
        public static double PrecomputedValue;

        static CircleCalculator()
        {
            PrecomputedValue = Math.E * Math.PI;
            Console.WriteLine("Static Initialization Done");
        }

        public CircleCalculator()
        {
            Console.WriteLine("Instance Created.");
        }

    }

    class SystemValidator
    {
        public static string EnvironmentName;

        static SystemValidator()
        {
            EnvironmentName = DateTime.Today.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
        ? "Maintenance Mode"
        : "Production";
        }

        public string ShowStatus()
        {
            return EnvironmentName;
        }
    }

    class Logger
    {
        public static string LogTrace;

        static Logger()
        {
            LogTrace = "[Static]";
        }

        public Logger()
        {
            LogTrace += "[Instance]";
        }
    }
}
