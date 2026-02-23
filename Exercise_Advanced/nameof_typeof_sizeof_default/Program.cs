using System.Runtime.CompilerServices;

namespace nameof_typeof_sizeof_default
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //nameof exercises
            //1. Argument Validation
            try
            {
                SetAge(-3);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //2. Property Change Notification
            User user = new User("John");

            //3. Logging Constants
            Console.WriteLine($"Method: {nameof(User.NotifyChange)} is from the class:{nameof(User)}");

            //typeof exercises
            //1. Type Checking
            Circle circle = new Circle();
            if (typeof(Circle) == circle.GetType())
            {
                Console.WriteLine($"Namespace : {typeof(Circle).Namespace}");
                Console.WriteLine($"Full Name : {typeof(Circle).FullName}");
                Console.WriteLine($"Name of the type : {typeof(Circle).Name}");
            }

            //2. Generic Metadata
            Console.WriteLine(GetTypeName<Circle>());
            Console.WriteLine(GetTypeName<int>());

            //3. 
            Type processorType = typeof(DataProcessor);
            object[] attributes = processorType.GetCustomAttributes(typeof(HelpAttribute), false);

            if (attributes.Length > 0)
            {
                HelpAttribute help = (HelpAttribute)attributes[0];
                Console.WriteLine($"Found Attribute! Description: {help.Description}");
            }

            //sizeof exercises
            //1. Primitive Mapping
            Console.WriteLine($"The size of int is {sizeof(int)}");
            Console.WriteLine($"The size of long is {sizeof(long)}");
            Console.WriteLine($"The size of double is {sizeof(double)}");
            Console.WriteLine($"The size of bool is {sizeof(bool)}");
            Console.WriteLine($"The different between the size of int and long is {sizeof(int) - sizeof(long)}. ");

            //2. Custom Struct Size
            Console.WriteLine($"Size of Point struct: {Unsafe.SizeOf<Point>()} bytes");

            //3. Memory Math
            long[] array = new long[1000];
            Console.WriteLine($"Total memory consumped by the array is {sizeof(long) * array.Length} bytes");

            //default exercises
            //1. Array Initialization
            DateTime[] dateTimes = new DateTime[5];
            dateTimes[3] = default;
            Console.WriteLine($"The default date time value for dateTimes[3] is {dateTimes[3]}");

            //2. Generic Defaults
            Box<int> box = new Box<int>(5);
            Box<string> box2 = new Box<string>("my text");
            Console.WriteLine($"Default value for box: {box.ResetValue()}");
            Console.WriteLine($"Default value for box2: {box2.ResetValue()}");

            //3. The Default Literal
            DoSomething(1);
            DoSomething(default, "some messages");


        }

        static int SetAge(int age)
        {
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(age)} cannot less than 0");
            }
            return age;
        }

        static string GetTypeName<T>()
        {

            return typeof(T).Name;
        }

        static void DoSomething(int count = default, string message = default)
        {
            Console.WriteLine($"The value of count is {count}");
            Console.WriteLine($"The value of message is {message}");
        }
    }

    class User
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyChange(nameof(UserName));
            }
        }

        public User(string userName)
        {
            this.UserName = userName;
        }

        public void NotifyChange(string propertyName)
        {
            Console.WriteLine($"{propertyName} had been updated");
        }
    }

    public struct Point { public int X; public int Y; }

    public interface IShape
    {

    }

    class Circle : IShape
    {

    }

    // 1. Define a custom attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class HelpAttribute : Attribute
    {
        public string Description { get; set; }
    }

    // 2. Apply it to a class
    [Help(Description = "This class processes data")]
    class DataProcessor { }

    class Box<T>
    {
        public T _box;
        public Box(T box)
        {
            this._box = box;
        }
        public T ResetValue()
        {
            this._box = default;
            return _box;
        }
    }

}
