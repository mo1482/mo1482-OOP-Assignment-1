namespace Part1_ProceduralToOOP;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");

        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

        if (quantity > Stock)
            throw new InvalidOperationException($"Not enough stock for product #{Id}.");

        Stock -= quantity;
    }

    public override string ToString()
        => $"#{Id} {Name} price={Price:F2} stock={Stock}";
}
