namespace TechGenBasics.TuplesAndArrays;

public static class TupleExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Tuples & ValueTuples ---");

        // ValueTuple (struct, preferred)
        (string Name, int Age) person = ("Bob", 25);
        Console.WriteLine($"ValueTuple: {person.Name}, age {person.Age}");

        // Tuple without names
        var coords = (10, 20);
        Console.WriteLine($"Coords: ({coords.Item1}, {coords.Item2})");

        // Returning a tuple from a method
        var stats = GetMinMax(new[] { 5, 1, 9, 3 });
        Console.WriteLine($"Min={stats.Min}, Max={stats.Max}");
    }

    public static (int Min, int Max) GetMinMax(int[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("Array must not be empty.", nameof(values));

        var min = values[0];
        var max = values[0];
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] < min) min = values[i];
            if (values[i] > max) max = values[i];
        }
        return (min, max);
    }
}
