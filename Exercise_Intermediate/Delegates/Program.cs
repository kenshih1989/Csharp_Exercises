namespace Delegates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Define and Invoke a Simple Delegate
            PrintLog(printMessage, "This is the 1st line of message.");
            PrintLog(printMessage, "This is the 2nd line of message.");
            PrintLog(printMessage, "This is 3rd line and so on...");

            //2.Implement an Action Timer
            SimpleTimer myTimer = new SimpleTimer();
            myTimer.Seconds = 2;
            myTimer.startTimer(myTimer.Seconds, myTimer.printTimeUp);

            //3.Use a Lambda as an Action
            ProcessData("This is my message",
                (msg) =>
                {
                    Console.WriteLine(msg.ToUpper());
                }
            );

            //4. Try Action Multicasting
            Action<String> multiLogger;
            multiLogger = LogToConsole;
            multiLogger += (msg) =>
            {
                Console.WriteLine($"[FILE SYSTEM]: Appending '{msg}' to log.txt...");
            };
            Console.WriteLine("---Start---");
            multiLogger("User Login!");
            Console.WriteLine("---End---");

            //5. Pass in a Func to Validate a Number
            List<int> numbers = new List<int> { 3, 10, 15, 20, 25, 30, 33 };
            Console.WriteLine("Original List: " + string.Join(", ", numbers));

            List<int> bigNumbers = FilterNumber(numbers, n => n > 10);
            Console.WriteLine("Big numbers List: " + string.Join(", ", bigNumbers));

            List<int> evenNumbers = FilterNumber(numbers, n => n % 2 == 0);
            Console.WriteLine("Even numbers List: " + string.Join(", ", evenNumbers));
        }


        public delegate void LogHandler(string message);

        static void printMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void PrintLog(LogHandler action, string message)
        {
            Console.WriteLine("Printing the log message...");
            // Invoke the delegate (the method passed in)
            action(message);
        }

        public static void ProcessData(string message,Action<string> action)
        {
            Console.WriteLine($"The original message: {message}");
            Console.Write("The uppercase message: ");
            action(message);
        }

        public static void LogToConsole(string message)
        {
            Console.WriteLine($"[Log To Console]: {message}");
        }

        public static List<int> FilterNumber(List<int> myList, Func<int, bool> validator)
        {


            List<int> result = new List<int>();

            foreach (int num in myList)
            {
                if (validator != null && validator(num))
                {
                    result.Add(num);
                }
            }

            return result;
        }
    
    }
    class SimpleTimer
    {
        public int Seconds;

        
        public void startTimer(int seconds, Action action)
        {
            Console.WriteLine("Starting the timer...");
            Thread.Sleep(seconds*1000);
            action();

        }

        public void printTimeUp()
        {
            Console.WriteLine($"Time Up after {this.Seconds} seconds");
        }


    }
}
