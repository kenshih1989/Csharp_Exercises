namespace Structs
{
    internal class Program
    {
        static double CalculateSum(in LargeData data) //in keyword make the data pass by reference byt makes the variables read-only
        {
            // data.Val1 = 10.0; // This would cause a compile error because 'in' is read-only.
            return data.val1 + data.val2 + data.val3 + data.val4 +
                   data.val5 + data.val6 + data.val7 + data.val8;
        }

        static void Main(string[] args)
        {
            //1. The Basics - Modeling a 2D Point
            Point2D point = new Point2D(3, 4);
            //point.X = 5; //Get error as it is readonly
            //point.Y = 6; //Get error as it is readonly
            Console.WriteLine($"Point Coordinates: ({point.X}, {point.Y})");
            Console.WriteLine($"The distances from the origin (0,0) is: {point.getDistanceFromOrigin()}");

            //2. Class to Struct - Memory Optimization
            ColorRGB color = new ColorRGB(0, 255, 255);
            Console.WriteLine($"Value of R: {color.R}");
            Console.WriteLine($"Value of G: {color.G}");
            Console.WriteLine($"Value of B: {color.B}");

            //3. The "Ref" Factor – Working with Large Structs
            LargeData largeData = new LargeData(4.2, 3.3, 6.1, 7.8, 4.3, 6.2, 8.1, 1.5);
            double total = CalculateSum(largeData);
            Console.WriteLine($"value of CalculateSum is: {total}");

            //4.  Refactoring a "Data Bag"
            SphereMetrics sphereMetrics1 = new SphereMetrics(3);
            SphereMetrics sphereMetrics2 = new SphereMetrics();
            sphereMetrics2 = sphereMetrics1;
            sphereMetrics2.Radius = 5.5;
            Console.WriteLine($"Radius of sphereMetrics1 is {sphereMetrics1.Radius}");
            Console.WriteLine($"The volume of sphereMetrics1 is {sphereMetrics1.GetVolume()}");
            Console.WriteLine($"Radius of sphereMetrics2 is {sphereMetrics2.Radius}");
            Console.WriteLine($"The volume of sphereMetrics2 is {sphereMetrics2.GetVolume()}");


            //5. Structs with Properties and Logic
            TimeSlot timeSlot1 = new TimeSlot(5, 20);
            TimeSlot timeSlot2 = new TimeSlot(7, 70);

            Console.WriteLine($"Time slot 1 is {timeSlot1}");
            Console.WriteLine($"Total duration for time slot 1 is :{timeSlot1.TotalMinutes()} minutes");
            Console.WriteLine($"Time slot 2 is {timeSlot2}");
            Console.WriteLine($"Total duration for time slot 2 is :{timeSlot2.TotalMinutes()} minutes");

        }

        public struct Point2D
        {
            public readonly double X;
            public readonly double Y;

            public Point2D(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            public double getDistanceFromOrigin()
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public readonly struct ColorRGB
        {
            public readonly byte R;
            public readonly byte G;
            public readonly byte B;
            public ColorRGB(byte r, byte g, byte b)
            {
                this.R = r;
                this.G = g;
                this.B = b;
            }
        }

        public struct LargeData
        {
            public double val1;
            public double val2;
            public double val3;
            public double val4;
            public double val5;
            public double val6;
            public double val7;
            public double val8;

            public LargeData(double input1, double input2, double input3, double input4, double input5, double input6, double input7, double input8)
            {
                this.val1 = input1;
                this.val2 = input2;
                this.val3 = input3;
                this.val4 = input4;
                this.val5 = input5;
                this.val6 = input6;
                this.val7 = input7;
                this.val8 = input8;
            }

        }

        public struct SphereMetrics 
        {
            public double Radius;
            const double PI = Math.PI;

            public SphereMetrics(double radius)
            {
                this.Radius = radius;
            }

            public double GetVolume()
            {
                return 4.0 / 3.0 * PI * Math.Pow(Radius, 3);
            }
        }

        public struct TimeSlot
        {
            public int Hours;
            public int Minutes;

            public TimeSlot(int hours, int minutes)
            {
                this.Hours = hours;
                if (minutes < 0 || minutes > 59)
                {
                    Console.WriteLine("Warining: Minutes should be between 0 and 59");
                    this.Minutes = 0;
                }
                else
                {
                    this.Minutes = minutes;
                }
            }

            public int TotalMinutes() 
            { 
                return this.Hours*60+this.Minutes; 
            }

            public override string ToString()
            {
                // "D2" is a format string that ensures at least 2 digits (e.g., 05 instead of 5)
                return $"{Hours:D2}:{Minutes:D2}";
            }
        }


    }
}
