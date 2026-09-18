namespace Part3_BuilderPattern;

public class InvoiceBuilder
{
    private string? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;

    private Address? _billingAddress;
    private Address? _shippingAddress;

    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal? _discountAmount;
    private decimal? _taxAmount;
    private decimal? _totalAmount;

    public InvoiceBuilder SetInvoiceId(string value) 
    {
        _invoiceId = value;
        return this; 
    }

    public InvoiceBuilder SetCustomerName(string value) 
    { 
        _customerName = value; 
        return this; 
    }

    public InvoiceBuilder SetCustomerEmail(string value) 
    { 
        _customerEmail = value; 
        return this; 
    }

    public InvoiceBuilder SetCustomerPhone(string value) 
    { 
        _customerPhone = value; 
        return this; 
    }

    public InvoiceBuilder SetBillingStreet(string value) => SetBillingAddress(
        AddOrReplaceAddress(_billingAddress, value, null, null, null, null));
    public InvoiceBuilder SetBillingCity(string value) => SetBillingAddress(
        AddOrReplaceAddress(_billingAddress, null, value, null, null, null));
    public InvoiceBuilder SetBillingState(string value) => SetBillingAddress(
        AddOrReplaceAddress(_billingAddress, null, null, value, null, null));
    public InvoiceBuilder SetBillingZipCode(string value) => SetBillingAddress(
        AddOrReplaceAddress(_billingAddress, null, null, null, value, null));
    public InvoiceBuilder SetBillingCountry(string value) => SetBillingAddress(
        AddOrReplaceAddress(_billingAddress, null, null, null, null, value));

    public InvoiceBuilder SetShippingStreet(string value) => SetShippingAddress(
        AddOrReplaceAddress(_shippingAddress, value, null, null, null, null));
    public InvoiceBuilder SetShippingCity(string value) => SetShippingAddress(
        AddOrReplaceAddress(_shippingAddress, null, value, null, null, null));
    public InvoiceBuilder SetShippingState(string value) => SetShippingAddress(
        AddOrReplaceAddress(_shippingAddress, null, null, value, null, null));
    public InvoiceBuilder SetShippingZipCode(string value) => SetShippingAddress(
        AddOrReplaceAddress(_shippingAddress, null, null, null, value, null));
    public InvoiceBuilder SetShippingCountry(string value) => SetShippingAddress(
        AddOrReplaceAddress(_shippingAddress, null, null, null, null, value));

    public InvoiceBuilder SetBillingAddress(Address value) 
    { 
        _billingAddress = value; 
        return this; 
    }

    public InvoiceBuilder SetShippingAddress(Address value) 
    {
        _shippingAddress = value; 
        return this; 
    }

    public InvoiceBuilder SetOrderDate(DateTime value) 
    { 
        _orderDate = value; 
        return this;
    }

    public InvoiceBuilder SetPaymentMethod(string value) 
    { 
        _paymentMethod = value; 
        return this; 
    }

    public InvoiceBuilder SetCurrency(string value) 
    { 
        _currency = value; 
        return this; 
    }

    public InvoiceBuilder SetSubTotal(decimal value) 
    { 
        _subTotal = value; 
        return this; 
    }

    public InvoiceBuilder SetDiscountAmount(decimal value) 
    { 
        _discountAmount = value; 
        return this; 
    }

    public InvoiceBuilder SetTaxAmount(decimal value) 
    { 
        _taxAmount = value; 
        return this; 
    }

    public InvoiceBuilder SetTotalAmount(decimal value) 
    { 
        _totalAmount = value; 
        return this; 
    }

    public Invoice Build()
    {
        Require(_invoiceId, "InvoiceId");
        Require(_customerName, "CustomerName");
        Require(_customerEmail, "CustomerEmail");
        Require(_customerPhone, "CustomerPhone");

        if (_billingAddress is null || _shippingAddress is null)
            throw new InvalidOperationException("BillingAddress and ShippingAddress are mandatory.");
        if (_orderDate is null)
            throw new InvalidOperationException("OrderDate is mandatory.");

        Require(_paymentMethod, "PaymentMethod");
        
        Require(_currency, "Currency");

        if (_subTotal is null || _subTotal < 0)
            throw new InvalidOperationException("SubTotal must be zero or positive.");

        if (_discountAmount is null || _discountAmount < 0)
            throw new InvalidOperationException("DiscountAmount must be zero or positive.");

        if (_taxAmount is null || _taxAmount < 0)
            throw new InvalidOperationException("TaxAmount must be zero or positive.");
            
        if (_totalAmount is null || _totalAmount < 0)
            throw new InvalidOperationException("TotalAmount must be zero or positive.");

        var orderPayment = new OrderPaymentInfo
        (
            _orderDate.Value,
            _paymentMethod!,
            _currency!,
            _subTotal.Value,
            _discountAmount.Value,
            _taxAmount.Value,
            _totalAmount.Value
        );

        return new Invoice
        (
            _invoiceId!,
            _customerName!,
            _customerEmail!,
            _customerPhone!,
            _billingAddress,
            _shippingAddress,
            orderPayment
        );
    }

    private static Address AddOrReplaceAddress(Address? existing,string? street,string? city,string? state,string? zipCode,string? country)
    {
        return new Address
        (
            street ?? existing?.Street ?? "",
            city ?? existing?.City ?? "",
            state ?? existing?.State ?? "",
            zipCode ?? existing?.ZipCode ?? "",
            country ?? existing?.Country ?? ""
        );
    }

    private static void Require(string? value, string field)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"{field} is mandatory.");
    }
}
