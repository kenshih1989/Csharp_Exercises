using System.Security.Cryptography.X509Certificates;

namespace Class_Static_Access_Modifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Bank Vault
            BankAccount myAccount = new BankAccount(1000.0);
            myAccount.Deposit(500.0);
            myAccount.GetBalance();

            //2. The Library Book
            Book myBook = new Book("978-0451524935");
            myBook.Title = "1984";
            myBook.Author = "George Orwell";
            //myBook.ISBN = "123-4567890123"; // This line will cause a compile-time error
            Console.WriteLine($"Title: {myBook.Title}, Author: {myBook.Author}, ISBN: {myBook.ISBN}");

            //3. The Global User Counter
            User user1 = new User();
            Console.WriteLine($"User count value is {User.userCount}");

            //4. The "Math Wizard"
            Console.WriteLine($"5 + 10 = {Calculator.Add(5, 10)}");
            Console.WriteLine($"Area of circle with radius 3: {Calculator.CalculateCircleArea(3)}");

            //5. The ID Generator
            Employee emp1 = new Employee();
            Employee emp2 = new Employee();
            Console.WriteLine($"Employee 1 ID: {emp1.EmployeeID}");
            Console.WriteLine($"Employee 2 ID: {emp2.EmployeeID}");
            Console.WriteLine($"The next Employee ID will be {Employee.nextId}"); 

        }
        class BankAccount
        {
            private double balance;

            public BankAccount(double initialBalance)
            {
                balance = initialBalance;
            }

            public void Deposit(double amount)
            {
                if (amount > 0)
                {
                    balance += amount;
                }
            }

            public void GetBalance()
            {
                Console.WriteLine($"Current Balance: {balance.ToString("C")}");
            }
        }

        class Book
        {
            public string Title;
            public string Author;
            // Read-only from the outside, settable only inside
            public string ISBN { get; private set; }
            public Book(string isbn)
            {
                this.ISBN = isbn;
            }
        }

        class User
        {
            public static int userCount = 0;
            public User()
            {
                userCount++;
            }
        }

        static class Calculator
        {
            public static double Add(double a, double b)
            {
                return a + b;
            }

            public static double CalculateCircleArea(double radius)
            {
                return Math.PI * radius * radius;
            }
        }

        class Employee
        {
            public static int nextId = 101;
            public int EmployeeID { get; private set; }
            public Employee()
            {
                EmployeeID = nextId;
                nextId++;
            }
        }
    }
}
