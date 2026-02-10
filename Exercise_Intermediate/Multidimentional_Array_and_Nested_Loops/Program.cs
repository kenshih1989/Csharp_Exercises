namespace Multidimentional_Array_and_Nested_Loops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Matrix Summer
            int[,] _2DArray = new int[3, 4]
            {
                {1,3,5,7 },
                {2,4,6,8 },
                {7,8,9,10 }
            };

            int sum = 0;
            for (int i = 0; i < _2DArray.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < _2DArray.GetLength(1); j++)
                {
                    sum += _2DArray[i, j];
                    Console.Write(_2DArray[i, j] + "\t");
                }
                Console.WriteLine($"Sum of row{i + 1} is: {sum}");
            }

            //2. Multi-Dimentional Identity Matrix
            Console.Write("Number of rows and columns for Identity Matrix:");
            int n = Convert.ToInt32(Console.ReadLine());

            int[,] identityMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        identityMatrix[i, j] = 1;
                    }
                    else
                    {
                        identityMatrix[i, j] = 0;
                    }
                    Console.Write(identityMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            //3. The Multiplication Table Generator
            Console.Write("Number of rows of the matrix:");
            int rows = Convert.ToInt32(Console.ReadLine());

            //forming a square 2D array
            int[,] multiplicationTable = new int[rows, rows];

            Console.WriteLine("1-based logic:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    multiplicationTable[i, j] = (i + 1) * (j + 1);
                    Console.Write(multiplicationTable[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("0-based logic:");
            Console.WriteLine("1-based logic:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    multiplicationTable[i, j] = (i) * (j);
                    Console.Write(multiplicationTable[i, j] + "\t");
                }
                Console.WriteLine();
            }

            //4. Transpose the Grid
            int[,] originalArray = new int[2, 3]
            {
                {5,2,8},
                {9,3,7}
            };
            int originalRows = originalArray.GetLength(0);
            int originalCols = originalArray.GetLength(1);
            int[,] transposedArray = new int[originalCols, originalRows];
            for (int i = 0; i < originalRows; i++)
            {
                for (int j = 0; j < originalCols; j++)
                {
                    transposedArray[j, i] = originalArray[i, j];
                }
            }

            Console.WriteLine("Original Array data:");
            for (int i = 0; i < originalArray.GetLength(0); i++)
            {
                for (int j = 0; j < originalArray.GetLength(1); j++)
                {
                    Console.Write(originalArray[i, j] + "\t"); ;
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Transposed Array data:");
            for (int i = 0; i < transposedArray.GetLength(0); i++)
            {
                for (int j = 0; j < transposedArray.GetLength(1); j++)
                {
                    Console.Write(transposedArray[i, j] + "\t"); ;
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //5. The "Game of Life" Style Neighbor Check
            char[,] myBoard = new char[5, 5]
            {
                { '.','.','x','.','.'},
                { '.','x','.','.','.'},
                { 'x','.','x','.','.'},
                { '.','.','.','.','x'},
                { '.','.','.','x','.'}
            };

            int targetedRow;
            int targetedCol;
            int count = 0;
            do
            {
                targetedRow = Program.getValue("row");
                if (targetedRow > 5 || targetedRow < 1)
                    Console.WriteLine("Please enter the row value between 1 to 5");
            } while (targetedRow > 5 || targetedRow < 1);

            do
            {
                targetedCol = Program.getValue("column");
                if (targetedCol > 5 || targetedCol < 1)
                    Console.WriteLine("Please enter the column value between 1 to 5");
            } while (targetedCol > 5 || targetedCol < 1);

            Console.WriteLine($"Checking neighbors for cell at [{targetedRow}, {targetedCol}]...");

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    // Skip the target cell itself (offset 0,0)
                    if (r == 0 && c == 0) continue;

                    int checkRow = targetedRow + r-1;
                    int checkCol = targetedCol + c-1;

                    // THE BOUNDARY GUARD
                    // Ensure the neighbor we are checking is actually inside the array
                    if (checkRow >= 0 && checkRow < myBoard.GetLength(0) &&
                        checkCol >= 0 && checkCol < myBoard.GetLength(1))
                    {
                        // If there is an 'x' at this valid neighbor coordinate, count it
                        Console.WriteLine($"Checking row{checkRow} col{checkCol}... it is {myBoard[checkRow, checkCol]}");
                        if (myBoard[checkRow, checkCol] == 'x')
                        {
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine($"Found {count} neighbors with an 'x'.");
        }

        public static int getValue(string type)
        {
            Console.Write($"Enter the targeted {type} (1-5):");
            int desiredValue = Convert.ToInt32(Console.ReadLine());
            return desiredValue;
        }
    }
}
