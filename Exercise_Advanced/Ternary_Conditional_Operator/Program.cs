namespace Ternary_Conditional_Operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Temperature Check
            int temp = 25;
            string feel = temp >= 20 ? "warm" : "Cold";
            Console.WriteLine($"{feel}");

            //2. Discount Logic
            double totalPurchase = 120;
            double discountRate = totalPurchase > 100 ? 20 : 5;
            Console.WriteLine($"The discount rate is {discountRate}%");

            //3. Even or Odd?
            int num = 67;
            string answer = num % 2 == 0 ? "Even" : "Odd";
            Console.WriteLine($"{num} is an {answer} number");

            //4. User Access Level
            bool isAdmin = true;
            string accessMessage = isAdmin ? "Full Access Granted" : "Limited Access Granted";
            Console.WriteLine($"Access message: {accessMessage}");

            //5. The Max Finder
            int a = 6;
            int b = 4;
            int maxValue = a>b?a:b;
            Console.WriteLine($"The larger number is {maxValue}");

        }
    }
}
