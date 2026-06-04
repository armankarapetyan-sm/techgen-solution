namespace TechGenBasics.ElevatorSystem;

// Using an enum makes the three operating states explicit and compiler-checked.
// Without it we'd need raw integers (0 = normal, 1 = maintenance, 2 = emergency),
// which are easy to misuse and impossible for the compiler to validate.
enum ElevatorMode { Normal, Maintenance, Emergency }