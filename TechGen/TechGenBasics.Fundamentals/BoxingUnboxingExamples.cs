using System.Collections;

namespace TechGenBasics.Fundamentals
{
    /// <summary>
    /// Boxing and unboxing: value types stored as heap objects, and common pitfalls.
    /// </summary>
    public static class BoxingUnboxingExamples
    {
        public static void Run()
        {
            Console.WriteLine("--- Boxing & Unboxing ---");

            DemonstrateSimpleBoxAndUnbox();
            Console.WriteLine();
            DemonstrateStructAndInterfaceBoxing();
            Console.WriteLine();
            DemonstrateUnboxFailures();
            Console.WriteLine();
            DemonstrateEqualityPitfall();
            Console.WriteLine();
            DemonstrateBoxedStructIsACopy();
            Console.WriteLine();
            DemonstrateNullableBoxing();
            Console.WriteLine();
            DemonstrateImplicitBoxing();
            Console.WriteLine();
            DemonstratePerformanceIssue();
        }

        /// <summary>
        /// Boxing: value type -> object (heap allocation + copy).
        /// Unboxing: object -> value type (cast + copy).
        /// </summary>
        private static void DemonstrateSimpleBoxAndUnbox()
        {
            Console.WriteLine("[Simple] int -> object -> int");

            int value = 42;
            object boxed = value; // boxing: copy 42 onto the heap as System.Int32

            Console.WriteLine($"  boxed type: {boxed.GetType().Name}");
            Console.WriteLine($"  boxed value: {boxed}");

            int unboxed = (int)boxed; // unboxing: copy back to stack
            Console.WriteLine($"  unboxed: {unboxed}");
        }

        /// <summary>
        /// Structs box when converted to object or when passed as an interface type.
        /// </summary>
        private static void DemonstrateStructAndInterfaceBoxing()
        {
            Console.WriteLine("[Struct] Boxing struct and interface dispatch");

            Counter counter = new Counter(10);
            object boxedStruct = counter; // entire struct copied to heap
            Console.WriteLine($"  boxed struct type: {boxedStruct.GetType().Name}");

            Counter restored = (Counter)boxedStruct;
            Console.WriteLine($"  unboxed Count: {restored.Count}");

            // Assigning struct to interface variable boxes the value on the heap
            IIncrementable viaInterface = counter;
            viaInterface.Increment();
            Console.WriteLine(
                $"  After Increment via interface: counter.Count={counter.Count} (unchanged — call used boxed copy)");
            Console.WriteLine(
                $"  GetType still reports struct name: {viaInterface.GetType().Name} (boxed values keep their type name)");
        }

