namespace TechGenBasics.TuplesAndArrays;

/// <summary>
/// One-dimensional, multi-dimensional, and jagged arrays with small problems.
/// </summary>
public static class ArrayProblems
{
    public static void Run()
    {
        Console.WriteLine("--- Arrays ---");

        RunOneDimensional();
        RunMultiDimensional();
        RunJagged();
    }

    private static void RunOneDimensional()
    {
        Console.WriteLine("[1D] Reverse & find max");
        int[] nums = { 3, 1, 4, 1, 5, 9 };
        Reverse(nums);
        Console.WriteLine($"  Reversed: [{string.Join(", ", nums)}]");
        Console.WriteLine($"  Max: {FindMax(nums)}");
    }

    private static void RunMultiDimensional()
    {
        Console.WriteLine("[2D] Matrix sum & transpose");
        int[,] matrix =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        Console.WriteLine($"  Sum of elements: {SumMatrix(matrix)}");
        PrintMatrix(Transpose(matrix));
    }

    private static void RunJagged()
    {
        Console.WriteLine("[Jagged] Rows of different lengths — flatten & row sums");
        int[][] jagged =
        {
            new[] { 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6 }
        };

        Console.WriteLine($"  Flattened: [{string.Join(", ", Flatten(jagged))}]");
        for (var i = 0; i < jagged.Length; i++)
            Console.WriteLine($"  Row {i} sum: {RowSum(jagged[i])}");
    }

    public static void Reverse(int[] array)
    {
        for (int left = 0, right = array.Length - 1; left < right; left++, right--)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }

    public static int FindMax(int[] array)
    {
        if (array.Length == 0)
            throw new ArgumentException("Array is empty.", nameof(array));

        var max = array[0];
        for (var i = 1; i < array.Length; i++)
            if (array[i] > max)
                max = array[i];
        return max;
    }

    public static int SumMatrix(int[,] matrix)
    {
        var sum = 0;
        for (var row = 0; row < matrix.GetLength(0); row++)
        for (var col = 0; col < matrix.GetLength(1); col++)
            sum += matrix[row, col];
        return sum;
    }

    public static int[,] Transpose(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var result = new int[cols, rows];
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < cols; c++)
            result[c, r] = matrix[r, c];
        return result;
    }

    public static int[] Flatten(int[][] jagged)
    {
        int count = 0;
        for (int i = 0; i < jagged.Length; i++)
        {
            count = count + jagged[i].Length;
        }

        int[] result = new int[count];
        int index = 0;
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                result[index] = jagged[i][j];
                index++;
            }
        }

        return result;
    }

    public static int RowSum(int[] row)
    {
        var sum = 0;
        for (int i = 0; i < row.Length; i++)
        {
            sum += row[i];
        }
        return sum;
    }

    private static void PrintMatrix(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        Console.WriteLine("  Transposed:");
        for (var r = 0; r < rows; r++)
        {
            var line = new int[cols];
            for (var c = 0; c < cols; c++)
                line[c] = matrix[r, c];
            Console.WriteLine($"    [{string.Join(", ", line)}]");
        }
    }
}
