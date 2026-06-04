namespace TechGenBasics.ElevatorSystem;

// OOP: Composition ("has-a" with ownership)
// Building creates its Floors in the constructor and owns them for its entire lifetime.
// Floors cannot exist without a Building — if the Building is gone, so are its Floors.
// This is the key difference from Aggregation, where the contained objects can survive
// independently (see Elevator, which only borrows Persons temporarily).
//
// Why we need Building: it is the container for all floors, letting the rest of the
// system say "give me floor 3" via an indexer rather than managing a raw array.
class Building
{
    public int FloorCount { get; }

    private readonly Floor[] _floors;

    public Floor this[int i] => _floors[i];

    public Building(int floorCount, int capacityPerFloor)
    {
        FloorCount = floorCount;
        _floors    = new Floor[floorCount];
        for (int i = 0; i < floorCount; i++)
            _floors[i] = new Floor(i, capacityPerFloor);
    }
}