namespace TechGenBasics.ArraysAlgorithms;

public class Problem05_FloodFiller
{
    private readonly struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public static void FillRecursive(int[,] arr, int startX, int startY, int newValue)
    {
        if (arr == null) return;

        int n = arr.GetLength(0);
        int m = arr.GetLength(1);

        if (startX < 0 || startX >= n || startY < 0 || startY >= m)
        {
            return;
        }

        int currentValue = arr[startX, startY];
        if (currentValue == newValue) return;

        FillRecursive(arr, startX, startY, currentValue, newValue, n, m);
    }

    private static void FillRecursive(int[,] arr, int x, int y, int oldValue, int newValue, int n, int m)
    {
        if (x < 0 || x >= n || y < 0 || y >= m) return;
        if (arr[x, y] != oldValue) return;

        arr[x, y] = newValue;

        FillRecursive(arr, x - 1, y, oldValue, newValue, n, m);
        FillRecursive(arr, x + 1, y, oldValue, newValue, n, m);
        FillRecursive(arr, x, y - 1, oldValue, newValue, n, m);
        FillRecursive(arr, x, y + 1, oldValue, newValue, n, m);
    }

    public static void FillIterative(int[,] arr, int startX, int startY, int newValue)
    {
        if (arr == null) return;
        int n = arr.GetLength(0);
        int m = arr.GetLength(1);

        if (startX < 0 || startX >= n || startY < 0 || startY >= m) return;

        int oldValue = arr[startX, startY];
        if (oldValue == newValue) return;

        int capacity = n * m;
        Point[] queue = new Point[capacity];
        int head = 0;
        int tail = 0;

        arr[startX, startY] = newValue;
        queue[tail++] = new Point(startX, startY);

        while (head < tail)
        {
            Point p = queue[head++];

            TryAdd(p.X - 1, p.Y);
            TryAdd(p.X + 1, p.Y);
            TryAdd(p.X, p.Y - 1);
            TryAdd(p.X, p.Y + 1);
        }

        void TryAdd(int x, int y)
        {
            if (x < 0 || x >= n || y < 0 || y >= m) return;
            if (arr[x, y] != oldValue) return;

            arr[x, y] = newValue;
            queue[tail++] = new Point(x, y);
        }
    }
}