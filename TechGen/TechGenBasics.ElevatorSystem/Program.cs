
namespace TechGenBasics.ElevatorSystem;

// Composition Root — the single place where all concrete types are chosen and wired together.
// The rest of the codebase depends only on abstractions (ISimulationRenderer, Building,
// Elevator, etc.). Here we decide which implementations to use and inject them.
//
// OOP: Dependency Injection in practice.
// Swapping ConsoleRenderer for NullRenderer is the only change needed to suppress all
// output — no other class needs to know or care which renderer is active.
class Program
{
    static void Main(string[] args)
    {
        int floors  = ReadInt("Floor count",  min: 2, max: 20, defaultVal: 5);
        int persons = ReadInt("Person count", min: 1, max: floors * 5, defaultVal: 3);

        var building = new Building(floorCount: floors, capacityPerFloor: floors * 2);
        var elevator = new Elevator(capacity: 4);
        var rand     = new Random();

        Person[] people = new Person[persons];
        for (int i = 0; i < persons; i++)
        {
            int start = rand.Next(floors);
            int dest = rand.Next(floors);
            while (dest == start) dest = rand.Next(floors);
            people[i] = new Person(i + 1, currentFloor: start, destinationFloor: dest);
        }

        Console.WriteLine();
        Console.WriteLine("  Generated passengers:");
        for (int i = 0; i < persons; i++)
            Console.WriteLine($"    {people[i]}  (starts at F{people[i].CurrentFloor})");
        Console.WriteLine();

        // Swap ConsoleRenderer for NullRenderer to run with no output (pure logic only)
        ISimulationRenderer renderer = new ConsoleRenderer();
        // ISimulationRenderer renderer = new NullRenderer();

        SimulationResult result = new Simulation(building, elevator, people, people.Length, renderer).Run();

        if (renderer is NullRenderer)
            Console.WriteLine($"  Done in {result.Steps} steps ({result.PersonCount} people).");
    }

    private static int ReadInt(string label, int min, int max, int defaultVal)
    {
        while (true)
        {
            Console.Write($"  {label} [{min}-{max}] (Enter = {defaultVal}): ");
            string? line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) return defaultVal;
            if (int.TryParse(line, out int value) && value >= min && value <= max) return value;

            Console.WriteLine($"  Please enter a number between {min} and {max}.");
        }
    }
}