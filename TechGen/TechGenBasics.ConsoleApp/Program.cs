using TechGenBasics.ClassesAndProperties;
using TechGenBasics.Fundamentals;
using TechGenBasics.Oop;
using TechGenBasics.TuplesAndArrays;

namespace TechGenBasics.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("TechGen Basics — C# learning examples");
            Console.WriteLine("=====================================\n");

            while (true)
            {
                PrintMenu();
                Console.Write("\nSelect module (0 = exit): ");
                string? input = Console.ReadLine();

                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        FundamentalsRunner.RunAll();
                        break;
                    case "2":
                        TuplesAndArraysRunner.RunAll();
                        break;
                    case "3":
                        ClassesAndPropertiesRunner.RunAll();
                        break;
                    case "4":
                        OopRunner.RunAll();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye.");
                        return;
                    default:
                        Console.WriteLine("Unknown option. Try again.");
                        break;
                }

                Console.WriteLine("\n--- Press Enter to return to menu ---");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Fundamentals (methods, classes/structs, boxing, ref/out/in, ...)");
            Console.WriteLine("2. Tuples & Arrays (tuples, deconstruction, 1D/2D/jagged)");
            Console.WriteLine("3. Classes & Properties");
            Console.WriteLine("4. OOP (encapsulation, abstraction, inheritance, polymorphism)");
            Console.WriteLine("5. Relationships (association, aggregation, composition, ...)");
            Console.WriteLine("6. C# exercises (how to run tests)");
            Console.WriteLine("7. Arrays & algorithms exercises");
            Console.WriteLine("8. Iterators (IEnumerable, IEnumerator, foreach, boxing)");
            Console.WriteLine("9. Advanced OOP exercise (interfaces, abstractions, polymorphism)");
            Console.WriteLine("0. Exit");
        }
    }
}