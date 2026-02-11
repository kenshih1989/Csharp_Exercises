namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Word Counter
            string text = "This is a sample text with several words. This text is for testing";
            Dictionary<string, int> wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            string[] words = text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }
            Console.WriteLine("Word Count:");
            foreach (var kvp in wordCount)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            //2. Employee Directory
            Dictionary<int, string> employeeDirectory = new Dictionary<int, string>();
            employeeDirectory.Add(101, "Alice Johnson");
            employeeDirectory.Add(102, "Bob Smith");
            employeeDirectory.Add(103, "Charlie Brown");
            employeeDirectory.Add(104, "Diana Prince");
            employeeDirectory.Add(105, "Ethan Hunt");

            //check if an employee ID exists
            int searchId = 106;
            if (employeeDirectory.ContainsKey(searchId))
            {
                Console.WriteLine($"\nEmployee ID {searchId} found: {employeeDirectory[searchId]}");
            }
            else
            {
                Console.WriteLine($"\nEmployee ID {searchId} not found.");
            }

            //remove an employee
            employeeDirectory.Remove(103);
            Console.WriteLine("\nRemoved employee with ID 103.");

            //Display all employees
            Console.WriteLine("\nEmployee Directory:");
            foreach (KeyValuePair<int,string> keyValuePair in employeeDirectory)
            {
                Console.WriteLine($"ID: {keyValuePair.Key}, Name: {keyValuePair.Value}");
            }

            //3. Product Inventory
            Dictionary<string, Product> productInventory = new Dictionary<string, Product>();
            productInventory.Add("P001", new Product("Laptop", 999.99m, 10));
            productInventory.Add("P002", new Product("Smartphone", 499.99m, 25));
            productInventory.Add("P003", new Product("Tablet", 299.99m, 15));

            //Find and reduce a product's stock by 1
            string productIdToFind = "P002";
            if (productInventory.TryGetValue(productIdToFind, out Product product))
            {
                product.StockQuantity--;
                Console.WriteLine($"\nReduced stock of {product.Name}. New stock quantity: {product.StockQuantity}");
            }
            else
            {
                Console.WriteLine($"\nProduct ID {productIdToFind} not found.");
            }

            //4. Grouping by Grade
            Dictionary<string, List<string>> gradeGroups = new Dictionary<string, List<string>>();
            gradeGroups.Add("10th Grade", new List<string> { "Alice", "Bob" });
            gradeGroups.Add("11th Grade", new List<string> { "Charlie" });

            //print students in 10th Grade
            Console.WriteLine("\nStudents in 10th Grade:");
            if (gradeGroups.TryGetValue("10th Grade", out List<string> tenthGraders))
            {
                foreach (string student in tenthGraders)
                {
                    Console.WriteLine(student);
                }
            }
            else
            {
                Console.WriteLine("No students found in 10th Grade.");
            }

            //5. Country Currency (The "Safe" Dictionary Lookup)
            Dictionary<string, string> countryCurrency= new Dictionary<string, string>();
            countryCurrency.Add("USD", "United State Dollar");
            countryCurrency.Add("EUR", "Euro");
            countryCurrency.Add("JPY", "Japanese Yen");
            countryCurrency.Add("GBP", "British Pound Sterling");
            countryCurrency.Add("MYR", "Malaysian Ringgit");

            Console.Write("\nCountry Currencies:");
            string currencyCodeToFind = Console.ReadLine();

            if (countryCurrency.TryGetValue(currencyCodeToFind, out string currencyName))
            {
                Console.WriteLine($"Currency for {currencyCodeToFind}: {currencyName}");
            }
            else
            {
                Console.WriteLine($"Currency code {currencyCodeToFind} not found.");
            }

        }

        class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public Product(string name, decimal price, int quantity)
            {
                this.Name = name;
                this.Price = price;
                this.StockQuantity = quantity;
            }
        }
    }
}
