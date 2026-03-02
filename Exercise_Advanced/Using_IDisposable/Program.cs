using System;
using System.Data.Common;
using System.Data.SqlClient;
using static Using_IDisposable.Program;

namespace Using_IDisposable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The using Declaration (The Basics)
            using var logger = new LocalLogger();
            logger.Write("My log message");
            // logger is disposed automatically at the end of Main()

            //2. The using Block (Scope Control)
            using (var processor = new ImageProcessor())
            {
                processor.Process();
            }

            //3. Implementing IDisposable in a Wrapper
            using (var dbWrapper = new DatabaseWrapper("Server=myServer;Database=myDB;User Id=myUser;Password=myPass;"))
            {
                // Perform database operations here
                Console.WriteLine("Executing Database Commands...");
            }

            //4. Preventing the "ObjectDisposedException"
            var calculator = new Calculator();
            int result = calculator.Calculate(5, 10);
            Console.WriteLine($"Calculation Result: {result}");
            calculator.Dispose();
            Console.WriteLine("Attempting to use Calculator after disposal...");
            //Console.WriteLine($"Calculation Result: {calculator.Calculate(5, 10)}"); // This will throw an exception

            // This will not throw an exception as calculator instance recreated.
            // Not recomended as it can lead to resource leaks if not handled properly.
            Console.WriteLine($"Calculation Result: {new Calculator().Calculate(5, 10)}"); 
            

            //5. The Full Dispose Pattern (Advanced)
            using (var resource = new ComplexResource())
            {
                // Use the complex resource here
                Console.WriteLine("Using Complex Resource...");
            }
        }

        public class LocalLogger : IDisposable
        {
            public void Write(string shortcut) => Console.WriteLine($"Writing: {shortcut}");
            public void Dispose() => Console.WriteLine("File Stream Closed.");
        }


        public class ImageProcessor : IDisposable
        {
            public void Process() => Console.WriteLine("Processing Pixels...");
            public void Dispose() => Console.WriteLine("Graphics Memory Released.");
        }

        public class DatabaseWrapper : IDisposable
        {

            public DatabaseWrapper(string connectionString)
            {
                Console.WriteLine("Opening Database Connection...");
            }

            public void Dispose()
            {
                // Best practice: check for null and dispose the internal resource
                //_connection?.Dispose();
                Console.WriteLine("Closing Database Connection...");
            }
        }

        public class Calculator : IDisposable
        {
            private bool _isDisposed = false;

            public int Calculate(int a, int b)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(Calculator));
                return a + b;
            }

            public void Dispose()
            {
                _isDisposed = true;
            }
        }

        public class ComplexResource : IDisposable
        {
            private bool _disposed = false;

            // Managed resource
            private System.ComponentModel.Component _component = new System.ComponentModel.Component();

            // Implement the Finalizer (Destructor)
            ~ComplexResource()
            {
                //Only clean up unmanaged resources here
                Dispose(false);
            }

            public void Dispose()
            {
                // Call Dispose(true) and suppress finalization
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed) return;

                if (disposing)
                {
                    // Clean up managed resources
                    _component.Dispose();
                    Console.WriteLine("Managed resources cleaned up.");
                }

                // Clean up unmanaged resources (if any)
                Console.WriteLine("Unmanaged resources cleaned up.");
                _disposed = true;
            }
        }
    }
}
