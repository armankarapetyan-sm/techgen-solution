namespace TechGenBasics.ElevatorSystem;

// OOP: Abstraction via Interface
// Defines a contract — the render events Simulation needs — without specifying how
// they are implemented. Simulation depends only on this interface, not on any concrete
// class. That means we can swap ConsoleRenderer for NullRenderer (or any future
// renderer) without touching Simulation at all.
//
// Why we need this interface: it decouples "what needs to be displayed" from "how it
// is displayed". Simulation calls these methods; it doesn't care whether the output
// goes to the console, a file, or nowhere at all.
interface ISimulationRenderer
{
    void RenderHeader(int floorCount, int capacity, int personCount);
    void RenderStepHeader(int step, int floor, ElevatorMode mode, string passengers);
    void RenderEvent(string note, ElevatorMode mode);
    void RenderOutOfService();
    void RenderDroppedOff(string[] names);
    void RenderBoarded(string[] names);
    void RenderBuilding(Building building, Elevator elevator, Person[] people, int count);
    void RenderDone(int totalSteps, int personCount);
}