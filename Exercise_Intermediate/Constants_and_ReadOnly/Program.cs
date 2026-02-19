using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Constants_and_ReadOnly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Basic "Fixed Value"
            //PhysicsConstants.CERN = 120; //Cannot be changed
            Console.WriteLine($"The value of constant CERN is :{PhysicsConstants.CERN}");

            //2. The Runtime Setup
            ReportGenerator myReportGenerator = new ReportGenerator();
            myReportGenerator.ShowReportId();

            //3. Const vs. Readonly Comparison
            Circle myCircle = new Circle(4.0);
            Console.WriteLine($"The area of the circle is: {myCircle.GetArea()}");

            //4. The "Static Readonly" Pattern
            foreach (string AdminName in AppConfig.AdminUserNames)
            {
                Console.WriteLine($"{AdminName}");
            }

            //5. Debugging the Assignment
            SecureVault mySecureVault = new SecureVault();
            Console.WriteLine($"The value of CreatedAt: {mySecureVault.CreatedAt}");

        }
    }

    static class PhysicsConstants
    {
        public const int CERN = 299792458;
    }

    class ReportGenerator
    {
        //can't use const as the report id only can be known during run-time
        //const variable need to know the value during the compile-time
        public readonly string _reportId; 

        public ReportGenerator()
        {
            _reportId = Guid.NewGuid().ToString();
        }

        public void ShowReportId()
        {
            Console.WriteLine($"The report Id is {this._reportId}");
        }
    }

    public class Circle
    {
        public const double Pi = Math.PI;

        public readonly double Radius;

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        public double GetArea() => Pi* Math.Pow(Radius,2);

    }

    public class AppConfig
    {
        public static readonly string[] AdminUserNames = new string[3]{ "Admin", "Administrator", "Admin2" };
    }

    public class SecureVault
    {
        public readonly DateTime CreatedAt;

        public SecureVault()
        {
            CreatedAt = DateTime.Now;
        }

        public void UpdateTimeStamp()
        {
            //CreatedAt = DateTime.Now; //Readonly field only can be assigned at constructor
        }
    }
}
