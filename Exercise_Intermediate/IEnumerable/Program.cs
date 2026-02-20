using System.Collections;

namespace IEnumerable_and_IEnumerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The "Reverse" Iterator
            int[] myArray = new int[5] { 1, 2, 3, 4, 5 };
            ReverseArrayEnumerable myReverseArrayEnumerable = new ReverseArrayEnumerable(myArray);

            List<int> myList = new List<int>();
            foreach (int i in myReverseArrayEnumerable)
            {
                myList.Add(i);
            }

            Console.WriteLine(String.Join(", ", myList));

            //2. The "Skipper" (Every Other Item)
            List<string> letters = new List<string> { "A", "B", "C", "D", "E" };
            EveryOther<string> lettersSkipper = new EveryOther<string>(letters);

            foreach (var item in lettersSkipper)
            {
                Console.Write($"{item} "); // Should print A, C, E
            }

            List<int> integers = new List<int> { 1, 2, 3, 4, 5 };
            EveryOther<int> integersSkipper = new EveryOther<int>(integers);

            foreach (var item in integersSkipper)
            {
                Console.Write($"{item} "); // Should print 1, 3, 5
            }
            Console.WriteLine();

            //3. The Infinite Fibonacci Generator
            FibonacciEnumerable fibonanciEnumerable = new FibonacciEnumerable();

            int counter = 0;
            foreach (var item in fibonanciEnumerable)
            {
                Console.Write($"{item} ");
                counter++;
                if (counter == 10)
                    break;
            }
            Console.WriteLine();

            //4: The "Circular" Buffer
            string[] myStringInputs = new string[2] { "Red", "Blue" };
            CircularIterator<string> myCircularIterator = new CircularIterator<string>(myStringInputs);
            counter = 0;
            foreach (var item in myCircularIterator)
            {
                Console.Write($"{item} ");
                counter++;
                if (counter == 10)
                    break;
            }
            Console.WriteLine();

            //5.The "Filtering" Enumerator (Manual Where)
            int[] myIntegerNumbers = new int[8] { 4, 5, 6, 7, 8, 9, 10, 11 };
            EvenNumbersOnly myEvenNumbersOnly = new EvenNumbersOnly(myIntegerNumbers);
            foreach (int integer in myEvenNumbersOnly)
            {
                Console.Write($"{integer} ");
            }


        }
    }

    public class ReverseArrayEnumerable : IEnumerable<int>
    {
        private int[] _arrayInput;
        public ReverseArrayEnumerable(int[] arrayInput)
        {
            _arrayInput = arrayInput;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new ReverseArrayEnumerator(_arrayInput);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ReverseArrayEnumerator : IEnumerator<int>
    {
        private int[] _arrayInput;
        private int _position = -1; // Start before the first element

        public ReverseArrayEnumerator(int[] arrayInput)
        {
            _arrayInput = arrayInput;
            _position = arrayInput.Length;
        }

        // 1. Move the cursor
        public bool MoveNext()
        {
            _position--;
            return (_position > -1);
        }

        // 2. Get the item at the current cursor
        public int Current => _arrayInput[_position];

        // 3. Boilerplate requirements
        object IEnumerator.Current => Current;
        public void Reset() => _position = _arrayInput.Length;
        public void Dispose() { /* Cleanup if needed */ }

    }

    public class EveryOther<T> : IEnumerable<T>
    {
        private List<T> _list;
        public EveryOther(List<T> list) => _list = list;

        public IEnumerator<T> GetEnumerator()
        {
            return new EveryOtherEnumerator<T>(_list);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class EveryOtherEnumerator<T> : IEnumerator<T>
    {
        private List<T> _list;
        private int _position = -2; // Start before the first element

        public EveryOtherEnumerator(List<T> list) => _list = list;


        //1.Move the cursor
        public bool MoveNext()
        {
            _position += 2;
            return (_position < _list.Count);
        }

        //2. Get the item at the current cursor
        public T Current => _list[_position];

        //3. Boilerplate requirements
        object IEnumerator.Current => Current;
        public void Reset() => _position = -2;
        public void Dispose() { }
    }


    public class FibonacciEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator() => new FibonacciEnumerator();
        IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class FibonacciEnumerator : IEnumerator<int>
    {
        private int _current;
        private int _next;
        private bool _hasStarted;

        public FibonacciEnumerator()
        {
            Reset();
        }
        //1. Move the cursor
        public bool MoveNext()
        {
            if (!_hasStarted)
            {
                _current = 0;
                _next = 1;
                _hasStarted = true;
            }
            else
            {
                int temp = _current + _next;
                _current = _next;
                _next = temp;
            }
            return true;
        }
        // 2. Get the item at the current cursor
        public int Current => _current;
        // 3. Boilerplate requirements
        object IEnumerator.Current => Current;

        public void Reset()
        {
            _current = 0;
            _next = 1;
            _hasStarted = false;
        }

        public void Dispose() { }
    }

    public class CircularIterator<T> : IEnumerable<T>
    {
        private T[] _input;
        public CircularIterator(T[] input) {
            _input = input;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new CircularIteratorEnumerator<T>(_input);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CircularIteratorEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _input;
        private int position = -1;
        public CircularIteratorEnumerator(T[] input)
        {
            _input = input;
        }

        //Move the cursor
        public bool MoveNext()
        {
            //return false if the array is empty
            if (_input.Length == 0) return false;

            //Use modulo operator to repeat
            position = (position + 1) % _input.Length;

            return true;
        }

        // 2. Get the item at the current cursor
        public T Current => _input[position];

        // 3. Boilerplate requirements
        object IEnumerator.Current => Current;
        public void Dispose()
        {
            
        }
        public void Reset() => position = -1;
    }

    public class EvenNumbersOnly : IEnumerable<int>
    {
        private readonly int[] _input;
        public EvenNumbersOnly(int[] input)
        {
            _input = input;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new EvenNumbersOnlyEnumerator(_input);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class EvenNumbersOnlyEnumerator : IEnumerator<int>
    {
        private readonly int[] _input;
        private int _position=-1;

        public EvenNumbersOnlyEnumerator(int[] input)
        {
            _input = input;
        }
        //1. Move the cursor
        public bool MoveNext()
        {
            for (int i = _position + 1; i < _input.Length; i++)
            {
                if (_input[i] % 2 == 0)
                {
                    _position = i;
                    return true; ;
                }
            }
            return false;
        }

        //2. Get the item at the current cursor
        public int Current => _input[_position];

        //3. Boilerplate requirements
        object IEnumerator.Current => Current;
        public void Dispose(){}
        public void Reset() => _position = -1;
    }
}
