namespace TechGenBasics.ElevatorSystem;

// Carries the outcome of one simulation run. All properties are read-only —
// set once in the constructor and never changed — preventing accidental mutation
// after the simulation completes.
//
// Why we need it: returning a plain int from Simulation.Run() would only carry
// the step count. A dedicated type lets us return multiple related values together
// and makes the call site self-documenting (result.Steps, result.PersonCount).
class SimulationResult
{
    public int Steps       { get; }
    public int PersonCount { get; }

    public SimulationResult(int steps, int personCount)
    {
        Steps       = steps;
        PersonCount = personCount;
    }
}