namespace FizzBuzz2.ShopManagement;

public class Employee
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
