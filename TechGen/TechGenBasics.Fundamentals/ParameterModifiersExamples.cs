namespace TechGenBasics.Fundamentals;

/// <summary>
/// ref, out, and in parameter modifiers.
/// </summary>
public static class ParameterModifiersExamples
{
    public static void Run()
    {
        Console.WriteLine("--- ref / out / in ---");

        // ref: must be initialized before call; method can read and write
        var counter = 10;
        Increment(ref counter);
        Console.WriteLine($"After Increment(ref): {counter}");

        // out: caller does not need to initialize; method must assign before return
        if (TryDivide(10, 3, out var quotient, out var remainder))
            Console.WriteLine($"10 / 3 => quotient={quotient}, remainder={remainder}");

        // in: read-only reference (no copy for large structs)
        var point = new Point2D(3, 4);
        Console.WriteLine($"Distance from origin (in): {DistanceFromOrigin(in point)}");

        // Swap with ref
        var a = 1;
        var b = 2;
        Swap(ref a, ref b);
        Console.WriteLine($"After Swap: a={a}, b={b}");
    }

    public static void Increment(ref int value)
    {
        value++;
    }

    public static bool TryDivide(int dividend, int divisor, out int quotient, out int remainder)
    {
        quotient = 0;
        remainder = 0;
        if (divisor == 0)
            return false;

        quotient = dividend / divisor;
        remainder = dividend % divisor;
        return true;
    }

    public static double DistanceFromOrigin(in Point2D p)
    {
        // p.X = 99; // compile error: in is read-only
        return Math.Sqrt(p.X * p.X + p.Y * p.Y);
    }

    public static void Swap(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }

    public readonly struct Point2D
    {
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }
}
