namespace Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Classic FizzBuzz Problem
            for (int i = 1; i <= 50; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else
                {
                    Console.WriteLine(i);
                }
            }

            //2. The Investment Calculator
            double principal = 1000; // Initial investment
            double rate = 0.05; // Annual interest rate
            int numberOfYears = 0;
            while (principal < 2000)
            {
                principal += principal * rate;
                numberOfYears++;
            }
            Console.WriteLine($"It took {numberOfYears} years to double the investment.");

            //3. Number Guessing Game
            Random random = new Random();
            int numberToGuess = random.Next(1, 11); // Random number between 1 and 10
            int guess;
            do
            {
                Console.Write("Guess a number between 1 and 10: ");
                guess = int.Parse(Console.ReadLine());
                if (guess < numberToGuess)
                {
                    Console.WriteLine("Too low!");
                }
                else if (guess > numberToGuess)
                {
                    Console.WriteLine("Too high!");
                }
            } while (guess != numberToGuess);
            Console.WriteLine("Congratulations! You've guessed the number.");

            //4. The Pyramid Builder
            Console.Write("Enter the number of levels for the pyramid: ");
            int levels = int.Parse(Console.ReadLine());
            for (int i = 1; i <= levels; i++)
            {
                // Print stars
                for (int j = 0; j < i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Inverted Pyramid
            for (int i = levels - 1; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            //5. Factorial Finder
            Console.Write("Enter a number to calculate its factorial: ");
            int number = int.Parse(Console.ReadLine());
            int result = number;
            for (int i = number - 1; i >= 1; i--)
            {
                result *= i;
            }
            Console.WriteLine($"Factorial of {number} is: {result}");
        }
    }
}
