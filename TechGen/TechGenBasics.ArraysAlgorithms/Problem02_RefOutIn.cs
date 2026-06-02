namespace TechGenBasics.ArraysAlgorithms
{
    /// <summary>
    /// Problem 2 — ref, out, and in parameter modifiers (reference solution).
    /// </summary>
    public static class Problem02_RefOutIn
    {
        /// <summary>
        /// out: caller does not need to initialize quotient/remainder;
        /// method must assign them before returning true.
        /// </summary>
        public static bool TryDivide(int dividend, int divisor, out int quotient, out int remainder)
        {
            // out parameters must be assigned on every path
            quotient = 0;
            remainder = 0;

            if (divisor == 0)
            {
                return false;
            }

            quotient = dividend / divisor;
            remainder = dividend % divisor;
            return true;
        }

        /// <summary>
        /// ref: exchanges values in the caller's variables (no return value needed).
        /// </summary>
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// in: read-only reference to a struct — avoids copying a large struct.
        /// </summary>
        public static double DistanceFromOrigin(in Point2D point)
        {
            return Math.Sqrt(point.X * point.X + point.Y * point.Y);
        }
    }

    /// <summary>Read-only struct used with the in modifier.</summary>
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