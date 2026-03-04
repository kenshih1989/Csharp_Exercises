using System.Diagnostics;
using System.Reflection.Metadata;

namespace Assert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The "Impossible" Null
            User user = null;
            double purchasingPrice = 50;
            CalculateDiscount(user, purchasingPrice);
            User user2 = new User();
            user2.Discount = 10;
            CalculateDiscount(user2, purchasingPrice);

            //2. The Physical Boundary
            double temperature = -300;
            Trace.Assert(temperature > -273.15, "Temperature cannot be below absolute zero");

            //3. Array Index Safety
            int[] buffer = new int[10];
            UpdateBuffer(-2, 5, buffer);

            //4. The Internal State Check
            Door door = new Door();
            door.IsLocked = true;
            ValidateDoorState(door);

            //5. The "Release" Mode Trap
            Debug.Assert(false);
            Trace.Assert(false);
        }

        static void CalculateDiscount(User user, double price)
        {
            Debug.Assert(user!=null, "User object must not be null in order to calculate the discount");
            Console.WriteLine($"The purchasing price after having discount is {price- price*user.Discount/100}");
        }

        static void UpdateBuffer(int index, int value, int[] buf)
        {
            Debug.Assert(buf != null, "Buffer should have been initialized by the Factory.");
            Debug.Assert(index >= 0 && index < buf.Length, $"Index {index} is out of range for buffer size {buf.Length}");
            buf[index] = value;
        }

        static void ValidateDoorState(Door d)
        {
            Debug.Assert(!(d.IsLocked&&!d.IsClosed),"Door cannot open and locked" );
        }
    }

    class User
    {
        public double Discount { get; set; }
    }

    class Door
    {
        public bool IsClosed { get; set; } = false;
        public bool IsLocked { get; set; } = false;
    }

}
