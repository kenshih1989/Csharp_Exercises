using System.ComponentModel.Design;

namespace Yield_Return_and_Yield_Break
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part 1: yield return
            //1. The Even Number Stream
            foreach (var item in GetEvens(8))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //2. The Name Formatter
            List<string> list = new List<string>() { "Alice", "Bob", "Catherine" };
            foreach (var item in CapitalizeName(list))
            {
                Console.WriteLine($"{item} ");
            }

            //3. The Infinite Counter
            int counter = 0;
            foreach (var item in InfiniteCounter())
            {
                Console.Write($"{item} ");
                counter++;
                if (counter == 10)
                    break;
            }

            Console.WriteLine();

            //Part2: yield break
            //4. The Early Exit Filter
            List<string> list2 = new List<string>() { "Apple", "Banana", "END", "Cherry" };
            foreach (var item in ExitFilter(list2))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //5. The Range Guard
            List<int> positives = new List<int>() { 4, 3, 2, 7, 5, 9, 6, 8 };
            List<int> numbers = new List<int>() { 4, 3, -2, 7, 5, 9, 6, 8 };
            foreach (var item in TakeFirstN(positives, 4))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            foreach (var item in TakeFirstN(numbers, 4))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //6. The Password Validator
            string myPassword1 = "Syx$5dosTa";
            string myPassword2 = "Pass word";
            foreach (var item in PasswordReader(myPassword1))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            foreach (var item in PasswordReader(myPassword2))
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            //Bonus: Database Reader (Try-Finally)
            counter = 0;
            foreach (var item in ReadDatabaseRecords())
            {
                Console.Write($"{item} ");
                counter++;
                if (counter == 2)
                    break;
            }


        }

        static IEnumerable<int> GetEvens(int max)
        {
            for (int i = 0; i <= max; i++)
                if (i % 2 == 0)
                    yield return i;
        }

        static IEnumerable<string> CapitalizeName(List<string> names)
        {
            foreach (var item in names)
                yield return item.ToUpper() + "!";
        }

        static IEnumerable<int> InfiniteCounter()
        {
            int i = 0;
            while (true)
            {
                i++;
                yield return i;
            }
        }

        static IEnumerable<string> ExitFilter(List<string> fruits)
        {
            foreach (var f in fruits)
            {
                if (f == "END")
                    yield break;
                else
                    yield return f;
            }
        }

        static IEnumerable<int> TakeFirstN(List<int> numbers, int n)
        {
            int counter = 0;
            foreach (int number in numbers)
            {
                if (number < 0 || n == counter)
                    yield break;

                yield return number;
                counter++;
            }
        }

        static IEnumerable<char> PasswordReader(string password)
        {
            foreach (char f in password)
            {
                if (char.IsWhiteSpace(f))
                {
                    Console.WriteLine("\n[Iterator Error]: Spaces are not allowed!");
                    yield break;
                }

                yield return f;
            }
        }

        static IEnumerable<string> ReadDatabaseRecords()
        {
            Console.WriteLine("--- Database Connection Opened ---");
            try
            {
                for (int i = 1; i <= 4; i++)
                {
                    yield return $"Record {i}";
                }
            }
            finally
            {
                // This is the magic part! 
                // It runs even if the caller uses 'break' or 'yield break'.
                Console.WriteLine("--- Database Connection Closed Safely ---");
            }
        }
    }
}
