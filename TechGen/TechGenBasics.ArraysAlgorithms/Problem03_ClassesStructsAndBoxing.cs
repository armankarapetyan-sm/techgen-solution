using System;

namespace TechGenBasics.Exercises.Solved
{
    /// <summary>
    /// Problem 4 — Classes vs structs, passing, ref, boxing (reference solution).
    /// </summary>
    public static class Problem03_ClassesStructsAndBoxing
    {
        /// <summary>
        /// Struct passed by value: method receives a copy.
        /// Changing point.X/Y only affects the copy; return false to signal caller unchanged.
        /// </summary>
        public static bool TryMoveByValue(MutablePoint point, int x, int y)
        {
            point.X = x;
            point.Y = y;
            return false;
        }

        /// <summary>
        /// ref passes the actual struct variable — caller is updated.
        /// </summary>
        public static void MoveInPlace(ref MutablePoint point, int x, int y)
        {
            point.X = x;
            point.Y = y;
        }

        /// <summary>
        /// Array indexer on structs returns ref — mutate element without a local copy.
        /// </summary>
        public static void MoveFirstPoint(MutablePoint[] points, int x, int y)
        {
            ref MutablePoint first = ref points[0];
            first.X = x;
            first.Y = y;
        }

        /// <summary>
        /// Class passed by value copies the reference, not the object.
        /// rect still points to the same heap object — Width/Height change visible to caller.
        /// </summary>
        public static void Scale(ResizeableRectangle rect, int factor)
        {
            rect.Width = rect.Width * factor;
            rect.Height = rect.Height * factor;
        }

        /// <summary>
        /// Sum ints directly from int[] — no cast to object, so no boxing.
        /// </summary>
        public static int SumWithoutBoxing(int[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum = sum + values[i];
            }

            return sum;
        }
    }

    /// <summary>Value type — copied when passed to methods by default.</summary>
    public struct MutablePoint
    {
        public MutablePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }

    /// <summary>Reference type — methods receive a reference to the same instance.</summary>
    public class ResizeableRectangle
    {
        public ResizeableRectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
