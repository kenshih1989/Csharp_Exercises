using MathUtilities;
using LUI = LegacyUI;
using MUI = ModernUI;
namespace Namespaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basics: Creating and Using a Namespace
            //Comment out the using MathUtilities; on line#1 for this exercise purpose
            MathUtilities.Calculator myCalculator = new MathUtilities.Calculator();
            Console.WriteLine($"The summation of 4 and 5 is {myCalculator.Add(4, 5)}");

            //2. The using Directive
            Calculator anotherCalculator = new Calculator();
            Console.WriteLine($"The summation of 3 and 7 is {anotherCalculator.Add(3, 7)}");

            //4. Resolving Naming Conflicts (Aliases)
            MUI.Button modernBtn = new MUI.Button();
            LUI.Button legacyBtn = new LUI.Button();
            Console.WriteLine($"The type of modernBtn is {modernBtn.GetType()}");
            Console.WriteLine($"The type of legacyBtn is {legacyBtn.GetType()}");

            //5. Global Using Directives
            List<int> myIntegerList = new List<int>();

        }
    }
}

namespace MathUtilities
{
    class Calculator
    {
        public int Add(int x, int y) => x + y;
    }
}

//3. Nested Namespaces
namespace Corporate
{
    namespace HR
    {
        class Employee
        {
            public string Name;
            public Employee(string name)
            {
                Name = name;
            }
        }
    }

    namespace Accounting
    {
        public class Payroll
        {
            public void ProcessEntry()
            {
                Corporate.HR.Employee emp1 = new Corporate.HR.Employee("John");

                HR.Employee emp2 = new HR.Employee("Jane");
            }
        }
    }
}

namespace LegacyUI
{
    class Button
    {

    }
}

namespace ModernUI
{
    class Button
    {

    }
}
