namespace TechGenBasics.ClassesAndProperties;

/// <summary>
/// Different kinds of classes: static, sealed, nested.
/// </summary>
public static class ClassVarietyExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Different Class Types ---");

        Console.WriteLine($"MathHelper.Double(5) = {MathHelper.Double(5)}");

        Circle circle = new Circle(3);
        Console.WriteLine($"Circle r=3 area={circle.Area():F2}");

        UserProfile user = new UserProfile(1, "dev@example.com");
        user.Email = "new@example.com";
        Console.WriteLine($"UserProfile: id={user.Id}, email={user.Email}");

        Container outer = new Container();
        outer.Inner.SetMessage("Nested class works");
        Console.WriteLine(outer.Inner.Message);
    }
}

public static class MathHelper
{
    public static int Double(int n)
    {
        return n * 2;
    }
}

public sealed class Circle
{
    private readonly double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double Radius
    {
        get { return _radius; }
    }

    public double Area()
    {
        return Math.PI * _radius * _radius;
    }
}

public class UserProfile
{
    public UserProfile(int id, string email)
    {
        Id = id;
        Email = email;
    }

    public int Id { get; }
    public string Email { get; set; }
}

public class Container
{
    private readonly InnerHelper _inner = new InnerHelper();

    public InnerHelper Inner
    {
        get { return _inner; }
    }

    public class InnerHelper
    {
        private string _message = string.Empty;

        public string Message
        {
            get { return _message; }
        }

        public void SetMessage(string msg)
        {
            _message = msg;
        }
    }
}
