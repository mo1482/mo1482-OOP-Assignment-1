namespace Part1_ProceduralToOOP;

public class Order
{
    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines.AsReadOnly();

    public decimal Total
    {
        get
        {
            decimal total = _lines.Sum(line => line.LineTotal);
            return Customer.IsVip ? total * 0.90m : total;
        }
    }

    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Date = string.IsNullOrWhiteSpace(date)
            ? throw new ArgumentException("Date is required.", nameof(date))
            : date;
    }

    public void AddLine(Product product, int quantity)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot change a paid order.");

        if (product is null)
            throw new ArgumentNullException(nameof(product));

        product.ReduceStock(quantity);
        _lines.Add(new OrderLine(product, quantity));
    }

    public void MarkPaid()
    {
        if (_lines.Count == 0)
            throw new InvalidOperationException("Cannot pay an empty order.");

        IsPaid = true;
    }
}
