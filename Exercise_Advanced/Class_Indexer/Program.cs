using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Class_Indexer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Temperature Logger (Basic)
            WeeklyTemp weeklyTemp = new WeeklyTemp();
            weeklyTemp[0] = 45;
            weeklyTemp[1] = 0;
            weeklyTemp[2] = 75;
            weeklyTemp[8] = 15;

            Console.WriteLine($"Value for weeklyTemp[0] is {weeklyTemp[0]}");
            Console.WriteLine($"Value for weeklyTemp[1] is {weeklyTemp[1]}");
            Console.WriteLine($"Value for weeklyTemp[2] is {weeklyTemp[2]}");
            Console.WriteLine($"Value for weeklyTemp[8] is {weeklyTemp[8]}");


            //2. The Multi-Type Indexer (Overloading)
            ContactList contactList = new ContactList();
            contactList[0] = "Alice";
            contactList[1] = "Bob";
            contactList[2] = "Catherine";
            contactList[3] = "John";
            contactList[4] = "Mark";

            //using index as input
            Console.WriteLine($"Value for contactList[0] is {contactList[0]}");
            Console.WriteLine($"Value for contactList[1] is {contactList[1]}");

            //using name as input
            Console.WriteLine($"Contact list index for John is :{contactList["John"]}");
            Console.WriteLine($"Contact list index for Mary is :{contactList["Mary"]}");

            //3.  The Dictionary-Style Indexer (String Keys)
            SimpleDictionary simpleDictionary = new SimpleDictionary();
            simpleDictionary["1stKey"] = "Text1";
            simpleDictionary["2ndKey"] = "Text2";
            simpleDictionary["3rdKey"] = "Text3";
            simpleDictionary["3rdKey"] = "Text3_Ammended";


            Console.WriteLine($"Value of 1stkey in the simple dictionary: {simpleDictionary["1stKey"]}");
            Console.WriteLine($"Value of 2ndkey in the simple dictionary: {simpleDictionary["2ndKey"]}");
            Console.WriteLine($"Value of 3rdkey in the simple dictionary: {simpleDictionary["3rdKey"]}");
            Console.WriteLine($"Value of 4thkey in the simple dictionary: {simpleDictionary["4thKey"]}");

            //4. The 2D Grid (Multi-parameter Indexer)
            GameGrid gameGrid = new GameGrid();
            gameGrid[5, 2] = 'x';
            gameGrid[2, 5] = 'O';
            gameGrid[3, 2] = 'X';
            gameGrid[4, 4] = 'o';
            gameGrid[8, 3] = 'x';

            Console.WriteLine($"The value at gameGrid[3, 2] is :{gameGrid[3, 2]}");
            Console.WriteLine($"The value at gameGrid[8, 3] is :{gameGrid[8, 3]}");
            Console.WriteLine($"The value at gameGrid[4, 4] is :{gameGrid[4, 4]}");
            Console.WriteLine($"The value at gameGrid[5, 5] is :{gameGrid[5, 5]}");
            gameGrid.DisplayAll();
            Console.WriteLine();

            //5. Read-Only Bit Flapper (Advanced)
            BitStatus bitStatus = new BitStatus(5); // 0101
            Console.WriteLine($"Is bit at position 0 is 1? {bitStatus[0]}");
            Console.WriteLine($"Is bit at position 1 is 1? {bitStatus[1]}");
            Console.WriteLine($"Is bit at position 2 is 1? {bitStatus[2]}");
            Console.WriteLine($"Is bit at position 3 is 1? {bitStatus[3]}");

        }
    }

    class WeeklyTemp
    {
        private decimal[] DaysTemperature = new decimal[7];

        public decimal this[int index]
        {
            get
            {
                //Prevent out of range
                if (index < 0 || index >= DaysTemperature.Length)
                    return -99;

                return DaysTemperature[index];
            }
            set
            {
                if (index >= 0 && index < DaysTemperature.Length)
                {
                    if (value >= -50 && value <= 60)
                        DaysTemperature[index] = value;
                    else
                        DaysTemperature[index] = -99;
                }
            }
        }
    }

    class ContactList
    {
        private string[] Names = new string[10];

        public string this[int index]
        {
            get => (index >= 0 && index < Names.Length) ? Names[index] : "Unknown";
            set => Names[index] = value;
        }

        public int this[string name]
        {
            get
            {
                for (int i = 0; i < Names.Length; i++)
                {
                    if (Names[i] == name)
                        return i;
                }
                return -1;
            }
        }
    }

    class SimpleDictionary
    {
        private string[] Keys = new string[10];
        private string[] Values = new string[10];

        public string this[string mykey]
        {
            get
            {
                for (int i = 0; i < Keys.Length; i++)
                {
                    if (Keys[i] == mykey)
                        return Values[i];
                }
                return "No such key";
            }

            set
            {
                bool foundKey = false;
                int filledCount = 0;

                //Check how many spaces in array been filled
                foreach (string key in Values)
                {
                    if (key != null)
                        filledCount++;
                }

                //key found and overwrite the value
                for (int i = 0; i < Keys.Length; i++)
                {
                    if (Keys[i] == mykey)
                    {
                        Values[i] = value;
                        foundKey = true;
                        break;
                    }
                }

                //key not found and add the key/value pair
                if (!foundKey)
                {
                    Keys[filledCount] = mykey;
                    Values[filledCount] = value;
                }
            }
        }
    }

    class GameGrid
    {
        private char[,] Grid = new char[10, 10];

        public char this[int x, int y]
        {
            get
            {
                return Grid[x, y];
            }

            set
            {
                Grid[x, y] = char.ToUpper(value);
            }
        }

        public void DisplayAll()
        {
            // Get the number of rows (dimension 0) and columns (dimension 1)
            int rows = Grid.GetLength(0);
            int cols = Grid.GetLength(1);

            // Outer loop iterates through rows
            for (int i = 0; i < rows; i++)
            {
                // Inner loop iterates through columns
                for (int j = 0; j < cols; j++)
                {
                    if (Grid[i, j] == default)
                    {
                        // Display "_ " if it is null value
                        Console.Write("_ ");
                    }
                    else
                    {
                        // Access and use the element at the current row and column
                        Console.Write(Grid[i, j] + " ");
                    }

                }
                // Move to the next line after each row
                Console.WriteLine();
            }
        }
    }

    class BitStatus
    {
        private int IntValue{get;set;}

        public BitStatus(int intValue)
        {
            IntValue = intValue;
        }

        public bool this[int bitIndex]
        {
            get=> (IntValue & (1 << bitIndex)) != 0;
        }
    }
}
