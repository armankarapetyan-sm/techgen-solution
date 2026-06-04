using System.Text;

namespace TechGenBasics.ElevatorSystem;

// Aggregation ("has-a" without ownership)
// Elevator temporarily holds Person references but does not own them — Persons exist
// before they board and continue to exist after they alight. This is Aggregation:
// the Elevator borrows objects whose lifetime it does not control (contrast with
// Building, which owns its Floors via Composition).
//
// Why we need Elevator: it encapsulates all movement logic, capacity rules, and mode
// transitions. No external code can move the elevator to an arbitrary floor, exceed
// capacity, or set an invalid mode — those invariants are enforced inside the class.
class Elevator
{
    public int          CurrentFloor   { get; private set; }
    public int          Capacity       { get; }
    public ElevatorMode Mode           { get; private set; }
    public int          PassengerCount => _count;

    public string Direction => _targetFloor > CurrentFloor ? "UP"
                             : _targetFloor < CurrentFloor ? "DOWN"
                             : "idle";

    private readonly Person[] _passengers;
    private int _count;
    private int _targetFloor;

    public Elevator(int capacity)
    {
        Capacity    = capacity;
        _passengers = new Person[capacity];
        Mode        = ElevatorMode.Normal;
    }

    // Manager action: only allowed when elevator is empty
    public bool SetMaintenance()
    {
        if (_count > 0) return false;
        Mode = ElevatorMode.Maintenance;
        return true;
    }

    // Passenger action: only possible when someone is inside
    public Person? TriggerEmergency()
    {
        if (_count == 0) return null;
        Mode = ElevatorMode.Emergency;
        return _passengers[0];
    }

    public void Resume() => Mode = ElevatorMode.Normal;

    public void SetTarget(int floor) => _targetFloor = floor;

    public void Move()
    {
        if (Mode != ElevatorMode.Normal) return;
        if      (CurrentFloor < _targetFloor) CurrentFloor++;
        else if (CurrentFloor > _targetFloor) CurrentFloor--;
    }

    // Returns names of passengers who left at this floor
    public string[] Unboard()
    {
        var names = new string[_count];
        int n = 0;
        for (int i = 0; i < _count; i++)
        {
            Person p = _passengers[i];
            if (p.DestinationFloor != CurrentFloor) continue;

            p.HasArrived         = true;
            p.CurrentFloor       = CurrentFloor;
            p.Location           = PersonLocation.Arrived;
            names[n++]           = p.ToString();
            _passengers[i]       = _passengers[--_count];
            _passengers[_count]  = null!;
            i--;
        }
        var result = new string[n];
        Array.Copy(names, result, n);
        return result;
    }

    // Returns names of passengers who boarded from this floor
    public string[] Board(Floor floor)
    {
        var names = new string[floor.WaitingCount];
        int n = 0;
        for (int i = 0; i < floor.WaitingCount && _count < Capacity; i++)
        {
            Person p              = floor.Peek(i);
            _passengers[_count++] = p;
            p.Location            = PersonLocation.InElevator;
            names[n++]            = p.ToString();
            floor.RemoveAt(i--);
        }
        var result = new string[n];
        Array.Copy(names, result, n);
        return result;
    }

    public string PassengerSummary()
    {
        if (_count == 0) return "empty";
        var sb = new StringBuilder();
        for (int i = 0; i < _count; i++)
        {
            if (i > 0) sb.Append(", ");
            sb.Append(_passengers[i]);
        }
        return sb.ToString();
    }
}