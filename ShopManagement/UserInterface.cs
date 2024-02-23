namespace FizzBuzz2.ShopManagement;

public class UserInterface
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
