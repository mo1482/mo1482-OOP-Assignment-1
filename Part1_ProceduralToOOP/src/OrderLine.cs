namespace Part1_ProceduralToOOP;

public class OrderLine
{
    public Product Product { get; }
    public int Quantity { get; }
    public decimal LineTotal => Product.Price * Quantity;

    public OrderLine(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

        Product = product ?? throw new ArgumentNullException(nameof(product));
        Quantity = quantity;
    }
}
