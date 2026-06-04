using System.Text;

namespace TechGenBasics.ElevatorSystem;

// OOP: Encapsulation — the internal waiting array is completely hidden.
// Callers use Enqueue / Peek / RemoveAt rather than touching the array directly,
// so the storage strategy can change without breaking any other class.
//
// Why we need Floor: a floor is the place where passengers wait before boarding.
// Grouping the waiting queue inside Floor means each floor owns and manages its
// own queue — no external code can put someone on the wrong floor or corrupt the count.
class Floor
{
    public int Number       { get; }
    public int WaitingCount => _count;

    private readonly Person[] _waiting;
    private int _count;

    public Floor(int number, int capacity)
    {
        Number   = number;
        _waiting = new Person[capacity];
    }

    public void   Enqueue(Person p) => _waiting[_count++] = p;
    public Person Peek(int i)       => _waiting[i];

    public void RemoveAt(int i)
    {
        _waiting[i]      = _waiting[--_count];
        _waiting[_count] = null!;
    }

    public string WaitingSummary()
    {
        if (_count == 0) return "";
        var sb = new StringBuilder();
        for (int i = 0; i < _count; i++)
        {
            if (i > 0) sb.Append(", ");
            sb.Append(_waiting[i]);
        }
        return sb.ToString();
    }
}