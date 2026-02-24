namespace Null_Coalesing_Operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The UI Placeholder
            string userName = null;
            string displayName = userName ?? "Guest";
            Console.WriteLine($"DisplayName:{displayName}");

            //2. Object Property Guard
            WebPage myPage = new WebPage();
            string currentTitle = myPage.PageInfo?.Title ?? "Default Page";
            Console.WriteLine($"The current title is {currentTitle}");

            //3. Ensuring a Collection Exists
            Article myArticle = new Article();
            string newTag = "CSharp";

            myArticle.Tags ??= new List<string>();
            myArticle.Tags.Add(newTag);
            Console.WriteLine($"{nameof(myArticle)} got {myArticle.Tags.Count} tag");

            //4. Configuration Fallbacks
            string envVariable = null;
            string appSettings = null;
            string connectionString = envVariable ?? appSettings ?? "LocalDB";
            Console.WriteLine($"The connection string value is {connectionString}");

            //5. Method Return Safety
            string description = GetDescription();
            string shoutyDescription = description?.ToUpper() ?? "EMPTY";
            Console.WriteLine($"The shoutyDescription value is {shoutyDescription}");
        }
        static string GetDescription()
        {
            return null;
        }
    }

    public class MetaData { public string Title { get; set; } }
    public class WebPage { public MetaData PageInfo { get; set; } }
    public class Article
    {
        public List<string> Tags { get; set; }
    }
}
