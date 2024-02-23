using FizzBuzz2.ShopManagement;

var shop = new Shop();

shop.AddProduct("broccoli", 0.6m, "Sheepy Farms");
shop.AddProduct("strawberry and banana smoothie", 3m, "Innocent Smoothies");
shop.AddProduct("olive oil", 3m, "Filippo Berio");

var userInterface = new UserInterface
{
    Shop = shop,
};

userInterface.Run();
