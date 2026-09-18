namespace Part3_BuilderPattern;

public class OrderBuilder
{
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal? _discountAmount;
    private decimal? _taxAmount;
    private decimal? _totalAmount;

    public OrderBuilder SetOrderDate(DateTime orderDate) 
    {
        _orderDate = orderDate; 
        return this; 
    }

    public OrderBuilder SetPaymentMethod(string paymentMethod) 
    {
        _paymentMethod = paymentMethod; 
        return this; 
    }

    public OrderBuilder SetCurrency(string currency) 
    {
        _currency = currency;
         return this; 
    }

    public OrderBuilder SetSubTotal(decimal subTotal) 
    {
        _subTotal = subTotal;
        return this; 
    }
    public OrderBuilder SetDiscountAmount(decimal discountAmount) 
    {
        _discountAmount = discountAmount; 
        return this; 
    }

    public OrderBuilder SetTaxAmount(decimal taxAmount) 
    {
        _taxAmount = taxAmount; 
        return this; 
    }

    public OrderBuilder SetTotalAmount(decimal totalAmount) 
    {
        _totalAmount = totalAmount; 
        return this; 
    }

    public OrderPaymentInfo Build()
    {
        if (_orderDate is null)
            throw new InvalidOperationException("Order date is mandatory.");

        if (string.IsNullOrWhiteSpace(_paymentMethod))
            throw new InvalidOperationException("Payment method is mandatory.");

        if (string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Currency is mandatory.");

        if (_subTotal is null || _subTotal < 0)
            throw new InvalidOperationException("SubTotal must be zero or positive.");

        if (_discountAmount is null || _discountAmount < 0)
            throw new InvalidOperationException("DiscountAmount must be zero or positive.");

        if (_taxAmount is null || _taxAmount < 0)
            throw new InvalidOperationException("TaxAmount must be zero or positive.");

        if (_totalAmount is null || _totalAmount < 0)
            throw new InvalidOperationException("TotalAmount must be zero or positive.");

        return new OrderPaymentInfo
        (
            _orderDate.Value,
            _paymentMethod!,
            _currency!,
            _subTotal.Value,
            _discountAmount.Value,
            _taxAmount.Value,
            _totalAmount.Value
        );
    }
}
