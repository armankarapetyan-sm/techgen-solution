namespace TechGenBasics.TuplesAndArrays;

public static class DeconstructionExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Deconstruction ---");

        // Deconstruct value tuple
        var fullName = ("Arman", "Karapetyan");
        var (first, last) = fullName;
        Console.WriteLine($"Deconstructed: {first} {last}");

        // Deconstruct method return
        var (min, max) = TupleExamples.GetMinMax(new[] { 2, 8, 4 });
        Console.WriteLine($"Min={min}, Max={max}");

        // Deconstruct custom type
        var point = new Point(7, 3);
        var (x, y) = point;
        Console.WriteLine($"Point deconstructed: x={x}, y={y}");

        // Discard with _
        var (_, onlyY) = point;
        Console.WriteLine($"Discarded X, kept Y={onlyY}");

        // Deconstruct items from an array using a for loop
        (int Id, string Label)[] items = GetItems();
        for (int i = 0; i < items.Length; i++)
        {
            int id = items[i].Id;
            string label = items[i].Label;
            Console.WriteLine($"  Item {id}: {label}");
        }
    }

    private static (int Id, string Label)[] GetItems()
    {
        return new (int, string)[]
        {
            (1, "Alpha"),
            (2, "Beta")
        };
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}
