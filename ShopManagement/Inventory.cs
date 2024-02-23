namespace FizzBuzz2.ShopManagement;

public class Inventory
{
    public Dictionary<Product, int> Stock { get; } = [];

    public override string ToString()
    {
        return string.Join('\n', Stock.Select(productQuantityPair => $"Product {productQuantityPair.Key.Name}: {productQuantityPair.Value} in stock"));
    }
}
