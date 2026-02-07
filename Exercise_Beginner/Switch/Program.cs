//1. Day of the week
using System.Net.Http.Headers;

Console.Write("Enter a number within 1 to 7 to represent a day:");
int dayNumber = Convert.ToInt32(Console.ReadLine());

switch (dayNumber)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    case 4:
        Console.WriteLine("Thursday");
        break;
    case 5:
        Console.WriteLine("Friday");
        break;
    case 6:
        Console.WriteLine("Saturday");
        break;
    case 7:
        Console.WriteLine("Sunday");
        break;
    default:
        Console.WriteLine("Invalid day number");
        break;
}
//using switch expression (C# 8.0 and later)
string dayName = dayNumber switch
{
    1 => "Monday",
    2 => "Tuesday",
    3 => "Wednesday",
    4 => "Thursday",
    5 => "Friday",
    6 => "Saturday",
    7 => "Sunday",
    _ => "Invalid day number"
};
Console.WriteLine($"Day name using switch expression: {dayName}");

//2. The Multi-Case: Season Finder
Console.Write("Enter the name of a month to find the season:");
string month = Console.ReadLine();

switch (month.ToUpper())
{
    case "DECEMBER":
    case "JANUARY":
    case "FEBRUARY":
        Console.WriteLine("Winter");
        break;
    case "MARCH":
    case "APRIL":
    case "MAY":
        Console.WriteLine("Spring");
        break;
    case "JUNE":
    case "JULY":
    case "AUGUST":
        Console.WriteLine("Summer");
        break;
    case "SEPTEMBER":
    case "OCTOBER":
    case "NOVEMBER":
        Console.WriteLine("Autumn");
        break;
    default:
        Console.WriteLine("Invalid month name");
        break;
}
//using switch expression (C# 8.0 and later)
string season = month.ToUpper() switch
{
    "DECEMBER" or "JANUARY" or "FEBRUARY" => "Winter",
    "MARCH" or "APRIL" or "MAY" => "Spring",
    "JUNE" or "JULY" or "AUGUST" => "Summer",
    "SEPTEMBER" or "OCTOBER" or "NOVEMBER" => "Autumn",
    _ => "Invalid month name"
};
Console.WriteLine($"Season using switch expression: {season}");

//3. The Calculator: Simple Arithmetic
Console.Write("Enter first number:");
double num1 = Convert.ToDouble(Console.ReadLine());
Console.Write("Enter second number:");
double num2 = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter an operator (+, -, *, /):");
char operation = Console.ReadLine()[0];
double result;

switch (operation)
{
    case '+':
        result = num1 + num2;
        break;
    case '-':
        result = num1 - num2;
        break;
    case '*':
        result = num1 * num2;
        break;
    case '/':
        if (num2 != 0)
        {
            result = num1 / num2;
        }
        else
        {
            Console.WriteLine("Error: Division by zero");
            return;
        }
        break;
    default:
        Console.WriteLine("Invalid operator");
        return;
}
Console.WriteLine($"Result for {num1} {operation} {num2} is {result}");

//using switch expression (C# 8.0 and later)
result = operation switch
{
    '+' => num1 + num2,
    '-' => num1 - num2,
    '*' => num1 * num2,
    '/' when num2 != 0 => num1 / num2,
    '/' => throw new DivideByZeroException("Error: Division by zero"),
    _ => throw new InvalidOperationException("Invalid operator")
};

//4. Ralational patterns: Grade Classifier
Console.Write("Enter your score (0-100):");
int score = Convert.ToInt32(Console.ReadLine());
switch (score)
{
    case >= 90 and <= 100:
        Console.WriteLine("Grade: A");
        break;
    case >= 80 and < 90:
        Console.WriteLine("Grade: B");
        break;
    case >= 70 and < 80:
        Console.WriteLine("Grade: C");
        break;
    case >= 60 and < 70:
        Console.WriteLine("Grade: D");
        break;
    case >= 0 and < 60:
        Console.WriteLine("Grade: F");
        break;
    default:
        Console.WriteLine("Invalid score");
        break;
}
//using switch expression (C# 8.0 and later)
string grade = score switch
{
    >= 90 and <= 100 => "A",
    >= 80 and < 90 => "B",
    >= 70 and < 80 => "C",
    >= 60 and < 70 => "D",
    >= 0 and < 60 => "F",
    _ => "Invalid score"
};

//5. Type and Property Patterns: Shipping Cost Calculator
double weight= 0.0;
Console.Write("Enter the type of item (Letter,Box):");
string itemType = Console.ReadLine();
if (itemType.ToUpper() == "BOX")
{
    Console.Write("Enter the weight of the box in kg:");
    weight = Convert.ToDouble(Console.ReadLine());
}
switch (itemType.ToUpper())
{
    case "LETTER":
        Console.WriteLine("Shipping Cost: $2.00");
        break;
    case "BOX":
        if (weight <= 5.0)
        {
            Console.WriteLine("Shipping Cost: $5.00");
        }
        else if (weight > 5.0 )
        {
            Console.WriteLine("Shipping Cost: $10.00");
        }
        break;
    default:
        Console.WriteLine("Invalid item type");
        break;
}


double shippingCost = itemType.ToUpper() switch
{
    "LETTER" => 2.00,
    "BOX" when weight <= 5.0 => 5.00,
    "BOX" when weight > 5.0 => 10.00,
    _ => -1.0 // Invalid case
};
Console.WriteLine(shippingCost != -1.0 ? $"Shipping Cost using switch expression for {itemType}: {shippingCost.ToString("C")}" : "Invalid item type or weight");
