namespace TechGenBasics.ElevatorSystem;

// OOP: Null Object Pattern
// A second implementation of ISimulationRenderer that does nothing.
// Without this pattern we'd need "if (renderer != null)" guards scattered throughout
// Simulation. Instead, Simulation always calls the interface — NullRenderer absorbs
// the calls silently, letting the simulation run as pure logic with zero output.
//
// Why we need it: swapping ConsoleRenderer for NullRenderer in Program.cs is the only
// change needed to suppress all output and get a bare SimulationResult.
class NullRenderer : ISimulationRenderer
{
    public void RenderHeader(int floorCount, int capacity, int personCount)      { }
    public void RenderStepHeader(int step, int floor, ElevatorMode mode,
                                 string passengers)                              { }
    public void RenderEvent(string note, ElevatorMode mode)                      { }
    public void RenderOutOfService()                                             { }
    public void RenderDroppedOff(string[] names)                                 { }
    public void RenderBoarded(string[] names)                                    { }
    public void RenderBuilding(Building building, Elevator elevator,
                               Person[] people, int count)                       { }
    public void RenderDone(int totalSteps, int personCount)                      { }
}