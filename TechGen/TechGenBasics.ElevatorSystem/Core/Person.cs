namespace TechGenBasics.ElevatorSystem;

enum PersonLocation { WaitingOnFloor, InElevator, Arrived }

// Why we need Person: every passenger has a fixed identity (Id), a fixed goal
// (DestinationFloor), and mutable runtime state (CurrentFloor, Location, HasArrived).
// Encapsulation keeps those fields behind property setters so no code can
// accidentally corrupt them — only Elevator and Simulation update them through
// well-defined operations.
class Person
{
    public int            Id               { get; }
    public int            DestinationFloor { get; }
    public int            CurrentFloor     { get; set; }
    public bool           HasArrived       { get; set; }
    public PersonLocation Location         { get; set; } = PersonLocation.WaitingOnFloor;

    public Person(int id, int currentFloor, int destinationFloor)
    {
        Id               = id;
        CurrentFloor     = currentFloor;
        DestinationFloor = destinationFloor;
    }

    public override string ToString() => $"P{Id}->F{DestinationFloor}";
}