//1. Personal Profile
string name;
int age;
decimal accountBalance;
bool premiumAccess;
char middleInitial;

name ="Alice";
age = 30;
accountBalance = 2500.75m;
premiumAccess = true;
middleInitial = 'M';

Console.WriteLine($"Name: {name}," +
    $" Age: {age}, " +
    $"Balance: {accountBalance}, " +
    $"Premium: {premiumAccess}, " +
    $"Middle Initial: {middleInitial}");

//2. The Arithmetic Mismatch
int a = 10;
double b = 3.0;

int result = (int)(a / b);
double correntResult = a / b;
Console.WriteLine($"Incorrect Result: {result}");
Console.WriteLine($"Correct Result: {correntResult}");

int remainder = a % (int)b;
Console.WriteLine($"Remainder: {remainder}");

//3. The Unit Converter
const int freezingPointOffset = 32;
double celsius;
celsius = 26.57;
double fahrenheit = celsius*9/5+freezingPointOffset;
Console.WriteLine($"{celsius.ToString("0.00")}°C is {fahrenheit.ToString("0.00")}°F");

//4. String Interpolation
string productName = "Laptop";
int quantity = 3;
double pricePerUnit = 999.99;
Console.WriteLine($"You have ordered {quantity} units of {productName} at ${pricePerUnit.ToString("0.00")} each. Total cost: ${(quantity * pricePerUnit).ToString("0.00")}");
Console.WriteLine($"You have ordered {quantity} units of {productName} at {pricePerUnit.ToString("C")} each. Total cost: {(quantity * pricePerUnit).ToString("C")}");

//5. The "Var" vs. The World
var itemName = "Apple"; // Inferred as string
var dateOfPurchase = DateTime.Now; // Inferred as DateTime
var Pi=3.14259; // Inferred as double
Console.WriteLine($"Item: {itemName}, Date: {dateOfPurchase}, Pi: {Pi}");

itemName = "Banana"; // Valid
//itemName = 123; // Invalid : Cannot implicitly convert type 'string' to 'int'
Console.WriteLine($"Item: {itemName}");

