namespace Singleton_Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basic Eager Singleton
            // 1. Access the instance and set the AppName
            var config1 = ConfigurationManager.Instance;
            config1.AppName = "My Awesome App";

            // 2. Access the instance again via a different variable
            var config2 = ConfigurationManager.Instance;

            // 3. Verify they are the same
            Console.WriteLine($"Config 1 Name: {config1.AppName}");
            Console.WriteLine($"Config 2 Name: {config2.AppName}");

            // This should be True
            Console.WriteLine($"Are they the same instance? {Object.ReferenceEquals(config1, config2)}");

            //2. The Double-Check Lock (Thread Safety)
            // In a real app, you'd use Parallel.Invoke or Threads to test this
            Logger.Instance.Log("Application Started");
            Logger.Instance.Log("Doing work...");

            var logger2 = Logger.Instance;
            Console.WriteLine($"Are they the same? {Object.ReferenceEquals(Logger.Instance, logger2)}");

            //3. The "Static Constructor" Approach
            FileService.Instance.WriteToFile("1st line of message...");
            FileService.Instance.WriteToFile("2nd line of message...");

            //4. The Hardware Interface (Resource Management)
            PrinterSpooler.Instance.Print("DocumentA");
            PrinterSpooler.Instance.Print("DocumentB");
            PrinterSpooler.Instance.Print("DocumentC");
            Console.WriteLine($"Total job executed : {PrinterSpooler.Instance.JobCount}");

            //5. The Singleton Registry (Advanced)
            SessionManager.Instance.AddSession("UserName", "Administrator");
            SessionManager.Instance.AddSession("Password", "W4E3%gfd");
            Console.WriteLine($"The username for the session is {SessionManager.Instance.GetSession("UserName")}");
            Console.WriteLine($"The password for the session is {SessionManager.Instance.GetSession("Password")}");
            Console.WriteLine($"The data for the session is {SessionManager.Instance.GetSession("Data")}");


        }
    }
    public sealed class ConfigurationManager
    {
        // STEP 1: Create a private static field and initialize it immediately (Eager)
        private static readonly ConfigurationManager _instance = new ConfigurationManager();

        // STEP 2: Make the constructor private so no one can use 'new'
        private ConfigurationManager()
        {
            // Optional: Add a console message here to see when it's created
            Console.WriteLine("Creating the Configuration Manager instance");
        }

        // STEP 3: Create a public static property to return the instance
        public static ConfigurationManager Instance => _instance;

        // STEP 4: Add an instance property for testing
        public string AppName { get; set; }

    }

    public sealed class Logger
    {
        // STEP 1: Define the instance field (set to null initially)
        private static Logger _instance = null;

        // STEP 2: Define a synchronization object (the "padlock")
        private static readonly object _padlock = new object();

        // Private constructor
        private Logger()
        {
            Console.WriteLine("Logger instance created.");
        }

        // STEP 3: Implement the Instance property with Double-Check Locking
        public static Logger Instance
        {
            get
            {
                // FIRST CHECK: If instance exists, return it immediately (fast)
                if (_instance == null)
                {
                    // LOCK: Only one thread can enter this block at a time
                    lock (_padlock)
                    {
                        // SECOND CHECK: Now that we are safe, check again
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG]: {message}");
        }
    }

    public sealed class FileService
    {
        private static readonly FileService _instance;

        // STEP 1: Create a Static Constructor
        // Note: No access modifiers (public/private) allowed on static constructors!
        static FileService()
        {
            // STEP 2: Initialize the _instance here
            // This is a great place for complex setup logic (like checking if a folder exists)
            Console.WriteLine("Static Constructor triggered.");
            _instance = new FileService();
        }

        // STEP 3: Private Instance Constructor
        private FileService()
        {
            Console.WriteLine($"Inside the private constructor");
        }

        public static FileService Instance => _instance;

        public void WriteToFile(string message)
        {
            Console.WriteLine($"Writing to file: {message}");
        }
    }

    public sealed class PrinterSpooler
    {
        private static readonly PrinterSpooler _instance;
        private int _jobCount;

        // STEP 1: Create a Static Constructor
        // Note: No access modifiers (public/private) allowed on static constructors!
        static PrinterSpooler()
        {
            // STEP 2: Initialize the _instance here
            Console.WriteLine("Static Constructor triggered. Initializing the PrinterSpooler instance...");
            _instance = new PrinterSpooler();
        }

        // STEP 3: Private Instance Constructor
        private PrinterSpooler()
        {
            Console.WriteLine("PrinterSpooler instance created successfully");
            _jobCount = 0;
        }

        public static PrinterSpooler Instance => _instance;

        public void Print(string document)
        {
            Console.WriteLine($"Print {document}");
            _jobCount++;
        }

        public int JobCount => _jobCount;
    }


    public sealed class SessionManager
    {
        // STEP 1: Eagerly initialize the instance
        private static readonly SessionManager _instance = new SessionManager();

        // STEP 2: The "Vault" - A private readonly dictionary
        private readonly Dictionary<string, string> _sessionData;

        // Private Constructor
        private SessionManager()
        {
            _sessionData = new Dictionary<string, string>();
            Console.WriteLine("Session Data created!");
        }

        public static SessionManager Instance => _instance;

        // STEP 3: Add logic to store a session
        public void AddSession(string key, string value)
        {
            _sessionData[key] = value;
        }

        // STEP 4: Add logic to retrieve a session
        public string GetSession(string key)
        {
            // Hint: check if key exists first!
            return _sessionData.ContainsKey(key) ? _sessionData[key] : "Not Found";
        }
    }


}