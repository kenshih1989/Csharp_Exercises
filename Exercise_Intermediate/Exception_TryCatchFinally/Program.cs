using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exception_TryCatchFinally
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basics (Division Safety)
            bool isSuccessful = false;
            while (!isSuccessful)
            {
                
                try
                {
                    Console.Write($"Enter the divider: ");
                    int divider = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Enter the divisor: ");
                    int divisor = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"{divider} / {divisor} is {divider / divisor}");
                    isSuccessful = true;
                }
                catch (ArithmeticException ex)
                {
                    Console.WriteLine("Error: You cannot divide by zero.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter valid whole numbers.");
                }
            }
            

            //2. The "Finally" Guarantee

            Console.WriteLine("Opening connection...");
            try
            {
                throw new Exception("Data corruption detected");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling error:{ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Closing connection");
            }

            //3.Multiple Catch Blocks
            int[] arrayOfIntegers = new int[5] { 1, 2, 3, 4, 5 };
            try
            {
                Console.Write($"Enter the index of the array (0-4): ");
                int index = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Enter the number to divide that number: ");
                int number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"{arrayOfIntegers[index]} divide by {number} is {arrayOfIntegers[index] / number}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Index out of range: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format exception: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Divide by zero exception: {ex.Message}");
            }

            //4. Custom Exceptions
            try
            {
                BankAccount myBankAccount = new BankAccount(500);
                myBankAccount.withdraw(700);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //5. The "Rethrow" Pattern
            try
            {
                ProcessData("abc");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }


        }

        public static int ProcessData(string myData)
        {
            try
            {
                return Int32.Parse(myData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log: Error occurred in processData.");
                throw new Exception("Final Catch: The application has safely stopped.", ex);
            }
        }
    }
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
    class BankAccount
    {
        public double InitialSaving { get; init; }
        public double TotalSaving { get; set; }
        public BankAccount(double initialSaving)
        {
            this.InitialSaving = initialSaving;
            this.TotalSaving = initialSaving;
        }

        public void Deposit(double amount)
        {
            this.TotalSaving += amount;
        }

        public double withdraw(double amount)
        {
            if (amount > TotalSaving)
                throw new InsufficientFundsException($"The saving in the account just got {TotalSaving} which is insufficient for withdraw {amount}");
            else
            {
                this.TotalSaving -= amount; ;
                return amount;
            }

        }

    }
}
