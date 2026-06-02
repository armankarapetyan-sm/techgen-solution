namespace TechGenBasics.Fundamentals;

public static class OverloadingExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Method Overloading ---");
        Console.WriteLine(Print(42));
        Console.WriteLine(Print(3.14));
        Console.WriteLine(Print("hello", 2));
        Console.WriteLine(Area(4));
        Console.WriteLine(Area(4, 6));
    }

    // Same name, different parameter types/count
    public static string Print(int value)
    {
        return $"int: {value}";
    }

    public static string Print(double value)
    {
        return $"double: {value:F2}";
    }

    public static string Print(string text, int times)
    {
        var result = string.Empty;
        for (int i = 0; i < times; i++)
        {
            result = result + text;
        }
        return $"string x{times}: {result}";
    }

    public static int Area(int side)
    {
        return side * side;
    }

    public static int Area(int width, int height)
    {
        return width * height;
    }
}
