using Part1_ProceduralToOOP;

var system = new OrderSystem();
SeedSampleData(system);
RunDemoScenario(system);

PrintCustomers(system);
PrintProducts(system);
PrintAllOrders(system);
Console.WriteLine($"\nPaid sales total after demo: {system.TotalPaidSales():F2}");

RunInteractiveMenu(system);

static void SeedSampleData(OrderSystem system)
{
    system.AddCustomer(new Customer(1, "Mona Ali", "mona@example.com", "Cairo", true));
    system.AddCustomer(new Customer(2, "Omar Hassan", "omar@example.com", "Alexandria", false));
    system.AddCustomer(new Customer(3, "Sara Nabil", "sara@example.com", "Giza", false));

    system.AddProduct(new Product(101, "USB Cable", 50.0m, 100));
    system.AddProduct(new Product(102, "Wireless Mouse", 250.0m, 40));
    system.AddProduct(new Product(103, "Mechanical Keyboard", 1200.0m, 15));
    system.AddProduct(new Product(104, "Laptop Stand", 400.0m, 25));
}

static void RunDemoScenario(OrderSystem system)
{
    Order order1 = system.CreateOrder(1001, 1, "2026-09-15");
    order1.AddLine(system.FindProduct(101)!, 2);
    order1.AddLine(system.FindProduct(102)!, 1);
    order1.MarkPaid();

    Order order2 = system.CreateOrder(1002, 2, "2026-09-15");
    order2.AddLine(system.FindProduct(103)!, 1);
    order2.AddLine(system.FindProduct(104)!, 1);

    Order order3 = system.CreateOrder(1003, 3, "2026-09-16");
    order3.AddLine(system.FindProduct(101)!, 5);
    order3.MarkPaid();
}

static void PrintCustomers(OrderSystem system)
{
    Console.WriteLine($"\n=== CUSTOMERS ({system.Customers.Count}) ===");
    foreach (Customer customer in system.Customers)
        Console.WriteLine(customer);
}

static void PrintProducts(OrderSystem system)
{
    Console.WriteLine($"\n=== PRODUCTS ({system.Products.Count}) ===");
    foreach (Product product in system.Products)
        Console.WriteLine(product);
}

static void PrintAllOrders(OrderSystem system)
{
    Console.WriteLine($"\n=== ALL ORDERS ({system.Orders.Count}) ===");
    foreach (Order order in system.Orders)
        PrintOrder(order);
}

static void PrintOrder(Order order)
{
    Console.WriteLine($"\n=== ORDER #{order.Id} ===");
    Console.WriteLine($"Date: {order.Date}");
    Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
    Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");
    Console.WriteLine("Lines:");

    foreach (OrderLine line in order.Lines)
    {
        Console.WriteLine(
            $" - {line.Product.Name} x{line.Quantity} @{line.Product.Price:F2} = {line.LineTotal:F2}");
    }

    Console.WriteLine($"TOTAL: {order.Total:F2}");
}

static void RunInteractiveMenu(OrderSystem system)
{
    int choice;

    do
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");

        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid choice.");
            continue;
        }

        try
        {
            switch (choice)
            {
                case 1:
                    PrintCustomers(system);
                    break;
                case 2:
                    PrintProducts(system);
                    break;
                case 3:
                    PrintAllOrders(system);
                    break;
                case 4:
                    Console.Write("Order id: ");
                    PrintOrder(system.FindOrder(int.Parse(Console.ReadLine() ?? ""))
                        ?? throw new InvalidOperationException("Order not found."));
                    break;
                case 5:
                    Console.Write("Order id: ");
                    int orderId = int.Parse(Console.ReadLine() ?? "");
                    Console.Write("Customer id: ");
                    int customerId = int.Parse(Console.ReadLine() ?? "");
                    Console.Write("Date (YYYY-MM-DD): ");
                    string date = Console.ReadLine() ?? "";
                    system.CreateOrder(orderId, customerId, date);
                    Console.WriteLine("Order created.");
                    break;
                case 6:
                    Console.Write("Order id: ");
                    Order order = system.FindOrder(int.Parse(Console.ReadLine() ?? ""))
                        ?? throw new InvalidOperationException("Order not found.");
                    Console.Write("Product id: ");
                    Product product = system.FindProduct(int.Parse(Console.ReadLine() ?? ""))
                        ?? throw new InvalidOperationException("Product not found.");
                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine() ?? "");
                    order.AddLine(product, quantity);
                    Console.WriteLine("Line added.");
                    break;
                case 7:
                    Console.Write("Order id: ");
                    Order orderToPay = system.FindOrder(int.Parse(Console.ReadLine() ?? ""))
                        ?? throw new InvalidOperationException("Order not found.");
                    orderToPay.MarkPaid();
                    Console.WriteLine("Order marked as paid.");
                    break;
                case 8:
                    Console.WriteLine($"Paid sales total: {system.TotalPaidSales():F2}");
                    break;
                case 0:
                    Console.WriteLine("Bye.");
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid numeric input.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    } while (choice != 0);
}
