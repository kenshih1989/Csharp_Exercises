//1. Gatekeeper
Console.Write("Enter your age:");
int age = Convert.ToInt32(Console.ReadLine());

bool hasParentConsent = true;

if (age >= 18)
{
    Console.WriteLine("Access Granted.");
}
else if (age >= 18 || hasParentConsent)
{
    Console.WriteLine("Access Granted with Consent.");
}
else
{
    Console.WriteLine("Access Denied.");
}

//2. The Grade Definer
Console.Write("Enter your score (0-100):");
int score = Convert.ToInt32(Console.ReadLine());

if (score >=90 && score <=100)
{
    Console.WriteLine("Grade: A");
}
else if (score >=80 && score <90)
{
    Console.WriteLine("Grade: B");
}
else if (score >=70 && score <80)
{
    Console.WriteLine("Grade: C");
}
else if (score >=60 && score <70)
{
    Console.WriteLine("Grade: D");
}
else if (score >=0 && score <60)
{
    Console.WriteLine("Grade: F");
}
else
{
    Console.WriteLine("Invalid Score.");
}

//3. Leap Year Calculator
Console.Write("Enter a year:");
int year = Convert.ToInt32(Console.ReadLine());

if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
{
    Console.WriteLine($"{year} is a Leap Year.");
}
else
{
    Console.WriteLine($"{year} is not a Leap Year.");
}

//4. The Triangle Validator
Console.Write("Enter side A length:");
int sideA = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter side B length:");
int sideB = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter side C length:");
int sideC = Convert.ToInt32(Console.ReadLine());

if (sideA == sideB && sideB == sideC)
{
    Console.WriteLine("The triangle is Equilateral.");
}
else if (sideA == sideB || sideB == sideC || sideA == sideC)
{
    Console.WriteLine("The triangle is Isosceles.");
}
else
{
    Console.WriteLine("The triangle is Scalene.");
}

//5. Login System Simulation
string correctUsername = "Admin";
string correctPassword = "SafePass123";

Console.Write("Enter username:");
string username = Console.ReadLine();
Console.Write("Enter password:");
string password = Console.ReadLine();

if (username == correctUsername && password == correctPassword)
{
    Console.WriteLine("Login Successful.");
}
else if (username == correctUsername)
{
    Console.WriteLine("Incorrect Password.");
}
else if (username != correctUsername)
{
    Console.WriteLine("User not found.");
}

