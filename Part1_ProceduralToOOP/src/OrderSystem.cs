namespace Part1_ProceduralToOOP;

public class OrderSystem
{
    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Customer> Customers => _customers.AsReadOnly();
    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public void AddCustomer(Customer customer)
    {
        if (_customers.Any(c => c.Id == customer.Id))
            throw new InvalidOperationException($"Customer id {customer.Id} already exists.");

        _customers.Add(customer);
    }

    public void AddProduct(Product product)
    {
        if (_products.Any(p => p.Id == product.Id))
            throw new InvalidOperationException($"Product id {product.Id} already exists.");

        _products.Add(product);
    }

    public Order CreateOrder(int orderId, int customerId, string date)
    {
        if (_orders.Any(o => o.Id == orderId))
            throw new InvalidOperationException($"Order id {orderId} already exists.");

        Customer customer = FindCustomer(customerId)
            ?? throw new InvalidOperationException($"Customer id {customerId} not found.");

        var order = new Order(orderId, customer, date);
        _orders.Add(order);
        return order;
    }

    public Customer? FindCustomer(int id) => _customers.FirstOrDefault(c => c.Id == id);
    public Product? FindProduct(int id) => _products.FirstOrDefault(p => p.Id == id);
    public Order? FindOrder(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public decimal TotalPaidSales()
        => _orders.Where(o => o.IsPaid).Sum(o => o.Total);
}
