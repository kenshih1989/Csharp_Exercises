namespace Stack_Queue_HashSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Stack (LIFO - Last In First Out)
            //The String Reverse
            Stack<char> charStack = new Stack<char>();
            string input = "Hello, World!";
            foreach (char c in input)
            {
                charStack.Push(c);
            }
            string reversed = Program.ReverseString(charStack);

            Console.WriteLine($"Original: {input}");
            Console.WriteLine($"Reversed: {reversed}");

            //Balanced Parentheses
            //Balanced mean every opening parenthesis has a corresponding closing parenthesis
            string parenthesesInput = "(())()";
            Stack<char> parenthesesStack = new Stack<char>();
            bool isBalanced = true;
            while (isBalanced)
            {
                foreach (char c in parenthesesInput)
                {
                    if (c == '(')
                    {
                        parenthesesStack.Push(c);
                    }
                    else if (c == ')')
                    {
                        if (parenthesesStack.Count == 0)
                        {
                            isBalanced = false;
                            break;
                        }
                        parenthesesStack.Pop();
                    }
                }
                if (parenthesesStack.Count != 0)
                {
                    isBalanced = false;
                }
                break;
            }
            Console.WriteLine($"Parentheses \"{parenthesesInput}\" balanced: {isBalanced}");

            //The Back and Forward button in Browsers
            BrowserHistory browserHistory = new BrowserHistory("homepage.com");
            browserHistory.Visit("page1.com");
            browserHistory.Visit("page2.com");

            Console.WriteLine($"Current URL: {browserHistory.GetCurrentURL()}");
            Console.WriteLine($"Back to: {browserHistory.Back()}");
            Console.WriteLine($"Back to: {browserHistory.Back()}");
            Console.WriteLine($"Current URL after back twice: {browserHistory.GetCurrentURL()}");
            Console.WriteLine($"Forward to: {browserHistory.Forward()}");
            Console.WriteLine($"Current URL after forward once: {browserHistory.GetCurrentURL()}");

            //2. Queue (FIFO - First In First Out)
            // The Print Spooler
            Queue<string> printQueue = new Queue<string>();
            printQueue.Enqueue("Document1");
            printQueue.Enqueue("Document2");
            printQueue.Enqueue("Document3");
            printQueue.Enqueue("Document4");
            printQueue.Enqueue("Document5");
            while (printQueue.Count > 0)
            {
                string document = printQueue.Dequeue();
                Console.WriteLine($"Printing: {document}");
            }

            //Circular Buffer Simulation
            int bufferSize = 3;
            Queue<int> circularBuffer = new Queue<int>(bufferSize);
            for (int i = 1; i <= 5; i++)
            {
                if (circularBuffer.Count == bufferSize)
                {
                    circularBuffer.Dequeue();
                }
                circularBuffer.Enqueue(i);
                Console.WriteLine($"Buffer after adding {i}: {string.Join(", ", circularBuffer)}");
            }

            // Level-Order Sequence
            Queue<int> levelOrderQueue = new Queue<int>();
            levelOrderQueue.Enqueue(1); // Root
            levelOrderQueue.Enqueue(2); // Level 1 - Left
            levelOrderQueue.Enqueue(3); // Level 1 - Right
            while (levelOrderQueue.Count > 0)
            {
                int node = levelOrderQueue.Dequeue();
                Console.WriteLine($"Visiting node: {node}");
            }

            //3. HashSet (Unique Collection)
            // Duplicate Finder
            int[] numbers = { 1, 2, 3, 2, 4, 5, 1 };
            List<int> duplicates = Program.GetDuplicates(numbers);
            Console.WriteLine($"Duplicates: {string.Join(", ", duplicates)}");

            // Unique Words Counter
            string text = "Hello world, hello universe!";
            string[] words = text.ToLower().Split(new char[] { ' ', ',', '!', '.' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> uniqueWords = new HashSet<string>(words);
            Console.WriteLine($"Unique words count: {uniqueWords.Count}");
            foreach (string word in uniqueWords)
            {
                Console.WriteLine($"Unique word: {word}");
            }

            //Set Intersection, Union and SymmetricExceptWith
            HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4 };
            HashSet<int> setB = new HashSet<int> { 3, 4, 5, 6 };

            HashSet<int> intersection = new HashSet<int>(setA);
            intersection.IntersectWith(setB);

            HashSet<int> union = new HashSet<int>(setA);
            union.UnionWith(setB);

            HashSet<int> symmetricExcept = new HashSet<int>(setA);
            symmetricExcept.SymmetricExceptWith(setB);

            Console.WriteLine($"Intersection: {string.Join(", ", intersection)}");
            Console.WriteLine($"Union: {string.Join(", ", union)}");
            Console.WriteLine($"Symmetric Except: {string.Join(", ", symmetricExcept)}");

        }

        public static string ReverseString(Stack<char> charStack)
        {
            string reversed = "";
            while (charStack.Count > 0)
            {
                reversed += charStack.Pop();
            }
            return reversed;
        }

        class BrowserHistory
        {
            private Stack<string> backStack;
            private Stack<string> forwardStack;
            private string currentURL;
            public BrowserHistory(string homepage)
            {
                backStack = new Stack<string>();
                forwardStack = new Stack<string>();
                currentURL = homepage;
            }
            public void Visit(string url)
            {
                backStack.Push(currentURL);
                currentURL = url;
                forwardStack.Clear();
            }
            public string Back()
            {
                if (backStack.Count > 0)
                {
                    forwardStack.Push(currentURL);
                    currentURL = backStack.Pop();
                }
                return currentURL;
            }
            public string Forward()
            {
                if (forwardStack.Count > 0)
                {
                    backStack.Push(currentURL);
                    currentURL = forwardStack.Pop();
                }
                return currentURL;
            }
            public string GetCurrentURL()
            {
                return currentURL;
            }
        }

        public static List<int> GetDuplicates(int[] numbers)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> duplicates = new HashSet<int>();
            foreach (int number in numbers)
            {
                if (!seen.Add(number))
                {
                    duplicates.Add(number);
                }
            }
            return duplicates.ToList();
        }

    }
}
