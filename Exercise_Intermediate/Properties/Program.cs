namespace Properties
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basic Auto-Property
            Book myBook = new Book();
            myBook.title = "The Great Gatsby";
            myBook.author = "F. Scott Fitzgerald";
            Console.WriteLine($"Title: {myBook.title}, Author: {myBook.author}");

            //2. The calculated property
            Rectangle myRectangle = new Rectangle();
            myRectangle.length = 5.0;
            myRectangle.width = 3.0;
            Console.WriteLine($"Area: {myRectangle.area}, Perimeter: {myRectangle.perimeter}");
            myRectangle.width = 2.0;
            Console.WriteLine($"Area: {myRectangle.area}, Perimeter: {myRectangle.perimeter}");

            //3. Validation (The Guard Dog)
            BankAccount myAccount = new BankAccount();
            myAccount.Deposit(1000);
            Console.WriteLine($"Balance after deposit: {myAccount.Balance}");
            myAccount.Withdraw(300);
            Console.WriteLine($"Balance after withdrawal: {myAccount.Balance}");
            myAccount.Deposit(-50);

            //4. The Immutable ID
            Employee emp = new Employee(1, "Alice");
            Console.WriteLine($"Employee ID: {emp.EmployeeID}, Name: {emp.Name}");
            //Test setting EmployeeID after initialization (should cause a compile-time error)
            //emp.EmployeeID = 2; // Uncommenting this line will cause a compile-time error

            //5. Secure Access
            SmartLight myLight = new SmartLight();
            myLight.adjustBrightness(30);
            Console.WriteLine($"Brightness: {myLight.Brightness}");
            myLight.adjustBrightness(80); // Should trigger validation
            Console.WriteLine($"Brightness: {myLight.Brightness}");
            myLight.adjustBrightness(-20);
            Console.WriteLine($"Brightness: {myLight.Brightness}");


        }
    }

    class Book
    {
        public string title { get; set; }
        public string author { get; set; }
    }

    class Rectangle
    {
        public double length { get; set; }
        public double width { get; set; }
        public double area => length * width;
        public double perimeter => 2 * (length + width);

    }

    class BankAccount
    {
        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            private set
            {
                if (value >= 0)
                {
                    _balance = value;
                }
                else
                {
                    Console.WriteLine("Balance cannot be negative.");
                }
            }
        }
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                _balance -= amount;
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
        }
    }

    class Employee
    {
        public int EmployeeID { get; init; }
        public string Name { get; set; }
        public Employee(int id, string name)
        {
            EmployeeID = id;
            Name = name;
        }
    }

    class SmartLight
    {
        private int brightness;

        public void adjustBrightness(int change)
        {
            Brightness += change;
        }
        public int Brightness
        {
            get => brightness;
            private set
            {
                if (value > 0 && value < 100)
                {
                    brightness = value;
                }
                else
                {
                    Console.WriteLine("Brightness must be between 0 and 100.");
                }
            }
        }
    }
}
