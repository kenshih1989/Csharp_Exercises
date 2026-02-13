using System.Runtime.InteropServices;

namespace Interfaces
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basics (The Contract)
            Car myCar = new Car();
            Bicycle myBicycle = new Bicycle();
            myCar.Move();
            myBicycle.Move();
            myCar.Stop();
            myBicycle.Stop();


            //2. Multiple Interface Implementation
            NotePad myNotePad = new NotePad();
            ReadOnlyDocument readOnlyDocument = new ReadOnlyDocument();
            myNotePad.Write("Hello, World!");
            // This line will cause a compile-time error: there interface implemented by ReadOnlyDocument does not have a Write method.
            //readOnlyDocument.Write("Hello, World!");

            //3. Explicit Interface Implementation
            MediaPlayer mediaPlayer = new MediaPlayer();
            ((IPlayer)mediaPlayer).Play();
            ((IFilePlayer)mediaPlayer).Play();

            //4. Interface Inheritance
            HumanWorker worker = new HumanWorker();
            worker.Learn();
            worker.Work();

            //5. Interface Properties and Logic
            IShape[] shapes = new IShape[]
            {
                new Circle(5),
                new Rectangle(4, 6)
            };
            double totalArea = CalculateTotalArea(shapes);
            Console.WriteLine($"Total Area of all shapes: {totalArea}");

        }

        private static double CalculateTotalArea(IShape[] shapes)
        {
            double total = 0;
            foreach (var shape in shapes)
            {
                total += shape.Area;
            }
            return total;
        }
    }

    interface IVehicle
    {
        void Move();
        void Stop();
    }

    class Car : IVehicle
    {
        public void Move()
        {
            Console.WriteLine("The car is driving on the road.");
        }
        public void Stop()
        {
            Console.WriteLine("The car has stopped.");
        }
    }

    class Bicycle : IVehicle
    {
        public void Move()
        {
            Console.WriteLine("The bicycle is pedaling on the path.");
        }
        public void Stop()
        {
            Console.WriteLine("The bicycle has stopped.");
        }
    }

    interface IReadable
    {
        void Read();
    }

    interface IWriteable
    {
        void Write(string text);
    }

    class NotePad : IReadable, IWriteable
    {
        public void Read()
        {
            Console.WriteLine("Reading from the notepad...");
        }
        public void Write(string text)
        {
            Console.WriteLine($"Writing to the notepad: {text}");
        }
    }
    class ReadOnlyDocument : IReadable
    {
        public void Read()
        {
            Console.WriteLine("Reading the document...");
        }
    }

    interface IPlayer
    {
        void Play();
    }

    interface IFilePlayer
    {
        void Play();
    }
    class MediaPlayer : IPlayer, IFilePlayer
    {
        void IPlayer.Play()
        {
            Console.WriteLine("Playing music...");
        }
        void IFilePlayer.Play()
        {
            Console.WriteLine("Executing file...");
        }
    }

    interface IWorkable
    {
        void Work();
    }

    interface ISmartWorker : IWorkable
    {
        void Learn();
    }

    class HumanWorker : ISmartWorker
    {
        public void Work()
        {
            Console.WriteLine("Human is working.");
        }
        public void Learn()
        {
            Console.WriteLine("Human is learning new skills.");
        }
    }

    interface IShape
    {
        double Area { get; }
    }

    class Circle: IShape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            Radius = radius;
        }
        public double Area
        {
            get { return Math.PI * Radius * Radius; }
        }
    }

    class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public double Area
        {
            get { return Width * Height; }
        }
    }

}
