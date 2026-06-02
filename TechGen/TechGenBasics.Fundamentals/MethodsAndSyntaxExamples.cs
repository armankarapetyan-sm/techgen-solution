namespace TechGenBasics.Fundamentals;

/// <summary>
/// Methods, parameters, return types, and basic C# syntax.
/// </summary>
public static class MethodsAndSyntaxExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Methods & Syntax ---");

        Console.WriteLine($"Add(3, 5) = {Add(3, 5)}");

        // Optional parameters & named arguments
        Console.WriteLine(Greet("Arman"));
        Console.WriteLine(Greet("Guest", formal: true));

        // params array
        Console.WriteLine($"Sum params: {Sum(1, 2, 3, 4)}");

        // Multiple return paths
        Console.WriteLine($"Absolute(-7) = {Absolute(-7)}");

        // void method with side effect
        int[] numbers = { 1, 2, 3 };
        DoubleAll(numbers);
        Console.WriteLine($"After DoubleAll: [{string.Join(", ", numbers)}]");
    }

    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static string Greet(string name, bool formal = false)
    {
        return formal ? $"Good day, {name}." : $"Hi, {name}!";
    }

    public static int Sum(params int[] values)
    {
        var total = 0;
        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }
        return total;
    }

    public static int Absolute(int value)
    {
        if (value < 0)
            return -value;
        return value;
    }

    public static void DoubleAll(int[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = values[i] * 2;
        }
    }
}