        /// <summary>
        /// Unboxing requires the exact type. Wrong cast throws InvalidCastException.
        /// </summary>
        private static void DemonstrateUnboxFailures()
        {
            Console.WriteLine("[Pitfalls] Invalid unbox and null");

            object boxedInt = 100;

            try
            {
                double wrong = (double)boxedInt; // InvalidCastException
                Console.WriteLine($"  Should not reach here: {wrong}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"  (int) boxed as double: InvalidCastException — {ex.Message.Split('.')[0]}");
            }

            object? nullBox = null;
            try
            {
                int fromNull = (int)nullBox; // NullReferenceException
                Console.WriteLine($"  Should not reach here: {fromNull}");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("  Unboxing null: NullReferenceException");
            }

            // Safe pattern with pattern matching
            if (boxedInt is int n)
            {
                Console.WriteLine($"  Pattern match succeeded: {n}");
            }
        }

        /// <summary>
        /// Two boxed integers with the same value are different object references.
        /// == on object compares references, not numeric value.
        /// </summary>
        private static void DemonstrateEqualityPitfall()
        {
            Console.WriteLine("[Pitfall] == on boxed values compares references");

            object boxA = 1;
            object boxB = 1;

            Console.WriteLine($"  boxA == boxB: {boxA == boxB}  ← often false (different heap objects)");
            Console.WriteLine($"  (int)boxA == (int)boxB: {(int)boxA == (int)boxB}  ← true after unboxing");

            int localA = 1;
            int localB = 1;
            Console.WriteLine($"  local ints == : {localA == localB}  ← no boxing, value comparison");
        }

        /// <summary>
        /// Unboxing yields a copy. Mutating the copy does not change the boxed object.
        /// </summary>
        private static void DemonstrateBoxedStructIsACopy()
        {
            Console.WriteLine("[Pitfall] Unboxing a struct gives a copy");

            ClassesStructsAndPassingExamples.MutablePoint original =
                new ClassesStructsAndPassingExamples.MutablePoint(1, 2);
            object boxed = original;

            ClassesStructsAndPassingExamples.MutablePoint copy =
                (ClassesStructsAndPassingExamples.MutablePoint)boxed;
            copy.X = 999;

            ClassesStructsAndPassingExamples.MutablePoint stillInBox =
                (ClassesStructsAndPassingExamples.MutablePoint)boxed;
            Console.WriteLine(
                $"  After mutating unboxed copy: boxed element is still ({stillInBox.X}, {stillInBox.Y})");
            Console.WriteLine($"  original local: ({original.X}, {original.Y})");
        }

        /// <summary>
        /// Nullable value types: null stays null; has value boxes the underlying type.
        /// </summary>
        private static void DemonstrateNullableBoxing()
        {
            Console.WriteLine("[Nullable] int? boxing behavior");

            int? hasValue = 7;
            int? noValue = null;

            object boxedSeven = hasValue; // boxes 7, not the Nullable wrapper
            object? boxedNull = noValue; // stays null, no boxing of int

            Console.WriteLine($"  boxedSeven: {boxedSeven}, type {boxedSeven.GetType().Name}");
            Console.WriteLine($"  boxedNull == null: {boxedNull is null}");

            // boxedSeven = null;
            int? restored = (int?)boxedSeven;
            Console.WriteLine($"  Unbox to int?: {restored}");
            // int restoredI = (int)boxedSeven;
            // Console.WriteLine($"  Unbox to int: {restoredI}");
        }

        /// <summary>
        /// Many APIs take object and cause hidden boxing (params, non-generic collections, object[]).
        /// </summary>
        private static void DemonstrateImplicitBoxing()
        {
            Console.WriteLine("[Implicit] Hidden boxing in common APIs");

            // Non-generic collection boxes every int
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            Console.WriteLine($"  ArrayList[0] type: {list[0]!.GetType().Name} (boxed on Add)");

            // params object[] boxes each argument
            LogValues(10, 20, 30);

            // Passing value type where object is expected
            PrintObject(42);

            // int[] stores values directly — no boxing per element
            int[] numbers = { 1, 2, 3 };
            Console.WriteLine($"  int[0] = {numbers[0]} (no heap box per element)");
        }

        /// <summary>
        /// Repeated boxing in a loop allocates many short-lived objects (GC pressure).
        /// </summary>
        private static void DemonstratePerformanceIssue()
        {
            Console.WriteLine("[Performance] Boxing in a loop vs int[]");

            const int iterations = 100_000;

            long boxedTicks = MeasureBoxingLoop(iterations);
            long arrayTicks = MeasureIntArrayLoop(iterations);

            Console.WriteLine(
                $"  {iterations:N0} iterations — boxing loop: {boxedTicks} ticks, int[]: {arrayTicks} ticks");
            Console.WriteLine("  Prefer typed arrays (int[]) over object/ArrayList to avoid boxing value types.");
        }

        private static void LogValues(params object[] values)
        {
            Console.Write("  LogValues(params object[]): types = ");
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                Console.Write(values[i].GetType().Name);
            }

            Console.WriteLine();
        }

        private static void PrintObject(object value)
        {
            Console.WriteLine($"  PrintObject received {value} as {value.GetType().Name}");
        }

        private static long MeasureBoxingLoop(int iterations)
        {
            long start = DateTime.UtcNow.Ticks;
            RunBoxingLoop(iterations);
            long end = DateTime.UtcNow.Ticks;
            return end - start;
        }

        private static void RunBoxingLoop(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                object boxed = i;
                int back = (int)boxed;
                if (back < 0)
                {
                    Console.Write("never");
                }
            }
        }

        private static long MeasureIntArrayLoop(int iterations)
        {
            RunIntArrayLoop(iterations);
            long start = DateTime.UtcNow.Ticks;
            RunIntArrayLoop(iterations);
            long end = DateTime.UtcNow.Ticks;
            return end - start;
        }

        private static void RunIntArrayLoop(int iterations)
        {
            int[] numbers = new int[iterations];
            for (int i = 0; i < iterations; i++)
            {
                numbers[i] = i;
            }
        }

        public struct Counter : IIncrementable
        {
            public Counter(int count)
            {
                Count = count;
            }

            public int Count;

            public void Increment()
            {
                Count++;
            }
        }

        public interface IIncrementable
        {
            void Increment();
        }
    }
}