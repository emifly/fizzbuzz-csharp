namespace FizzBuzz2.ShopManagement;

public class Shop
{
    private readonly HashSet<Customer> _customers = [];
    private readonly HashSet<Employee> _employees = [];
    private readonly HashSet<Product> _products = [];
    public Inventory Inventory { get; } = new();
    private readonly Dictionary<string, Product> _productRegister = [];

    public Customer WelcomeCustomer(string customerName)
    {
        var newCustomer = new Customer
        {
            Name = customerName,
            Shop = this,
        };
        _customers.Add(newCustomer);
        Console.WriteLine($"Customer {customerName} has entered the store.");
        return newCustomer;
    }

    public void SayGoodbyeToCustomer(Customer customer)
    {
        _customers.Remove(customer);
        Console.WriteLine($"Customer {customer.Name} has left the store.");
    }

    public Employee WelcomeEmployee(string employeeName)
    {
        var newEmployee = new Employee
        {
            Name = employeeName,
            Shop = this,
        };
        _employees.Add(newEmployee);
        Console.WriteLine($"Employee {employeeName} has entered the store.");
        return newEmployee;
    }

    public void AddProduct(string name, decimal price, string supplier)
    {
        var newProduct = new Product
        {
            Name = name,
            Price = price,
            Supplier = supplier,
        };
        if (_products.Add(newProduct))
        {
            _productRegister[name] = newProduct;
            SetProductStock(name, 0);
        }
    }

    public Product RetrieveProductFromShelf(string name)
    {
        var matchingProduct = GetProductByName(name);
        RegisterStockDecrease(name, 1);
        return matchingProduct;
    }

    public Product GetProductByName(string name)
    {
        return _productRegister[name];
    }

    public void RegisterStockIncrease(string productName, int quantity)
    {
        var existingStock = GetProductStock(productName);
        SetProductStock(productName, existingStock + quantity);
    }

    public void RegisterStockDecrease(string productName, int quantity)
    {
        var existingStock = GetProductStock(productName);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(quantity, existingStock);
        SetProductStock(productName, existingStock - quantity);
    }

    public int GetProductStock(string name)
    {
        var matchingProduct = GetProductByName(name);
        return Inventory.Stock[matchingProduct];
    }

    private void SetProductStock(string name, int newStock)
    {
        var matchingProduct = GetProductByName(name);
        Inventory.Stock[matchingProduct] = newStock;
    }
}
