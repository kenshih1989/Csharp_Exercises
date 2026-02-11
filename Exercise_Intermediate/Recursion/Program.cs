namespace Recursion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Classic Factorial Function
            Console.WriteLine(Program.Factorial(5)); // Output: 120

            //2. Sum of Digits
            Console.WriteLine(Program.SumOfDigits(1234)); // Output: 10

            //3. String Reversal
            Console.WriteLine(Program.ReverseString("hello")); // Output: "olleh"

            //4. Fibonacci Sequence
            Console.WriteLine(Program.Fibonacci(6)); // Output: 8

            //5. Power Function
            Console.WriteLine(Program.Power(2,3)); // Output: 8

        }
        static int Factorial(int n)
        {
            if (n <= 1)
                return 1;
            return n * Factorial(n - 1);
        }

        static int SumOfDigits(int n)
        {
            if (n == 0)
                return 0;
            return (n % 10) + SumOfDigits(n / 10);
        }

        static string ReverseString(string str)
        {
            if (str.Length <= 1)
                return str;
            return str[str.Length-1] + ReverseString(str.Substring(0, str.Length - 1));
        }

        static int Fibonacci(int n)
        {
            if (n <= 1)
                return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static double Power(double baseNum, int exponent)
        {
            if (exponent == 0)
                return 1;
            return baseNum * Power(baseNum, exponent - 1);
        }


    }
}
