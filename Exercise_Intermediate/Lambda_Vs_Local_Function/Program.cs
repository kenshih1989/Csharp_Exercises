using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lambda_Vs_Local_Function
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basics of Syntax
            // Lambda 
            Func<int, int, int> multiply = (a, b) => a * b;
            Console.WriteLine(multiply(5, 5));

            // Local function
            Console.WriteLine($"The mutiply value is: {Multiply(5, 5)}");
            int Multiply(int a, int b) => a * b;


            //2. The Recursion Challenge
            Func<int, int> calculateFactorial = null;
            calculateFactorial = (n) => n == 0 ? 1 : n * calculateFactorial(n - 1);
            Console.WriteLine($"Value of lambda expression: {calculateFactorial(5)}");

            Console.WriteLine($"Value of local function: {CalculateFactorial(5)}");
            int CalculateFactorial(int n)
            {
                if (n == 0)
                {
                    return 1;
                }
                else
                {
                    return n * calculateFactorial(n - 1);
                }
            }

            //3. Performance & Variable Capture
            Counter();

            void Counter()
            {
                int count = 0;
                Action lambdaCounter = () =>
                {
                    count++;
                    Console.WriteLine($"Lambda: {count}");
                };

                void localCounter()
                {
                    count++;
                    Console.WriteLine($"local Counter: {count}");
                }
                lambdaCounter();
                localCounter();
                //We got to pass the count by reference or value if we put the local function as static

            }

            //4. Ref and Out Parameters --> Lamda expression does not work on those parameters
            TestOutParameters();
            void TestOutParameters()
            {
                // 1. LOCAL FUNCTION (Works perfectly)
                bool TryDouble(string input, out int doubledValue)
                {
                    if (int.TryParse(input, out int parsed))
                    {
                        doubledValue = parsed * 2;
                        return true;
                    }
                    doubledValue = 0;
                    return false;
                }

                // Calling it
                if (TryDouble("21", out int result))
                {
                    Console.WriteLine($"Success! Doubled: {result}"); // Output: 42
                }
            }

            //5. The "Define After Use" Test

            //call both method here --> "hoisting" (calling it before the definition)

            Console.WriteLine(Sum(3, 4));

            Func<int, int, int> sum = (a, b) => a + b; //Lambda definition - treated as variable
            int Sum(int a, int b) => a + b; //Local function definition - treated as method

            Console.WriteLine(sum(2, 3));

            //Which one allows hoisting?
            //The Local Function allows hoisting.You can call it at the very first line of your method even if the implementation is at the very last line.

            //Why is this useful for "Main Logic" ?
            //In professional coding, we follow the "Step-Down Rule"(or the Newspaper Metaphor).
            //You want the most important, high - level logic to be at the top of the file so a reader understands what the code does immediately.

            //If you use Lambdas, you are forced to clutter the top of your method with all the "how-to" helper logic
            //before you can actually execute the main task.
            //With Local Functions, you can keep the "Executive Summary" at the top and hide the "Gory Details" at the bottom.



        }
    }
}
