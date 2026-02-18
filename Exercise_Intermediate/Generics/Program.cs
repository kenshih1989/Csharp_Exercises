using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. The Generic Swapper (The Basics)
            int a = 3;
            int b = 4;
            Console.WriteLine($"Before swap a:{a} b:{b}");
            Swap(ref a, ref b);
            Console.WriteLine($"After swap a:{a} b:{b}");

            string c = "abc";
            string d = "def";
            Console.WriteLine($"Before swap c:{c} d:{d}");
            Swap(ref c, ref d);
            Console.WriteLine($"After swap c:{c} d:{d}");

            //2. The Simple Box (Constraints)
            Box<string>.PrintType();
            Box<Player>.PrintType();
            //Following lines failed to compile:
            //Box<int>.PrintType(); // int is not a reference type but value type
            //Box<bool>.PrintType(); // bool is not a reference type but value type


            //3. Circular Buffer (Data Structures)
            CircularBuffer<int> circularBufferInt = new CircularBuffer<int>(5);
            circularBufferInt.Push(5);
            circularBufferInt.Push(6);
            circularBufferInt.Push(7);
            Console.WriteLine($"The value of pop is: {circularBufferInt.Pop()}");
            Console.WriteLine($"The value of pop is: {circularBufferInt.Pop()}");
            CircularBuffer<string> circularBufferString = new CircularBuffer<string>(3);
            circularBufferString.Push("First");
            circularBufferString.Push("Second");
            circularBufferString.Push("Thrid");
            Console.WriteLine($"The value of pop is: {circularBufferString.Pop()}");
            circularBufferString.Push("Forth");
            Console.WriteLine($"The value of pop is: {circularBufferString.Pop()}");
            Console.WriteLine($"The value of pop is: {circularBufferString.Pop()}");
            Console.WriteLine($"The value of pop is: {circularBufferString.Pop()}");
            Console.WriteLine($"The value of pop is: {circularBufferString.Pop()}"); //return default string (null)

            //4. The Result Wrapper (Real-world API Design)
            // Case A: Success
            Result<int> scoreResult =  Result<int>.Success(100);

            if (scoreResult.IsSuccess)
            {
                Console.WriteLine($"Success! Captured Data: {scoreResult.Data}");
            }

            // Case B: Failure
            Result<string> nameResult = Result<string>.Failure("Database connection lost.");

            if (!nameResult.IsSuccess)
            {
                Console.WriteLine($"Error: {nameResult.ErrorMessage}");
            }

            //5. Generic Repository Pattern (Advanced)
            Repository<Player> playerRepo = new Repository<Player>();

            // Create and add a player
            Player p1 = new Player { Id = 101 };
            Player p2 = new Player { Id = 102 };
            playerRepo.Add(p1);
            playerRepo.Add(p2);

            // Retrieve the player
            var found = playerRepo.GetById(101);
            if (found.IsSuccess)
            {
                Console.WriteLine($"Found: {found.Data.Id}");
            }
            else
            {
                Console.WriteLine($"Error: {found.ErrorMessage}");
            }

            playerRepo.Delete(102);
            playerRepo.Delete(103);



        }

        static void Swap<T>(ref T input1, ref T input2)
        {
            T temp = input1;
            input1 = input2;
            input2 = temp;
        }
    }
    static class Box<T> where T : class
    {
        public static void PrintType()
        {
            Console.WriteLine(typeof(T).Name);
        }
    }

    public class CircularBuffer<T>
    {
        private T[] _buffer;
        private int _head = 0; // Where we read from
        private int _tail = 0; // Where we write to
        private int _count = 0;

        public CircularBuffer(int capacity)
        {
            _buffer = new T[capacity];
        }

        public void Push(T item)
        {
            _buffer[_tail] = item;

            // Move tail forward, wrap to 0 if it hits Length
            _tail = (_tail + 1) % _buffer.Length;

            if (_count < _buffer.Length)
            {
                _count++;
            }
            else
            {
                // If buffer was full, the head must also move 
                // because we just overwrote the oldest data!
                _head = (_head + 1) % _buffer.Length;
            }

        }

        public T Pop()
        {
            if (_count == 0) return default(T);

            T item = _buffer[_head];

            // Move head forward, wrap to 0 if it hits Length
            _head = (_head + 1) % _buffer.Length;
            _count--;

            return item;
        }
    }

    public class Result<T>
    {
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        private Result(T data, bool success, string error)
        {
            Data = data;
            IsSuccess = success;
            ErrorMessage = error;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true, null);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(default, false, message);
        }
    }

    public interface IEntity
    {
        int Id { get; set; }
    }
    class Player : IEntity
    {
        public int Id { get; set; }
    }

    public class Repository<T> where T : class, IEntity, new()
    {
        // A simple list to act as our "Database"
        private List<T> _storage = new List<T>();

        public void Add(T item)
        {
            _storage.Add(item);
            Console.WriteLine($"Added item with ID: {item.Id}");
        }

        public void Delete(int id)
        {
            var searchedTarget = GetById(id);
            if (searchedTarget.IsSuccess)
            {
                _storage.Remove(searchedTarget.Data);
                Console.WriteLine($"Player with Id#{id} had been deleted.");
            }
            else
            {
                Console.WriteLine($"{searchedTarget.ErrorMessage}");
            }
        }

        public Result<T> GetById(int id)
        {
            foreach (var item in _storage)
            {
                if (item.Id == id) return Result<T>.Success(item);
            }
            return Result<T>.Failure($"No entity found with ID {id}");
        }

        
    }


}
