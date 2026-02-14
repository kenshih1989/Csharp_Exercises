namespace Switch_Patern_Matching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Shape Shifter (Property Pattern)
            Shape myFirstShape = new Rectangle(4.3, 7.3);
            Shape mySecondShape = new Circle(5.0);
            Shape myThridShape = new Square(8.0);
            Shape myFourthShape = new Shape();

            Console.WriteLine($"Area of the shape: {GetArea(myFirstShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(mySecondShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(myThridShape)}");
            Console.WriteLine($"Area of the shape: {GetArea(myFourthShape)}");

        }
        static double GetArea(object shape)
        {
            switch (shape)
            {
                case Rectangle rectangle:
                    return rectangle.Width * rectangle.Height;
                case Circle circle:
                    return circle.Radius * Math.PI;
                case Square square:
                    return square.Side * square.Side;
                default:
                    return 0;

            }
        }

    }



    public class Shape
    {

    }

    public class  Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
    public class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle (double radius)
        {
            this.Radius = radius;
        }
    }

    public class Square : Shape
    {
        public double Side { get; set; }
        public Square (double side)
        {
            this.Side = side;
        }  
    }
}
