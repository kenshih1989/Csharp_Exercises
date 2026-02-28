using System.Collections;
using System.Diagnostics;

namespace Data_Boxing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basics
            int value = 101;
            object boxValue = value;
            int value2 = (int)boxValue;
            Console.WriteLine($"Value:{value}, Value2:{value2}");

            //2. The "Hidden" Boxing Trap
            int myInt = 10;
            double myDouble = 5.5;
            bool myBool = true;
            Console.WriteLine($"Values: {myInt} {myDouble} {myBool}");

            //3. The Invalid Cast Exception
            double val = 9.99;
            object boxDouble = val;
            int unboxInt;
            try
            {
                unboxInt=(int)boxDouble;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //4. Collection Performance Challenge
            ArrayList myArrayList = new ArrayList();
            List<int> myList = new List<int>();

            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                myArrayList.Add(i);
            }
            stopwatch.Stop();

            stopwatch2.Start();
            for (int i = 0; i < 10000; i++)
            {
                myList.Add(i);
            }
            stopwatch2.Stop();
            Console.WriteLine($"Time used by array list: {stopwatch.ElapsedTicks} ticks");
            Console.WriteLine($"Time used by generic list: {stopwatch2.ElapsedTicks} ticks");

            //5. Boxing with Interfaces
            Worker worker = new Worker(); // Lives on the STACK
            IWorkable iWorker = (IWorkable)worker; // BOXING occurs here!


        }
    }

    interface IWorkable
    {
        public void DoWork();
    }

    struct Worker : IWorkable
    {
        public void DoWork()
        {
            Console.WriteLine("Doing work...");
        }

    }
}
