namespace Arrays_and_Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Reverse Engineering
            int[] numbers = new int[5]{ 1, 2, 3, 4, 5 };
            int[] reversedNumbers = new int[numbers.Length];
            int temp = 0;
            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                reversedNumbers[temp] = numbers[i];
                temp++;
            }
            //Print the reversed array
            foreach (int num in reversedNumbers)
            {
                Console.WriteLine(num);
            }

            //2. Linear Search
            string[] colors = new string[] { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" };
            Console.Write("Enter a color to search: ");
            string inputColor = Console.ReadLine();
            bool found = false;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].ToUpper() == inputColor.ToUpper())
                {
                    Console.WriteLine($"Color found at index {i}.");
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Color not found .");
            }

            //3. The Even/Odd Split
            List<int> integers = new List<int>();
            Random rand = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                integers.Add(rand.Next(1, 101)); // Random integers between 1 and 100
            }

            Console.WriteLine("Generated Integers:");
            foreach (int num in integers)
            {
                Console.WriteLine(num);
            }

            //Count how many even and odd numbers
            int evenCount = 0;
            int oddCount = 0;
            foreach (int num in integers)
            {
                if (num % 2 == 0)
                {
                    evenCount++;
                }
                else
                {
                    oddCount++;
                }
            }
            Console.WriteLine($"Even numbers count: {evenCount}");
            Console.WriteLine($"Odd numbers count: {oddCount}");

            //Calculate and print the sum of even numbers
            int evenSum = 0;
            foreach (int num in integers)
            {
                if (num % 2 == 0)
                {
                    evenSum += num;
                }
                else
                {
                    oddCount++;
                }
            }
            Console.WriteLine($"Sum of even numbers: {evenSum}");

            //4. Find the Maximum and Minimum
            int[] sampleNumbers = {34,7,23,99,1,56,12};

            //Identifies the smallest and largest numbers in the array
            int max = sampleNumbers[0];
            int min = sampleNumbers[0];
            for (int i = 1; i < sampleNumbers.Length; i++)
            {
                if (sampleNumbers[i] > max)
                {
                    max = sampleNumbers[i];
                }
                if (sampleNumbers[i] < min)
                {
                    min = sampleNumbers[i];
                }
            }
            Console.WriteLine($"Maximum number: {max}");
            Console.WriteLine($"Minimum number: {min}");

            //5. The Grade Normalizer
            int[] grades = { 85, 92, 78, 97, 88 };
            int[] newGrades = new int[grades.Length];
            for (int i = 0; i < grades.Length; i++)
            {
                newGrades[i] = grades[i]+5;
                if (newGrades[i] > 100)
                {
                    newGrades[i] = 100;
                }
            }
            Console.WriteLine("Before Normalization:");
            foreach (int grade in grades)
            {
                Console.WriteLine(grade);
            }

            Console.WriteLine("After Normalization:");
            foreach (int grade in newGrades)
            {
                Console.WriteLine(grade);
            }
            

        }
    }
}
