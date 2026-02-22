using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Metadata Explorer
            SecretAgent secretAgent = new SecretAgent("007");
            AnalyzeClass(secretAgent);

            //2. The Private Detective
            BankVault bankVault = new BankVault();
            Type type = bankVault.GetType();

            var field = type.GetField("_pinCode", BindingFlags.NonPublic | BindingFlags.Instance);

            // Get the value from the specific instance
            Console.WriteLine($"Current pin code is: {field.GetValue(bankVault)}");

            // Change the pin code.
            field.SetValue(bankVault, "9999");
            Console.WriteLine($"Pin code is updated to: {field.GetValue(bankVault)}");

            // Access the pin code through the instance
            Console.WriteLine($"The pin code value from instance: {bankVault.getPinCode()}");

            //3. The Dynamic Factory
            // Ask user to enter English or Spanish
            //Console.Write("Enter one of the language (English/Spanish): ");
            //string userInput = Console.ReadLine();
            string userInput = "English"; //Simulate the user input

            // Construct the Namespace.Classname
            string className = String.Concat("Reflection.", userInput, "Greeter");

            // Get the type
            type = Type.GetType(className);
            Console.WriteLine($"Class Name: {type.Name}");

            // Create an instance dynamically
            object agentInstance = Activator.CreateInstance(type);

            // Find and Invoke the "sayHello" method at runtime
            MethodInfo method = type.GetMethod("sayHello");
            method.Invoke(agentInstance, null);

            //4. Universal Property Cloner
            Hero thor = new Hero { Name = "Thor", Level = 100, Power = "Lightning" };
            Hero backupHero = new Hero();

            Console.WriteLine($"Before Clone: {backupHero.Name ?? "Empty"}");

            CloneProperties(thor, backupHero);

            Console.WriteLine($"After Clone: {backupHero.Name} - Level {backupHero.Level} - Power {backupHero.Power}");

            //5: The Method Runner(With Parameters)
            object[] objects = new object[] { 10, 20 };
            object[] objects2 = new object[] { "10", "20" };

            try
            {
                //Get the type
                type = typeof(Calculator);

                // Create an instance dynamically
                object calculatorInstance = Activator.CreateInstance(type);

                // Find and Invoke the "add" method at runtime
                MethodInfo calculatorMethod = type.GetMethod("add");

                // Proactive Validation - avoid run into catch statement
                // Get the parameters of the method
                ParameterInfo[] parameters = calculatorMethod.GetParameters();
                bool isValid = true;

                // Check the parameter count
                if (parameters.Length != objects.Length)
                {
                    Console.WriteLine($"The signature method only accept {parameters.Length} parameters, but you passed {objects.Length} parameters");
                    isValid = false;
                }
                else
                {
                    // Loop through to check each parameter's type
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (objects[i].GetType() != parameters[i].ParameterType)
                        {
                            Console.WriteLine($"Argument {i} should be {parameters[i].ParameterType.Name}, but you passed {objects[i].GetType().Name}");
                            isValid = false;
                        }
                    }
                }

                // Invoke the method only if the parameters types are correct
                if (isValid)
                {
                    Console.WriteLine($"The summation result is :{calculatorMethod.Invoke(calculatorInstance, objects)}");
                    //Console.WriteLine($"The summation result is :{calculatorMethod.Invoke(calculatorInstance, objects2)}");
                }

            }
            catch (TargetInvocationException ex)
            {
                // This catches errors that happen INSIDE the called method
                Console.WriteLine($"Error inside the method: {ex.InnerException?.Message}");
            }
            catch (ArgumentException ex)
            {
                // This catches the "Parameter Mismatch" error you are simulating
                Console.WriteLine("Type Mismatch: The parameters provided do not match the method signature.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }



        }

        static void AnalyzeClass(object obj)
        {
            // 1. Get the Type information
            Type type = obj.GetType();
            // Print full name and namespace of the class
            Console.WriteLine($"Full Name: {type.FullName}");
            Console.WriteLine($"Namespace: {type.Namespace}");

            // 2. List all Properties
            Console.WriteLine("Properties:");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");
            }

            //3. List all the public methods
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"Public method: {method.ReturnType.Name} {method.Name}");
            }

        }

        static void CloneProperties<T>(T source, T target)
        {
            // 1. Get the Type of T
            Type type = typeof(T);

            // 2. Get all Properties
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // 3. Loop through them:
            foreach (PropertyInfo prop in properties)
            {
                // Check if prop.CanRead and prop.CanWrite
                if (prop.CanRead && prop.CanWrite)
                {
                    // Use prop.GetValue and prop.SetValue
                    prop.SetValue(target, prop.GetValue(source, null));
                }
            }

        }

    }

    class SecretAgent
    {
        public string CodeName { get; set; }

        public SecretAgent(string name)
        {
            CodeName = name;
        }

        public void MissionStatus(string location)
        {
            Console.WriteLine($"Agent {CodeName} is currently active in {location}.");
        }
    }

    class BankVault
    {
        private string _pinCode = "1234";

        public string getPinCode()
        {
            return this._pinCode;
        }
    }

    class EnglishGreeter
    {
        public void sayHello()
        {
            Console.WriteLine("Hello in English");
        }
    }

    class SpanishGreeter
    {
        public void sayHello()
        {
            Console.WriteLine("Hello in Spanish");
        }
    }

    public class Hero
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Power { get; set; }
    }

    class Calculator
    {
        public int add(int a, int b) => a + b;
    }

}
