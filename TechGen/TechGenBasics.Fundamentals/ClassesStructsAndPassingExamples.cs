namespace TechGenBasics.Fundamentals
{
    /// <summary>
    /// Classes vs structs, passing data, and copy semantics (value vs reference types).
    /// </summary>
    public static class ClassesStructsAndPassingExamples
    {
        public static void Run()
        {
            Console.WriteLine("--- Classes, Structs & Passing Data ---");

            DemonstrateStructCopyOnPass();
            Console.WriteLine();
            DemonstrateClassReferenceOnPass();
            Console.WriteLine();
            DemonstrateStructWithReferenceField();
            Console.WriteLine();
            DemonstrateMutableStructInCollection();
            Console.WriteLine();
            DemonstratePassStructByRef();
        }

        /// <summary>
        /// Structs are value types: the method receives a copy.
        /// Changes inside the method do not affect the caller's variable.
        /// </summary>
        private static void DemonstrateStructCopyOnPass()
        {
            Console.WriteLine("[Struct] Passed by value — method gets a copy");

            MutablePoint point = new MutablePoint(1, 2);
            Console.WriteLine($"  Before TryMove: ({point.X}, {point.Y})");

            TryMove(point, 10, 20);
            Console.WriteLine($"  After TryMove:  ({point.X}, {point.Y})  ← unchanged (copy was modified)");
        }

        /// <summary>
        /// Classes are reference types: the method receives a copy of the reference,
        /// but it still points to the same object on the heap.
        /// </summary>
        private static void DemonstrateClassReferenceOnPass()
        {
            Console.WriteLine("[Class] Passed by value — reference is copied, object is shared");

            Rectangle rect = new Rectangle(4, 5);
            Console.WriteLine($"  Before Scale: width={rect.Width}");

            Scale(rect, 2);
            Console.WriteLine($"  After Scale:  width={rect.Width}  ← changed (same object)");
        }

        /// <summary>
        /// Copying a struct copies its fields. If a field is a reference type,
        /// both copies still share that heap object.
        /// </summary>
        private static void DemonstrateStructWithReferenceField()
        {
            Console.WriteLine("[Struct + reference field] Shallow copy shares heap data");

            LabelledBuffer first = new LabelledBuffer("A", new int[] { 1, 2, 3 });
            LabelledBuffer second = first; // struct assignment = field-wise copy

            second.Buffer[0] = 99;
            Console.WriteLine($"  first.Buffer[0] = {first.Buffer[0]}  ← also 99 (shared array)");
            Console.WriteLine($"  first.Label = '{first.Label}', second.Label = '{second.Label}' (independent strings)");
        }

        /// <summary>
        /// Getting a struct from a collection returns a copy unless you use ref.
        /// This is a common source of bugs with mutable structs.
        /// </summary>
        private static void DemonstrateMutableStructInCollection()
        {
            Console.WriteLine("[Mutable struct in array] Local copy is not the array slot");

            MutablePoint[] points =
            {
                new MutablePoint(0, 0),
                new MutablePoint(5, 5)
            };

            MutablePoint copy = points[0];
            copy.X = 100;
            Console.WriteLine($"  After mutating local copy: points[0] = ({points[0].X}, {points[0].Y})  ← still (0,0)");

            points[0] = copy;
            Console.WriteLine($"  After write-back:          points[0] = ({points[0].X}, {points[0].Y})");
        }

        private static void DemonstratePassStructByRef()
        {
            Console.WriteLine("[Struct + ref] No copy — caller's struct is updated");

            MutablePoint point = new MutablePoint(3, 4);
            MoveInPlace(ref point, 7, 8);
            Console.WriteLine($"  After MoveInPlace(ref): ({point.X}, {point.Y})");
        }

        private static void TryMove(MutablePoint point, int newX, int newY)
        {
            point.X = newX;
            point.Y = newY;
        }

        private static void MoveInPlace(ref MutablePoint point, int newX, int newY)
        {
            point.X = newX;
            point.Y = newY;
        }

        private static void Scale(Rectangle rect, int factor)
        {
            rect.Width = rect.Width * factor;
        }

        /// <summary>Value type — stored on the stack (usually) and copied by assignment/passing.</summary>
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

        /// <summary>Reference type — variable holds a reference; object lives on the heap.</summary>
        public class Rectangle
        {
            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int Width { get; set; }
            public int Height { get; set; }
        }

        /// <summary>
        /// Struct copy duplicates the reference field, not the array itself.
        /// </summary>
        public struct LabelledBuffer
        {
            public LabelledBuffer(string label, int[] buffer)
            {
                Label = label;
                Buffer = buffer;
            }

            public string Label;
            public int[] Buffer;
        }
    }
}
