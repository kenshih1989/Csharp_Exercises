namespace Multithreading
{
    internal class Program
    {
        static int counter = 0; // Shared counter
        static readonly object counterLock = new object(); // Lock object for synchronization
        static AutoResetEvent _waitForItem = new AutoResetEvent(false);
        static string _sharedData = "";

        static void Main(string[] args)
        {
            //1.The Basic "Ping-Pong"
            Thread pingThread = new Thread(writePing);
            Thread pongThread = new Thread(WritePong);

            pingThread.Start();
            pongThread.Start();

            pingThread.Join();
            pongThread.Join();

            //2.The Shared Counter(Race Condition)
            Parallel.Invoke(IncrementCounter, IncrementCounter, IncrementCounter, IncrementCounter, IncrementCounter);
            Console.WriteLine($"Final Counter Value: {counter}");

            //3.Background Worker with State
            int[] numbers = { 1, 2, 3, 4, 5 };
            ThreadWorker threadWorker = new ThreadWorker(numbers);
            Thread thread = new Thread(threadWorker.DoWork);
            Console.WriteLine("Starting background worker thread...");
            thread.Start();
            thread.Join(); // Wait for the thread to finish, without this line of code, the main thread will exit before the background worker finishes its work
            Console.WriteLine($"Result from background worker: {threadWorker.Result}");
            Console.WriteLine("Main thread is exiting.");

            //4.The Producer - Consumer(Manual Signaling)
            Thread Producer = new Thread(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine($"Producing item {i}");
                        Thread.Sleep(2000); // Simulate time taken to produce an item
                        _sharedData = $"Item {i}";
                        Console.WriteLine($"[Producer]: Created {_sharedData}");

                        _waitForItem.Set();// Flip the switch to Green
                    }
                });

            Thread Consumer = new Thread(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _waitForItem.WaitOne(); // Wait for the producer to signal that an item is produced
                        Console.WriteLine($"[Consumer]: Consumed {_sharedData}");
                    }
                });

            Producer.Start();
            Consumer.Start();

            //5. Thread Priority Race
            bool isRunning = true;
            long highCount = 0;
            long lowCount = 0;
            Thread high = new Thread(() => { while (isRunning) highCount++; });
            Thread low = new Thread(() => { while (isRunning) lowCount++; });

            high.Priority = ThreadPriority.Highest;
            low.Priority = ThreadPriority.Lowest;

            high.Start();
            low.Start();

            Thread.Sleep(3000); // Let them fight for 3 seconds
            isRunning = false; // Stop both

            high.Join();
            low.Join();

            Console.WriteLine($"High Priority Processed: {highCount:N0} increments");
            Console.WriteLine($"Low Priority Processed:  {lowCount:N0} increments");
            // You will see highCount is significantly larger than lowCount
        }

        static void writePing()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Ping");
                Thread.Sleep(500); // Sleep for 0.5 second
            }
        }

        static void WritePong()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Pong");
                Thread.Sleep(500); // Sleep for 0.5 second
            }
        }

        static void IncrementCounter()
        {
            lock (counterLock)
            {
                for (int i = 0; i < 100000; i++)
                {
                    counter++; // Increment the shared counter
                }
            }
        }
    }

    class ThreadWorker
    {
        private int[] Numbers { get; set; }
        public int Result { get; private set; }
        public ThreadWorker(int[] numbers)
        {
            Numbers = numbers;
        }
        public void DoWork()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is doing work.");
            foreach (var number in Numbers)
            {
                Result += number; // Simulate some work by summing the numbers
                Thread.Sleep(400); // Simulate work by sleeping for 0.4 second
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has finished work.");
        }
    }
}
