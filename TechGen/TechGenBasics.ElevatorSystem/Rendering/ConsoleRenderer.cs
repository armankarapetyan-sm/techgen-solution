using System;

namespace TechGenBasics.ElevatorSystem;

// OOP: Polymorphism via Interface
// ConsoleRenderer is one concrete implementation of ISimulationRenderer.
// Because Simulation only knows the interface, this class can be replaced at runtime
// with NullRenderer (or any other renderer) without any change to Simulation.
// This is the Open/Closed principle: Simulation is open for extension (add new renderers)
// but closed for modification (its code never changes when we add them).
class ConsoleRenderer : ISimulationRenderer
{
    private const string Bar = "============================================================";

    public void RenderHeader(int floorCount, int capacity, int personCount)
    {
        Console.WriteLine(Bar);
        Console.WriteLine("  ELEVATOR SIMULATION");
        Console.WriteLine($"  Floors: {floorCount}  |  Capacity: {capacity}  |  People: {personCount}");
        Console.WriteLine(Bar);
    }

    public void RenderStepHeader(int step, int floor, ElevatorMode mode, string passengers)
    {
        Console.Write($"  Step {step,3} | Floor {floor} | ");
        WriteMode(mode);
        Console.WriteLine($" | {passengers}");
    }

    public void RenderEvent(string note, ElevatorMode mode)
    {
        Console.ForegroundColor = mode switch
        {
            ElevatorMode.Emergency   => ConsoleColor.Red,
            ElevatorMode.Maintenance => ConsoleColor.Yellow,
            _                        => ConsoleColor.DarkGray,
        };
        Console.WriteLine($"           ! {note}");
        Console.ResetColor();
    }

    public void RenderOutOfService()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("           ~ Elevator out of service — no boarding or alighting this step");
        Console.ResetColor();
    }

    public void RenderDroppedOff(string[] names)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"           - dropped off : {string.Join(", ", names)}");
        Console.ResetColor();
    }

    public void RenderBoarded(string[] names)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"           + boarded     : {string.Join(", ", names)}");
        Console.ResetColor();
    }

    public void RenderBuilding(Building building, Elevator elevator, Person[] people, int count)
    {
        Console.WriteLine();

        for (int f = building.FloorCount - 1; f >= 0; f--)
        {
            Floor floor = building[f];
            bool  isEl  = f == elevator.CurrentFloor;

            Console.Write($"  F{f} ");

            if (isEl)
            {
                Console.ForegroundColor = elevator.Mode switch
                {
                    ElevatorMode.Emergency   => ConsoleColor.Red,
                    ElevatorMode.Maintenance => ConsoleColor.Yellow,
                    _                        => ConsoleColor.Cyan,
                };

                int    pax    = elevator.PassengerCount;
                string dir    = elevator.Direction;
                string status = elevator.Mode switch
                {
                    ElevatorMode.Maintenance => "[=== MAINTENANCE ===]",
                    ElevatorMode.Emergency   => "[===  EMERGENCY  ===]",
                    _ when pax > 0           => $"[=== {pax} pax  {dir,4} ===]",
                    _                        => $"[===  empty  {dir,4} ===]",
                };
                Console.Write(status);
                Console.ResetColor();

                if (elevator.Mode == ElevatorMode.Normal && pax > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  {elevator.PassengerSummary()}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("|                    |");
                Console.ResetColor();

                if (floor.WaitingCount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"  {floor.WaitingCount} waiting: {floor.WaitingSummary()}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
        }

        Console.Write("  Where: ");
        for (int i = 0; i < count; i++)
        {
            if (i > 0) Console.Write("   ");
            WritePersonStatus(people[i]);
        }
        Console.WriteLine("\n");
    }

    public void RenderDone(int totalSteps, int personCount)
    {
        Console.WriteLine(Bar);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  All {personCount} people arrived in {totalSteps} steps.");
        Console.ResetColor();
        Console.WriteLine(Bar);
    }

    private static void WriteMode(ElevatorMode mode)
    {
        (ConsoleColor color, string label) = mode switch
        {
            ElevatorMode.Maintenance => (ConsoleColor.Yellow, "[MAINT]"),
            ElevatorMode.Emergency   => (ConsoleColor.Red,    "[EMERG]"),
            _                        => (ConsoleColor.Green,  "[OK]   "),
        };
        Console.ForegroundColor = color;
        Console.Write(label);
        Console.ResetColor();
    }

    private static void WritePersonStatus(Person p)
    {
        switch (p.Location)
        {
            case PersonLocation.Arrived:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"P{p.Id} arrived at F{p.DestinationFloor}");
                break;
            case PersonLocation.InElevator:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"P{p.Id} in elevator -> F{p.DestinationFloor}");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"P{p.Id} waiting on F{p.CurrentFloor}");
                break;
        }
        Console.ResetColor();
    }
}