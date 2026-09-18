namespace Part3_BuilderPattern;

public class ComposedInvoiceBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderPaymentInfo? _orderPayment;

    public ComposedInvoiceBuilder SetInvoiceId(string value) 
    {
        _invoiceId = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetCustomerName(string value) 
    { 
        _customerName = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetCustomerEmail(string value) 
    {
        _customerEmail = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetCustomerPhone(string value) 
    { 
        _customerPhone = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetBillingAddress(Address value) 
    { 
        _billingAddress = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetShippingAddress(Address value) 
    { 
        _shippingAddress = value; 
        return this; 
    }

    public ComposedInvoiceBuilder SetOrderPayment(OrderPaymentInfo value) 
    { 
        _orderPayment = value; 
        return this; 
    }

    public Invoice Build()
    {
        Require(_invoiceId, "InvoiceId");
        Require(_customerName, "CustomerName");
        Require(_customerEmail, "CustomerEmail");
        Require(_customerPhone, "CustomerPhone");

        if (_billingAddress is null)
            throw new InvalidOperationException("BillingAddress is mandatory.");

        if (_shippingAddress is null)
            throw new InvalidOperationException("ShippingAddress is mandatory.");

        if (_orderPayment is null)
            throw new InvalidOperationException("Order/payment information is mandatory.");

        return new Invoice
        (
            _invoiceId!,
            _customerName!,
            _customerEmail!,
            _customerPhone!,
            _billingAddress,
            _shippingAddress,
            _orderPayment
        );
    }

    private static void Require(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"{field} is mandatory.");
    }
}
