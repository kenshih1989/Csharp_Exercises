using System.Net.Sockets;

namespace Null_Conditional_Operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Branch Name
            Store myStore = null;
            string name = myStore?.Location?.BranchName;
            Console.WriteLine($"The branch name of {nameof(myStore)} is {name}");
            myStore = new Store();
            myStore.Location = new Address();
            myStore.Location.BranchName = "HappyMart";
            name = myStore?.Location?.BranchName;
            Console.WriteLine($"The branch name of {nameof(myStore)} is {name}");

            //2. The Data Formatter
            Formatter processor = null;
            string result = processor?.Format("ABC");
            Console.WriteLine($"The result of {nameof(processor)} is {result}");
            processor = new Formatter();
            result = processor?.Format("ABC");
            Console.WriteLine($"The result of {nameof(processor)} is {result}");


            //3. Safe Collection Item Access
            Customer[] customer = null;
            Customer first = customer?[0];
            Console.WriteLine($"The first customer is {first?.Name}");
            customer = new Customer[5]
            {
                new Customer("John"),
                new Customer("Bob"),
                new Customer("Leon"),
                new Customer("Shirley"),
                new Customer("Ada")
            };
            first = customer?[0];
            Console.WriteLine($"The first customer is {first?.Name}");

            //4. Deep String Manipulation
            User user = null;
            User currentUser = user?.GetUser(user);
            string cleanEmail = currentUser?.GetUserEmail()?.Trim();
            Console.WriteLine($"The {nameof(cleanEmail)} is {cleanEmail}");
            user = new User();
            user.Email = "   myEmail@mail.com ";
            currentUser = user?.GetUser(user);
            cleanEmail = currentUser?.GetUserEmail()?.Trim();
            Console.WriteLine($"The {nameof(cleanEmail)} is {cleanEmail}");

            //5. The "ToString" Safety Net
            object data = null;
            string display = data?.ToString();
            Console.WriteLine($"The value of {nameof(display)} is {display}");
            data = 5;
            display = data?.ToString();
            Console.WriteLine($"The value of {nameof(display)} is {display}");
        }
    }

    public class Store { public Address Location { get; set; } }
    public class Address { public string BranchName { get; set; } }
    public class Formatter { public string Format(string s) => s.ToUpper(); }
    public class Customer
    {
        public string Name { get; set; }
        public Customer(string name)
        {
            Name = name;
        }
    }
    public class User
    {
        public string Email { get; set; }
        public User GetUser(User u)
        {
            return u;
        }
        public string GetUserEmail() => Email;
    }
}
