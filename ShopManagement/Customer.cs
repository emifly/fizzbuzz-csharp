namespace FizzBuzz2.ShopManagement;

public class Customer
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
