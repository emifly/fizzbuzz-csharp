namespace FizzBuzz2.ShopManagement;

public class Product
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
