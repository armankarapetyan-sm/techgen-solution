namespace TechGenBasics.Oop;

public static class InheritanceAndPolymorphism
{
    public static void Run()
    {
        Console.WriteLine("--- Inheritance & Polymorphism ---");

        Animal[] animals =
        {
            new Dog("Rex"),
            new Cat("Mittens")
        };

        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].Speak();
        }

        Employee emp = new Manager("E-42", "Jordan", 12);
        Console.WriteLine($"  {emp.Describe()} — bonus team size: {((Manager)emp).TeamSize}");
    }
}

public abstract class Animal
{
    protected Animal(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public abstract void Speak();
}

public class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public override void Speak()
    {
        Console.WriteLine($"  {Name} says: Woof!");
    }
}

public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override void Speak()
    {
        Console.WriteLine($"  {Name} says: Meow!");
    }
}

public class Employee
{
    public Employee(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }

    public virtual string Describe()
    {
        return $"Employee {Name} ({Id})";
    }
}

public class Manager : Employee
{
    public Manager(string id, string name, int teamSize) : base(id, name)
    {
        TeamSize = teamSize;
    }

    public int TeamSize { get; }

    public override string Describe()
    {
        return $"Manager {Name} ({Id})";
    }
}
