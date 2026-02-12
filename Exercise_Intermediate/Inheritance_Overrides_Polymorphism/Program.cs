using System.Security.Principal;

namespace Inheritance_Overrides_Polymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Inheritance: The "Is-A" relationship
            // The Smart Home Hierarchy
            SmartLight myLight = new SmartLight("Living Room Light", false, 75);
            myLight.SetBrightness(85);
            myLight.TurnOn();

            // The Employee Payroll
            Manager mgr = new Manager("Alice Johnson", "MGR123", 10);
            mgr.DisplayInfo();

            // The Vehicle Fleet
            Truck myTruck = new Truck("Ford", "F-150", 2.5);
            myTruck.StartEngine();
            myTruck.DisplayInfo();

            // 2. Method Overriding: Changing behavior in derived classes
            // The Sound of Music
            Instrument myGuitar = new Guitar();
            Instrument myDrum = new Drum();
            myGuitar.PlaySound();
            myDrum.PlaySound();

            // The Shape Area Calculator 
            Shape mySquare = new Square(4);
            Console.WriteLine($"Area of the square: {mySquare.CalculateArea()}");

            // The Messaging App
            Notification sms = new SMSNotification("Hello, this is a test SMS message. It is super long");
            sms.SendMessage();
            Notification email = new EmailNotification("My Email Subject", "Hello, this is a test Email message.");
            email.SendMessage();

            // 3. Polumorphism: The ability to treat objects of different types through a common interface
            // The Animal Shelter
            List<Animal> animals = new List<Animal>
            {
                new Dog(),
                new Cat(),
                new Bird()
            };

            foreach (var animal in animals)
            {
                animal.MakeSound();

            }

            // The Bank Account Processor
            List<BankAccount> accounts = new List<BankAccount>
            {
                new SavingsAccount(),
                new CheckingAccount()
            };
            BankAccount accountProcessor = new BankAccount();
            accountProcessor.ProcessAccounts(accounts);

            // The Drawing Canvas
            List<IDrawable> shapes = new List<IDrawable>
            {
                new Circle(3.0),
                new Triangle(5.0,8.5),
                new Rectangle(4.5,6.8)
            };
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
        class SmartDevice
        {
            public string DeviceName { get; set; }
            public bool IsOn { get; set; }
            public SmartDevice(string name, bool isOn)
            {
                DeviceName = name;
                IsOn = isOn;
            }
            public virtual void TurnOn()
            {
                Console.WriteLine($"{DeviceName} is now ON.");
            }
            public virtual void TurnOff()
            {
                Console.WriteLine($"{DeviceName} is now OFF.");
            }
        }

        class SmartLight : SmartDevice
        {
            private int Brightness { get; set; }
            public SmartLight(string name, bool isOn, int brightness) : base(name, isOn)
            {
                Brightness = brightness;
            }

            public void SetBrightness(int level)
            {
                Brightness = level;
                Console.WriteLine($"{DeviceName} brightness set to {Brightness}%.");
            }

            public override void TurnOn()
            {
                base.TurnOn();
                Console.WriteLine($"{DeviceName} current brightness is: {Brightness}%.");
            }
        }

        class Employee
        {
            protected string Name { get; set; }
            protected string ID { get; set; }
            public Employee(string name, string id)
            {
                Name = name;
                ID = id;
            }

        }

        class Manager : Employee
        {
            private int TeamSize { get; set; }
            public Manager(string name, string id, int teamSize) : base(name, id)
            {
                TeamSize = teamSize;
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"Manager Name: {Name}, ID: {ID}, Team Size: {TeamSize}");
            }
        }

        class Vehicle
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public Vehicle(string make, string model)
            {
                Make = make;
                Model = model;
            }
            public void StartEngine()
            {
                Console.WriteLine($"The engine of the {Make} {Model} is starting.");
            }
        }

        class Truck : Vehicle
        {
            public double CargoCapacity { get; set; }
            public Truck(string make, string model, double cargoCapacity) : base(make, model)
            {
                CargoCapacity = cargoCapacity;
            }
            public void DisplayInfo()
            {
                Console.WriteLine($"Truck Make: {Make}, Model: {Model}, Load Capacity: {CargoCapacity} tons");
            }



        }

        class Instrument
        {
            public virtual void PlaySound()
            {
                Console.WriteLine("Playing a generic instrument sound.");
            }
        }

        class Guitar : Instrument
        {
            public override void PlaySound()
            {
                Console.WriteLine("Strumming the guitar strings!");
            }
        }

        class Drum : Instrument
        {
            public override void PlaySound()
            {
                Console.WriteLine("Beating the drum rhythmically!");
            }
        }

        class Shape
        {
            public virtual double CalculateArea()
            {
                return 0;
            }
        }

        class Square : Shape
        {
            public double SideLength { get; set; }
            public Square(double sideLength)
            {
                SideLength = sideLength;
            }
            public override double CalculateArea()
            {
                return SideLength * SideLength;
            }
        }

        class Notification
        {
            private string _message = string.Empty;

            public virtual string Message
            {
                get => _message;
                set => _message = value;
            }
            public virtual void SendMessage()
            {
                Console.WriteLine($"Sending generic notification: {_message}");
            }
        }

        class SMSNotification : Notification
        {
            public const int characterLimit = 160;
            public override string Message
            {
                get => base.Message;
                set
                {
                    // Logic moved from constructor to here!
                    if (value.Length > characterLimit)
                    {
                        Console.WriteLine($"Warning: Truncating message to {characterLimit} chars.");
                        base.Message = value.Substring(0, characterLimit);
                    }
                    else
                    {
                        base.Message = value;
                    }
                }
            }
            public SMSNotification(string message)
            {
                    this.Message = message;
                
            }
            public override void SendMessage()
            {
                Console.WriteLine($"Sending SMS notification: {Message}");
            }
        }

        class EmailNotification : Notification
        {
            public string Subject { get; set; }

            public EmailNotification(string subject, string message)
            {
                this.Subject = subject;
                this.Message = message;
            }
            public override void SendMessage()
            {
                Console.WriteLine($"Subject: {Subject}");
                Console.WriteLine($"Sending email notification: {Message}");
            }
        }

        class Animal
        {
            public virtual void MakeSound()
            {
                Console.WriteLine("The animal makes a sound.");
            }
        }

        class Dog : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("The dog barks: Woof!");
            }
        }

        class Cat : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("The cat meows: Meow!");
            }
        }

        class Bird : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("The bird chirps: Tweet!");
            }
        }

        class BankAccount
        {
            public virtual void ApplyInterest()
            {
                Console.WriteLine("Applying interest on a generic account.");
            }

            public void ProcessAccounts(List<BankAccount> accounts)
            {
                foreach (var account in accounts)
                {
                    account.ApplyInterest();
                }
            }
        }

        class SavingsAccount : BankAccount
        {
            public override void ApplyInterest()
            {
                Console.WriteLine("Applying interest on a savings account at 4%.");
            }
        }

        class CheckingAccount : BankAccount
        {
            public override void ApplyInterest()
            {
                Console.WriteLine("Checking accounts do not earn interest.");
            }
        }

        class IDrawable
        {
            public virtual void Draw()
            {
                Console.WriteLine("Drawing a generic shape.");
            }
        }

        class Circle : IDrawable
        {
            public double Radius { get; set; }
            public Circle(double radius)
            {
                Radius = radius;
            }
            public override void Draw()
            {
                Console.WriteLine($"Drawing a circle with radius: {Radius}");
            }
        }

        class Triangle : IDrawable
        {
            public double Base { get; set; }
            public double Height { get; set; }
            public Triangle(double baseLength, double height)
            {
                Base = baseLength;
                Height = height;
            }
            public override void Draw()
            {
                Console.WriteLine($"Drawing a triangle with base: {Base} and height: {Height}");
            }
        }
        class Rectangle : IDrawable
        {
            public double Width { get; set; }
            public double Height { get; set; }
            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }
            public override void Draw()
            {
                Console.WriteLine($"Drawing a rectangle with width: {Width} and height: {Height}");
            }
        }
    }
}
