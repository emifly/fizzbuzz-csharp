namespace FizzBuzz2.ShopManagement;

public class Basket
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
