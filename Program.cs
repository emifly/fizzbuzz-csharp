var shop = new Shop();

shop.AddProduct("broccoli", 0.6m, "Sheepy Farms");
shop.AddProduct("strawberry and banana smoothie", 3m, "Innocent Smoothies");
shop.AddProduct("olive oil", 3m, "Filippo Berio");

var userInterface = new UserInterface
{
    Shop = shop,
};

userInterface.Run();

class UserInterface
{
    public required Shop Shop { private get; init; }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("*****");
            Console.WriteLine("A person enters the shop.");
            Console.Write("What is the person's name? ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Is the person a customer or an employee? ");
            if (Enum.TryParse<Role>(Console.ReadLine(), ignoreCase: true, out var role))
            {
                if (role == Role.Employee)
                {
                    var employee = Shop.WelcomeEmployee(name);
                    Console.WriteLine("*****");
                    Console.WriteLine("[1] Check a product's stock");
                    Console.WriteLine("[2] Register new stock");
                    Console.WriteLine("[3] Do a stock take");
                    Console.Write($"Enter the number of the action {name} would like to take: ");
                    var action = Console.ReadLine() ?? "";
                    switch (action)
                    {
                        case "1":
                            Console.Write($"Which product's stock would {name} like to check? ");
                            var productToCheck = Console.ReadLine() ?? "";
                            employee.CheckStockOfProduct(productToCheck);
                            break;

                        case "2":
                            Console.Write($"Which product would {name} like to register new stock for? ");
                            var productToRegisterStockFor = Console.ReadLine() ?? "";
                            Console.Write($"What quantity of stock would {name} like to add? ");
                            if (int.TryParse(Console.ReadLine(), out var quantityToRegister))
                            {
                                employee.IncreaseProductStockByQuantity(productToRegisterStockFor, quantityToRegister);
                            }
                            else
                            {
                                Console.WriteLine("Sorry, that isn't a valid quantity.");
                            }
                            break;

                        case "3":
                            employee.DoStockTake();
                            break;

                        default:
                            Console.WriteLine("Sorry, that wasn't one of the options.");
                            break;
                    }
                }
                else if (role == Role.Customer)
                {
                    var customer = Shop.WelcomeCustomer(name);
                    while (true)
                    {
                        Console.WriteLine("*****");
                        Console.WriteLine("[1] Add an item to the basket");
                        Console.WriteLine("[2] Check out");
                        Console.Write($"Enter the number of the action {name} would like to take: ");
                        var action = Console.ReadLine() ?? "";
                        switch (action)
                        {
                            case "1":
                                Console.Write($"Which product would {name} like to add? ");
                                var productToAdd = Console.ReadLine() ?? "";
                                customer.TakeProduct(productToAdd);
                                continue;

                            case "2":
                                customer.CheckOut();
                                break;

                            default:
                                Console.WriteLine("Sorry, that wasn't one of the options.");
                                break;
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Sorry, that wasn't one of the options.");
            }
        }
    }
}

class Product
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required string Supplier { get; init; }

    public override bool Equals(object? obj)
    {
        return obj is Product product && product.Name == Name && product.Supplier == Supplier;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Supplier);
    }
}

class Basket
{
    private readonly List<Product> _products = [];

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal CalculateBill()
    {
        return _products.Sum(product => product.Price);
    }
}

class Customer
{
    public required string Name { get; set; }
    public required Shop Shop { private get; init; }
    private readonly Basket _basket = new();

    public void TakeProduct(string productName)
    {
        Console.WriteLine($"Customer {Name} attempts to take product {productName}...");
        try
        {
            var matchingProduct = Shop.RetrieveProductFromShelf(productName);
            _basket.AddProduct(matchingProduct);
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine($"Product {productName} was not added to customer {Name}'s basket because it was not found.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Product {productName} was not added to customer {Name}'s basket because it was out of stock.");
        }
    }

    public void CheckOut()
    {
        Console.WriteLine($"Customer {Name} is checking out...");
        Console.WriteLine($"Customer {Name} pays their bill of {_basket.CalculateBill()}.");
        Shop.SayGoodbyeToCustomer(this);
    }
}

class Employee
{
    public required string Name { get; set; }
    public required Shop Shop { private get; init; }

    public void CheckStockOfProduct(string productName)
    {
        Console.WriteLine($"Employee {Name} is checking the stock of product {productName}...");
        try
        {
            var productStock = Shop.GetProductStock(productName);
            Console.WriteLine($"There are currently {productStock} of product {productName} in stock.");
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine($"Product {productName} was not found.");
        }
    }

    public void DoStockTake()
    {
        Console.WriteLine($"Employee {Name} is doing a stock take...");
        Console.WriteLine(Shop.Inventory);
    }

    public void IncreaseProductStockByQuantity(string productName, int quantity)
    {
        Shop.RegisterStockIncrease(productName, quantity);
    }
}

class Inventory
{
    public Dictionary<Product, int> Stock { get; } = [];

    public override string ToString()
    {
        return string.Join('\n', Stock.Select(productQuantityPair => $"Product {productQuantityPair.Key.Name}: {productQuantityPair.Value} in stock"));
    }
}

class Shop
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

enum Role
{
    Customer,
    Employee,
}
