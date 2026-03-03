namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Data Setup
            var numbers = new List<int> { 48, 24, 33, 74, 53, 86, 17, 85, 39, 10 };

            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", Dept = "IT", Salary = 90000 },
                new Employee { Id = 2, Name = "Bob", Dept = "HR", Salary = 50000 },
                new Employee { Id = 3, Name = "Charlie", Dept = "IT", Salary = 95000 },
                new Employee { Id = 4, Name = "Diana", Dept = "Sales", Salary = 70000 }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200 },
                new Product { Id = 2, Name = "Smartphone", Price = 800 },
                new Product { Id = 3, Name = "Tablet", Price = 600 }
            };

            var cities = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Houston", "London", "Tokyo", "Los Angeles" };

            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Emily", Age = 20, Grade = 95 },
                new Student { Id = 2, Name = "David", Age = 22, Grade = 75 },
                new Student { Id = 3, Name = "Sophia", Age = 19, Grade = 44 },
                new Student { Id = 4, Name = "Michael", Age = 21, Grade = 85 }
            };

            List<int> scores = new List<int> { 85, 92, 78, 90, 88, 95, 80, 91, 89, 94 };

            List<string> names = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Eve", "Ali", "Frank", "Grace", "Heidi", "Alan", "Ahmad" };

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Alice", EmployeeId = 1 },
                new User { Id = 2, Name = "Bob", EmployeeId = 2 },
                new User { Id = 3, Name = "Charlie", EmployeeId = 3 },
                new User { Id = 4, Name = "Diana", EmployeeId = 4 }
            };

            List<post> posts = new List<post>
            {
                new post { Id = 1, Title = "C# Basics", UserId = 1 },
                new post { Id = 2, Title = "LINQ in Depth", UserId = 1 },
                new post { Id = 3, Title = "HR Best Practices", UserId = 2 },
                new post { Id = 4, Title = "IT Security", UserId = 3 },
                new post { Id = 5, Title = "Sales Strategies", UserId = 4 }
            };


            // Method Syntax
            //1.The Filterer
            var greaterThan50 = numbers.Where(n => n > 50);
            Console.WriteLine("Numbers greater than 50:");
            foreach (var num in greaterThan50)
            {
                Console.WriteLine(num);
            }

            //2. The Transformaer
            var employeeEmails = employees.Select(e => e.Email);
            Console.WriteLine("Employee Emails:");
            foreach (var email in employeeEmails)
            {
                Console.WriteLine(email);
            }

            //3. The Top Performer
            var expensiveProducts = products.OrderByDescending(p => p.Price).FirstOrDefault(p => p.Price > 1000);
            Console.WriteLine($"Most expensive product over $1000: {expensiveProducts?.Name} (${expensiveProducts?.Price})");

            //4. The Unique Cities
            var uniqueCities = cities.Distinct();
            Console.WriteLine("Unique Cities:");
            foreach (var city in uniqueCities)
            {
                Console.WriteLine(city);
            }

            //5. The Counter
            var passingStudentsCount = students.Count(s => s.Grade >= 90);
            Console.WriteLine($"Number of students with grade 90 or above: {passingStudentsCount}");


            // Query Syntax
            //6. The Basic Query
            var highScores = from score in scores
                             where score >= 80 && score <= 100
                             select score;
            Console.WriteLine("High Scores:");
            foreach (var score in highScores)
            {
                Console.WriteLine(score);
            }

            //7. The Alphabetizer
            var namesStartingWithA = from name in names
                                     where name.StartsWith("A")
                                     orderby name
                                     select name;

            Console.WriteLine("Names starting with 'A':");
            foreach (var name in namesStartingWithA)
            {
                Console.WriteLine(name);
            }

            //8. The Simple Join
            var userPosts = from user in users
                            join post in posts on user.Id equals post.UserId
                            select new
                            {
                                UserName = user.Name,
                                PostTitle = post.Title
                            };
            foreach (var item in userPosts)
            {
                Console.WriteLine($"User: {item.UserName}, Post: {item.PostTitle}");
            }

            //9. The Anonymous Object 
            var productInstock = from product in products
                                 select new
                                 {
                                     product.Name,
                                     product.Price,
                                 };
            foreach (var item in productInstock)
            {
                Console.WriteLine($"Product: {item.Name}, Price: ${item.Price}");
            }

            //10. The Grouping
            var groupedByDept = from employee in employees
                                group employee by employee.Dept into deptGroup
                                select new
                                {
                                    Dept = deptGroup.Key,
                                    Employees = deptGroup.ToList()
                                };
            foreach (var group in groupedByDept)
            {
                Console.WriteLine($"Department: {group.Dept}");
                foreach (var emp in group.Employees)
                {
                    Console.WriteLine($" - {emp.Name} (${emp.Salary})");
                }
            }
        }

        class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Dept { get; set; }
            public decimal Salary { get; set; }
            public string Email => $"{Name.ToLower()}@company.com";
        }

        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public bool InStock => Price > 0;
        }

        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public int Grade { get; set; }
        }

        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int EmployeeId { get; set; }
        }

        class post
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int UserId { get; set; }
        }
    }
}
