namespace TypeCasting_is_as
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Shape Inspector
            List<Shape> myShapes = new List<Shape>()
            {
                new Circle(8),
                new Square(5)
            };

            foreach (Shape shape in myShapes)
            {
                if (shape is Circle)
                {
                    Circle circle = (Circle)shape;
                    Console.WriteLine($"This is a circle with radius {circle.Radius}");
                }
                else
                {
                    Square square = (Square)shape;
                    Console.WriteLine($"This is a square with side {square.Side}");
                }
            }

            //2. Safe Downcasting
            List<object> myObjects = new List<object>
            {
                5,
                "text",
                true
            };

            string resultOfStringCasting;

            foreach (object singleObject in myObjects)
            {
                resultOfStringCasting = singleObject as string;
                if (resultOfStringCasting != null)
                {
                    Console.WriteLine(resultOfStringCasting.ToUpper());
                }
                else
                {
                    Console.WriteLine("Conversion failed: Object is not a string.");
                }
            }

            //3. The Data Processor (Pattern Matching)
            object testObject1 = new object();
            object testObject2 = new object();
            object testObject3 = new object();

            testObject1 = 6;
            testObject2 = "text";
            testObject3 = true;

            ProcessData(testObject1, testObject2, testObject3);
            ;

            //4. Numerical Precision (Explicit Casting)
            double amountOfDouble = 99.99;
            int amountOfInt = (int)amountOfDouble;
            Console.WriteLine($"amountOfInt: {amountOfInt}"); //output: 99
            Console.WriteLine($"amountOfDouble: {amountOfDouble}");

            //5. The UI Component Handler (Mixed Techniques)
            object[] components = { new Button(), 12345, new Textbox() };
            foreach (var item in components)
            {
                if (item is int secretKey)
                {
                    Console.WriteLine($"Key found: {secretKey}");
                }
                else if (item is Button btn)
                {
                    btn.Click();
                }
                else
                {
                    Console.WriteLine($"Unknown component: {item.GetType().Name}");
                }
            }
        }

        public class Button { public void Click() => Console.WriteLine("Button Clicked!"); }
        public class Textbox { public string Text { get; set; } }

        static void ProcessData(params object[] datas)
        {
            foreach (object data in datas)
            {
                if (data is int number)
                {
                    Console.WriteLine($"Square of {number}: {number * number}");
                }
                else if (data is string text)
                {
                    Console.WriteLine($"Length of {text}: {text.Length}");
                }
                else
                {
                    Console.WriteLine("Unknown data type.");
                }
            }
        }
    }

    class Shape
    {

    }

    class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            Radius = radius;
        }
    }

    class Square : Shape
    {
        public double Side { get; set; }
        public Square(double side)
        {
            Side = side;
        }
    }
}
