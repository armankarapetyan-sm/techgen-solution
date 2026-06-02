namespace TechGenBasics.ClassesAndProperties;

public static class PropertyExamples
{
    public static void Run()
    {
        Console.WriteLine("--- Properties ---");

        var product = new Product("Keyboard", 79.99m);
        product.Quantity = 3;
        Console.WriteLine($"{product.Name}: unit={product.UnitPrice:C}, total={product.TotalPrice:C}");

        var account = new BankAccount("ACC-001", 100m);
        account.Deposit(50);
        try
        {
            account.Deposit(-10);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation caught: {ex.Message}");
        }
        Console.WriteLine($"Balance: {account.Balance:C}");

        var settings = new AppSettings { Theme = "Dark", MaxRetries = 5 };
        Console.WriteLine($"Settings: theme={settings.Theme}, retries={settings.MaxRetries}");
    }
}

/// <summary>Auto-property with computed property.</summary>
public class Product
{
    public Product(string name, decimal unitPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
    }

    public string Name { get; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public decimal TotalPrice
    {
        get { return UnitPrice * Quantity; }
    }
}

/// <summary>Property with private backing field and validation.</summary>
public class BankAccount
{
    private decimal _balance;

    public BankAccount(string number, decimal initialBalance)
    {
        Number = number;
        _balance = initialBalance;
    }

    public string Number { get; }

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));
        Balance += amount;
    }
}

/// <summary>Simple class with auto-properties and defaults.</summary>
public class AppSettings
{
    public string Theme { get; set; } = "Light";
    public int MaxRetries { get; set; } = 3;
}
