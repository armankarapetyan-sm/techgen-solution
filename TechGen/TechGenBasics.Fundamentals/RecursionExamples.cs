namespace TechGenBasics.Fundamentals;

public static class RecursionExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Recursion ---");
        Console.WriteLine($"Factorial(5) = {Factorial(5)}");
        Console.WriteLine($"Fibonacci(7) = {Fibonacci(7)}");
        Console.WriteLine($"SumDigits(12345) = {SumDigits(12345)}");
        Console.WriteLine($"Binary search index: {BinarySearch(new[] { 2, 5, 8, 12, 16, 23 }, 12)}");
    }

    public static int Factorial(int n)
    {
        if (n <= 1)
            return 1;
        return n * Factorial(n - 1);
    }

    public static int Fibonacci(int n)
    {
        if (n <= 1)
            return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public static int SumDigits(int number)
    {
        number = Math.Abs(number);
        if (number < 10)
            return number;
        return number % 10 + SumDigits(number / 10);
    }

    public static int BinarySearch(int[] sorted, int target, int left = 0, int? right = null)
    {
        right ??= sorted.Length - 1;
        if (left > right)
            return -1;

        var mid = left + (right.Value - left) / 2;
        if (sorted[mid] == target)
            return mid;
        if (sorted[mid] < target)
            return BinarySearch(sorted, target, mid + 1, right);
        return BinarySearch(sorted, target, left, mid - 1);
    }
}
