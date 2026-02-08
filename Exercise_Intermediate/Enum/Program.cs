using System;
using System.Security.Cryptography.X509Certificates;

namespace Enum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Traffic Light System
            string input = "Orange";
            if (!System.Enum.TryParse<TrafficLight>(input, out TrafficLight testedLight))
                Console.WriteLine($"Invalid traffic light color: {input}");

            TrafficLight currentLight = TrafficLight.Yellow;
            switch (currentLight)
            {
                case TrafficLight.Red:
                    Console.WriteLine("Stop");
                    break;
                case TrafficLight.Yellow:
                    Console.WriteLine("Caution");
                    break;
                case TrafficLight.Green:
                    Console.WriteLine("Go");
                    break;
                default:
                    Console.WriteLine("Invalid Light State");
                    break;
            }

            //Note: since the GetTrafficLightAction method is not static, we need to create an instance of Program to call it.
            Program program = new Program();
            Console.WriteLine($"Current Light: {currentLight}, Action: {program.GetTrafficLightAction(currentLight)}");

            //2. Enums with custom integer values
            Console.Write("Please enter the HttpError integer:");
            if (int.TryParse(Console.ReadLine(), out int errorCode) && System.Enum.IsDefined(typeof(HttpError), errorCode))
            {
                HttpError error = (HttpError)errorCode;
                Console.WriteLine($"The HttpError is: {error}");
            }
            else
            {
                Console.WriteLine("Invalid HttpError code.");

            }

            //3. Bitwise Flags: File Permissions
            Permission myPermission = new Permission();
            myPermission = Permission.Read | Permission.Delete | Permission.Execute;
            Console.WriteLine($"Current permissions: {myPermission}");
            if (myPermission.HasFlag(Permission.Write))
            {
                Console.WriteLine("You have write permission.");
            }
            else
            {
                Console.WriteLine("You do not have write permission.");
            }

            //4. Parsing String to Enum
            Console.Write("Please enter the Difficulty level (Easy, Medium, Hard): ");
            string difficultyInput = Console.ReadLine();
            if (System.Enum.TryParse<Difficulty>(difficultyInput, true, out Difficulty difficulty))
            {
                Console.WriteLine($"You selected difficulty: {difficulty}");
            }
            else
            {
                Console.WriteLine("Invalid difficulty level.");
            }

            //5. Iterating and Descriptions
            Console.WriteLine("Days of the Week:");
            foreach (DayOfWeek day in System.Enum.GetValues(typeof(DayOfWeek)))
            {
                int value = (int)day;
                string note = (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday) ? " (Weekend)" : "";

                Console.WriteLine($"{value}: {day} {note}");
            }

        }

        public enum TrafficLight
        {
            Red,
            Yellow,
            Green
        }

        public string GetTrafficLightAction(TrafficLight light)
        {
            string message = light switch
            {
                TrafficLight.Red => "Stop",
                TrafficLight.Yellow => "Caution",
                TrafficLight.Green => "Go",
                _ => "Invalid Light State"
            };

            return message;
        }

        public enum HttpError
        {
            BadRequest = 400,
            Unauthorized = 401,
            NotFound = 404,
            InternalServerError = 500
        }

        [Flags] // Indicate that this enum can be treated as a bit field
        public enum Permission
        {
            None = 0,
            Read = 1,
            Write = 2,
            Execute = 4,
            Delete = 8
        }

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        public enum DayOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
    }
}
