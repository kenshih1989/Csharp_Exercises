using System;

namespace Operator_Overloading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Fraction Struct
            var fraction1 = new Fraction { numerator = 1, denominator = 2 }; // Represents 1/2
            var fraction2 = new Fraction { numerator = 1, denominator = 3 }; // Represents 1/3
            Console.WriteLine($"Fraction 1: {fraction1.numerator}/{fraction1.denominator}");
            Console.WriteLine($"Fraction 2: {fraction2.numerator}/{fraction2.denominator}");
            Console.WriteLine($"Addition: {fraction1.numerator}/{fraction1.denominator} + {fraction2.numerator}/{fraction2.denominator} = {(fraction1 + fraction2).numerator}/{(fraction1 + fraction2).denominator}");
            var fraction3 = new Fraction { numerator = 1, denominator = 4 }; // Represents 1/4
            var fraction4 = new Fraction { numerator = 2, denominator = 8 }; // Represents 2/8
            var fraction5 = new Fraction { numerator = 6, denominator = 8 }; // Represents 6/8
            var myFraction = fraction3 + fraction4;
            Console.WriteLine($"My Fraction: {myFraction.numerator}/{myFraction.denominator}");
            var myFraction2 = fraction3 * fraction5;
            Console.WriteLine($"My Fraction 2: {myFraction2.numerator}/{myFraction2.denominator}");

            //2. The Money Type
            Money money1 = new Money(100, "USD");
            Money money2 = new Money(100, "USD");
            Console.WriteLine($"Money 1: {money1.Amount} {money1.Currency}");
            Console.WriteLine($"Money 2: {money2.Amount} {money2.Currency}");
            Console.WriteLine($"Money 1 == Money 2: {money1 == money2}");
            Money money3 = new Money(100, "EUR");
            Console.WriteLine($"Money 3: {money3.Amount} {money3.Currency}");
            Console.WriteLine($"Money 1 != Money 3: {money1 != money3}");

            //3.  Database "Bitmask" Permissions
            UserPermissions user1 = new UserPermissions(Permission.Read | Permission.Write);
            Console.WriteLine($"User 1 Permissions: {user1.Permissions}");
            user1 += new UserPermissions(Permission.Execute);
            Console.WriteLine($"User 1 Permissions after adding Execute: {user1.Permissions}");
            user1 -= new UserPermissions(Permission.Write);
            Console.WriteLine($"User 1 Permissions after removing Write: {user1.Permissions}");

            //4. Coordinate Offsets
            Point2D point = new Point2D(1, 1);
            Velocity velocity = new Velocity(2, 3);
            Console.WriteLine($"Initial Point: ({point.X}, {point.Y})");
            Console.WriteLine($"Velocity: ({velocity.Vx}, {velocity.Vy})");
            Point2D newPoint = point + velocity;
            Console.WriteLine($"New Point after applying velocity: ({newPoint.X}, {newPoint.Y})");

            //5. The "Smart" String (Case-Insensitive)
            CaseInsensitiveString str1 = new CaseInsensitiveString("Hello");
            CaseInsensitiveString str2 = new CaseInsensitiveString("hello");
            Console.WriteLine($"String 1: {str1.Value}");
            Console.WriteLine($"String 2: {str2.Value}");
            Console.WriteLine($"String 1 hash code: {str1.GetHashCode()}");
            Console.WriteLine($"String 2 hash code: {str2.GetHashCode()}");
            Console.WriteLine($"String 1 is having same hash code as string 2?: {str1.GetHashCode() == str2.GetHashCode()}");
            Console.WriteLine($"String 1 equals String 2: {str1.Equals(str2)}");
            Console.WriteLine($"String 1 == String 2: {str1.Equals(str2)}");
            List<CaseInsensitiveString> stringList1 = new List<CaseInsensitiveString> { str1 };
            List<CaseInsensitiveString> stringList2 = new List<CaseInsensitiveString> { str2 };
            Console.WriteLine($"String List 1 contains String 2: {stringList1.Contains(str2)}");

        }
    }

    struct Fraction
    {
        public double numerator;
        public double denominator;

        private static double GetGCD(double a, double b)
        {
            a = Math.Abs(a); b = Math.Abs(b);
            while (b != 0) { a %= b; (a, b) = (b, a); }
            return a;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            double num = a.numerator * b.denominator + b.numerator * a.denominator;
            double den = a.denominator * b.denominator;
            double common = GetGCD(num, den);
            return new Fraction { numerator = num / common, denominator = den / common };
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            double num = a.numerator * b.denominator - b.numerator * a.denominator;
            double den = a.denominator * b.denominator;
            double common = GetGCD(num, den);
            return new Fraction { numerator = num / common, denominator = den / common };

        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            double num = a.numerator * b.numerator;
            double den = a.denominator * b.denominator;
            double common = GetGCD(num, den);
            return new Fraction { numerator = num / common, denominator = den / common };
        }
    }

    class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Money(decimal amount, string currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }
        public override bool Equals(object? obj) => Equals(obj as Money);
        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Money? a, Money? b)
        {
            if (ReferenceEquals(a, b)) return true; // Both null or same instance
            if (a is null || b is null) return false;
            return (a.Amount == b.Amount && a.Currency == b.Currency);
        }

        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }
    }

    [Flags]
    enum Permission
    {
        Read = 1,
        Write = 2,
        Execute = 4
    }

    class UserPermissions
    {
        public Permission Permissions { get; set; }
        public UserPermissions(Permission permissions)
        {
            this.Permissions = permissions;
        }
        public static UserPermissions operator +(UserPermissions a, UserPermissions b)
        {
            return new UserPermissions(a.Permissions | b.Permissions);
        }
        public static UserPermissions operator -(UserPermissions a, UserPermissions b)
        {
            return new UserPermissions(a.Permissions & ~b.Permissions);
        }
    }

    struct Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    struct Velocity
    {
        public double Vx { get; set; }
        public double Vy { get; set; }
        public Velocity(double vx, double vy)
        {
            this.Vx = vx;
            this.Vy = vy;
        }
        public static Point2D operator +(Point2D point, Velocity velocity)
        {
            return new Point2D(point.X + velocity.Vx, point.Y + velocity.Vy);
        }
    }

    class CaseInsensitiveString : IEquatable<CaseInsensitiveString>
    {
        public string Value { get; set; }
        public CaseInsensitiveString(string value)
        {
            this.Value = value;
        }

        public bool Equals(CaseInsensitiveString? other)
        {
            return string.Equals(this.Value, other?.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            // Ensure the hash code is also case-insensitive!
            return Value?.ToLower().GetHashCode() ?? 0;
        }
    }
}
