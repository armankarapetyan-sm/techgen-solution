using System;

namespace TechGenBasics.ElevatorSystem;

// OOP: Dependency Injection + Single Responsibility
// Simulation owns the run loop and the scheduling algorithm, but knows nothing about
// how results are displayed. The renderer is injected through the constructor
// (Dependency Injection), so the same class works whether output goes to the console
// or nowhere at all — no if-statements inside, no knowledge of concrete types.
//
// Why we need it: separating orchestration logic from domain objects and display means
// each class has exactly one reason to change. If the scheduling algorithm changes,
// only Simulation changes. If the display changes, only the renderer changes.
class Simulation
{
    private readonly Building            _building;
    private readonly Elevator            _elevator;
    private readonly Person[]            _people;
    private readonly int                 _count;
    private readonly ISimulationRenderer _renderer;
    private readonly Random              _rand = new();

    public Simulation(Building building, Elevator elevator, Person[] people, int count,
                      ISimulationRenderer renderer)
    {
        _building = building;
        _elevator = elevator;
        _people   = people;
        _count    = count;
        _renderer = renderer;

        for (int i = 0; i < count; i++)
            _building[people[i].CurrentFloor].Enqueue(people[i]);
    }

    public SimulationResult Run()
    {
        _renderer.RenderHeader(_building.FloorCount, _elevator.Capacity, _count);

        int steps = 0;
        while (!AllArrived())
        {
            steps++;

            string? eventNote = ApplyRandomEvent();

            _renderer.RenderStepHeader(steps, _elevator.CurrentFloor, _elevator.Mode,
                                       _elevator.PassengerSummary());

            if (eventNote != null)
                _renderer.RenderEvent(eventNote, _elevator.Mode);

            if (_elevator.Mode != ElevatorMode.Normal)
            {
                _renderer.RenderOutOfService();
                _renderer.RenderBuilding(_building, _elevator, _people, _count);
                continue;
            }

            Floor floor   = _building[_elevator.CurrentFloor];
            var   dropped = _elevator.Unboard();
            var   boarded = _elevator.Board(floor);

            if (dropped.Length > 0) _renderer.RenderDroppedOff(dropped);
            if (boarded.Length > 0) _renderer.RenderBoarded(boarded);

            _elevator.SetTarget(NextTarget());
            _renderer.RenderBuilding(_building, _elevator, _people, _count);
            _elevator.Move();
        }

        _renderer.RenderDone(steps, _count);
        return new SimulationResult(steps, _count);
    }

    // Returns a note describing what event occurred (null = nothing happened)
    private string? ApplyRandomEvent()
    {
        int roll = _rand.Next(100);

        if (roll < 5)
        {
            bool ok = _elevator.SetMaintenance();
            return ok
                ? "Manager put the elevator into MAINTENANCE"
                : "Maintenance request blocked — passengers still aboard";
        }

        if (roll < 10)
        {
            Person? trigger = _elevator.TriggerEmergency();
            return trigger != null
                ? $"{trigger} pressed the EMERGENCY button!"
                : "Emergency request blocked — no one inside to trigger it";
        }

        _elevator.Resume();
        return null;
    }

    private int NextTarget()
    {
        bool canBoard = _elevator.PassengerCount < _elevator.Capacity;

        // Only go pick up waiting people if there is room to board
        if (canBoard)
        {
            for (int i = 0; i < _building.FloorCount; i++)
                if (_building[i].WaitingCount > 0) return i;
        }

        // Deliver passengers already inside the elevator
        for (int i = 0; i < _count; i++)
            if (_people[i].Location == PersonLocation.InElevator)
                return _people[i].DestinationFloor;

        // Elevator is empty but people are still waiting (capacity was the issue above)
        for (int i = 0; i < _building.FloorCount; i++)
            if (_building[i].WaitingCount > 0) return i;

        return 0;
    }

    private bool AllArrived()
    {
        for (int i = 0; i < _count; i++)
            if (!_people[i].HasArrived) return false;
        return true;
    }
}