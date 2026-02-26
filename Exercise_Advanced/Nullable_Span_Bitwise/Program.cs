using System.Diagnostics;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;

namespace Nullable_Span_Bitwise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Nullable Exercises
            //1. The Basic Guard
            string? input1 = null;
            string input2 = "Simple";
            Console.WriteLine();
            Console.WriteLine($"The length of the {input1} is : {PrintLength(input1)}");
            Console.WriteLine($"The length of the {input2} is : {PrintLength(input2)}");

            //2. The Default Value
            double temperature1 = 25;
            double? temperature2 = null;
            Console.WriteLine($"The temperature of the {temperature1} is : {ShowTemperature(temperature1)}");
            Console.WriteLine($"The temperature of the {temperature2} is : {ShowTemperature(temperature2)}");

            //3. Nullable Logic
            bool? decision = null;
            string result = decision switch
            {
                true => "Yes",
                false => "No",
                _ => "Maybe" // Handles null
            };
            Console.WriteLine($"Decision: {result}");

            //4. The Assigment Shortcut
            List<string> list = null;
            list ??= new List<string>();
            list.Add("input1");
            Console.WriteLine($"Total length of the list is: {list.Count}");

            //5. Refactoring Challenge
            Company company = new Company();
            company ??= new Company();
            company.Dept = null;
            company.Dept ??= new Department();
            company.Dept.Mgr = null;
            company.Dept.Mgr ??= new Manager();
            company.Dept.Mgr.Name = "John";

            Console.WriteLine($"The manager's name is: {company?.Dept?.Mgr?.Name}");

            //Span<T> and Memory Efficiency
            //1. The Slice
            int[] integerArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Span<int> integerSpan = new Span<int>(integerArray, 3, 4);
            Console.WriteLine($"The integerSpan got {integerSpan.Length} elements");
            Console.Write($"The current value in span: ");
            foreach (var integer in integerSpan)
            {
                Console.Write($"{integer.ToString()} ");
            }
            Console.WriteLine();
            Console.Write($"The current value in Array: ");
            foreach (var integer in integerArray)
            {
                Console.Write($"{integer.ToString()} ");
            }
            Console.WriteLine();

            integerSpan.Replace(5, 50); //Replace 5 with 50
            Console.Write($"The current value in Array after replacing 5 with 50 in span: ");
            foreach (var integer in integerArray)
            {
                Console.Write($"{integer.ToString()} ");
            }
            Console.WriteLine();

            //2. Stack Allocation
            Span<int> squaredSpan = stackalloc int[5]; // Allocated on the stack!
            for (int i = 0; i < squaredSpan.Length; i++)
            {
                squaredSpan[i] = i * i;
            }
            //Display the values in squaredSpan
            Console.Write("Stack Allocated Span: ");
            foreach (var val in squaredSpan) Console.Write($"{val} ");
            Console.WriteLine();

            //3. String Parsing (ReadOnlySpan)
            string dateString = "2026-02-26";
            ReadOnlySpan<char> roSpan = dateString;
            int year, month, day;
            year = Int32.Parse(roSpan.Slice(0, 4));
            month = Int32.Parse(roSpan.Slice(5, 2));
            day = Int32.Parse(roSpan.Slice(8, 2));
            Console.WriteLine($"Year: {year} Month: {month} Day:{day}");

            //4, The Searcher
            ReadOnlySpan<int> roIntSpan = integerArray;
            int target = 8;
            Console.WriteLine($"The index of the {target} is {FindIndex(roIntSpan.Slice(5, 3), target)}");

            //5. Performance Comparison
            int[] integerArray2 = new int[] { 5, 9, 3, 7, 2, 8, 6, 1, 5, 2 };
            int arraySum = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < integerArray2.Length; i++)
            {
                arraySum = i % 2 == 1 ? arraySum + integerArray2[i] : arraySum;
            }
            stopwatch.Stop();
            Console.WriteLine($"Execution Time in Ticks: {stopwatch.ElapsedTicks}");
            Span<int> integerSpan2 = integerArray2;
            int spanSum = 0;
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (int i = 0; i < integerSpan2.Length; i++)
            {
                spanSum = i % 2 == 1 ? spanSum + integerSpan2[i] : spanSum;
            }
            stopwatch2.Stop();
            Console.WriteLine($"Execution Time in Ticks: {stopwatch2.ElapsedTicks}");

            //Bitwise Operators
            //1. Even or Odd
            int a = 17;
            string result2 = (a & 1) == 0 ? "Even" : "Odd";
            Console.WriteLine($"{a} is {result2}");

            //2. The Toggle
            int value = 10; //1010 (8+0+2+0)
            int mask = 4; // This targets the 3rd bit: 0100 (0+4+0+0)

            // First toggle (Off to On)
            value = value ^ mask; //1010 ^ 0100 --> 1110 (8+4+2+0)
            Console.WriteLine($"After first toggle: {value}"); // Output: 14

            // Second toggle (On to Off)
            value = value ^ mask; // 1110 ^ 0100 --> 1010 (8+0+2+0)
            Console.WriteLine($"After second toggle: {value}"); // Output: 10

            //3. Bit Packing
            int high = 5; //0101
            int low = 2; //0010

            //Combine into 1 single 8-bit byte: shift high to the left by 4, combine with OR on low
            int single = (high << 4 | low); //0101 0000 | 0000 0010 --> 0101 0010 (64+16+2) 82
            Console.WriteLine($"The value after combining {nameof(high)} and {nameof(low)} is {single}");

            //unpack to get the high value (XOR and then shift to right by 4);
            int revertHigh = single >> 4; //0101 0010 >> 4 --> 0101 (4+1)

            //unpack to get the low value (clear the high bit with AND by using mask 15 (1111))
            int revertLow = single & 15; //0101 0010 & 0000 1111 --> 0000 0010 (2)

            Console.WriteLine($"Are {nameof(high)} equal to {nameof(revertHigh)}: {high.Equals(revertHigh)}");
            Console.WriteLine($"Are {nameof(low)} equal to {nameof(revertLow)}: {low.Equals(revertLow)}");

            //4. Power of Two
            int n = 8; //1000  n-1=7 0111
            int n2 = 15; //1111 n2-1=14 1110
            Console.WriteLine($"Is {n} is power of 2: {IsPowerOfTwo(n)}");
            Console.WriteLine($"Is {n2} is power of 2: {IsPowerOfTwo(n2)}");


            //5. The Mask
            int myInt = 18; //0001 0010 (16+2)
            //Extract the last 4 bits with AND by using Mask: 15(1111)
            Console.WriteLine($"The last 4 bits for {myInt} is {myInt & 15}");
        }

        static string PrintLength(string? input)
        {
            return input?.Length.ToString() ?? "String is null";
        }

        static double ShowTemperature(double? input)
        {
            return input ?? 20.0;
        }

        static int FindIndex(ReadOnlySpan<int> data, int target)
        {
            return data.IndexOf(target);
        }

        static bool IsPowerOfTwo(int value)
        {
            //value = 0 is not power of 2
            if (value > 0)
                return (value & value - 1) == 0;

            return false;
        }
    }

    public class Manager { public string Name { get; set; } }
    public class Department { public Manager Mgr { get; set; } }
    public class Company { public Department Dept { get; set; } }
}
