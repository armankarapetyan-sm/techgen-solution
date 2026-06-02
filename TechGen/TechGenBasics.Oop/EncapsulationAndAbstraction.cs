namespace TechGenBasics.Oop;

/// <summary>Encapsulation and abstraction via abstract base class and interface.</summary>
public static class EncapsulationAndAbstraction
{
    public static void Run()
    {
        Console.WriteLine("--- Encapsulation & Abstraction ---");

        INotifier email = new EmailNotifier("smtp.example.com");
        INotifier sms = new SmsNotifier();

        email.Send("Your order shipped.");
        sms.Send("Code: 4821");

        Shape[] shapes = { new Rectangle(4, 5), new CircleShape(2) };
        for (int i = 0; i < shapes.Length; i++)
        {
            Console.WriteLine($"  {shapes[i].GetType().Name} area = {shapes[i].Area():F2}");
        }
    }
}

public interface INotifier
{
    void Send(string message);
}

public class EmailNotifier : INotifier
{
    private readonly string _smtpHost;

    public EmailNotifier(string smtpHost)
    {
        _smtpHost = smtpHost;
    }

    public void Send(string message)
    {
        Console.WriteLine($"  [Email via {_smtpHost}] {message}");
    }
}

public class SmsNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"  [SMS] {message}");
    }
}

public abstract class Shape
{
    public abstract double Area();
}

public class Rectangle : Shape
{
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double Width { get; }
    public double Height { get; }

    public override double Area()
    {
        return Width * Height;
    }
}

public class CircleShape : Shape
{
    public CircleShape(double radius)
    {
        Radius = radius;
    }

    public double Radius { get; }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}
