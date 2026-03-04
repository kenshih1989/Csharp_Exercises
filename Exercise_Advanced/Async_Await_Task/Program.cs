using System.Diagnostics;

namespace Async_Await_Task
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //1. The Baisc "Hello Async"    
            Console.WriteLine("Cooking pasta...");
            await CookPasta();
            Console.WriteLine("Pasta is ready!");

            //2. The "Data Scraper" (Parallelism)
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Task task1 = GetStockPrice();
            Task task2 = GetWeather();
            Task task3 = GetNews();

            await Task.WhenAll(task1, task2, task3);
            stopwatch.Stop();
            Console.WriteLine("All data fetched!");
            Console.WriteLine($"Total time taken: {stopwatch.ElapsedMilliseconds} ms");

            //3. The "Race to the Finish"
            Task serverATask = ServerA();
            Task serverBTask = ServerB();

            Task firstFinished = await Task.WhenAny(serverATask, serverBTask);
            if (firstFinished == serverATask)
            {
                Console.WriteLine("Server A responded first!");
            }
            else
            {
                Console.WriteLine("Server B responded first!");
            }

            //4. The "Emergency Brake" (Cancellation)
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(2500)); ;
            try
            {
                await PerformLongGridSearch(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Timed out! The operation was cancelled.");
            }

            //5. The "Safe UI" (Error Handling)

            Task myTask1 = MyTask1();
            Task myTask2 = MyTask2();
            Task myTask3 = MyTask3();
            Task allTasks = Task.WhenAll(myTask1, myTask2, myTask3);
            try
            {
                await allTasks;
            }
            catch 
            {
                foreach (var inner in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine($"Found error: {inner.GetType().Name}");
                }
            }


        }

        static async Task CookPasta()
        {
            await Task.Run(() => Thread.Sleep(3000)); // Simulate time-consuming operation asynchronously
        }

        static async Task GetStockPrice()
        {
            Console.WriteLine("Fetching stock price...");
            await Task.Delay(2000); // Simulate a delay in fetching stock price
            Console.WriteLine("Stock price is $100");
        }

        static async Task GetWeather()
        {
            Console.WriteLine("Fetching weather data...");
            await Task.Delay(2000); // Simulate a delay in fetching weather data
            Console.WriteLine("The weather is sunny with a high of 25°C");
        }

        static async Task GetNews()
        {
            Console.WriteLine("Fetching news...");
            await Task.Delay(2000); // Simulate a delay in fetching news
            Console.WriteLine("Today's headline: Async/Await in C# is powerful!");
        }

        static async Task ServerA()
        {
            Console.WriteLine("Server A is processing...");
            await Task.Delay(3000); // Simulate a delay in processing
            Console.WriteLine("Server A has finished processing.");
        }
        static async Task ServerB()
        {
            Console.WriteLine("Server B is processing...");
            await Task.Delay(8000); // Simulate a delay in processing
            Console.WriteLine("Server B has finished processing.");
        }

        static async Task PerformLongGridSearch(CancellationToken cts)
        {
            Console.WriteLine("Performing Long Grid Search started ...");
            for (int i = 0; i < 100; i++)
            {
                if (cts.IsCancellationRequested)
                {
                    Console.WriteLine($"Current iteration number is :{i}");
                    return;
                }
                await Task.Delay(100);
            }
            Console.WriteLine("Complete Long Grid Search.");
        }

        static async Task MyTask1()
        {
            Console.WriteLine("Starting Task 1...");
            await Task.Delay(2000);
            Console.WriteLine("My Task 1 completed");
        }
        static async Task MyTask2()
        {
            Console.WriteLine("Starting Task 2...");
            await Task.Delay(1000);
            throw new InvalidOperationException("Invalid Operation occured");
            
        }
        static async Task MyTask3()
        {
            Console.WriteLine("Starting Task 3...");
            await Task.Delay(2000);
            throw new AccessViolationException("Access violation occured!");
            Console.WriteLine("My Task 3 completed");
        }
    }
}
