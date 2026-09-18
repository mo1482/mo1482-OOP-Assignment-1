namespace Part3_BuilderPattern;

public sealed class Invoice
{
    
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhone { get; }

    public string BillingStreet { get; }
    public string BillingCity { get; }
    public string BillingState { get; }
    public string BillingZipCode { get; }
    public string BillingCountry { get; }

    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingState { get; }
    public string ShippingZipCode { get; }
    public string ShippingCountry { get; }

    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    internal Invoice(string invoiceId,string customerName,string customerEmail,string customerPhone,Address billingAddress,Address shippingAddress,OrderPaymentInfo orderPayment)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;

        BillingStreet = billingAddress.Street;
        BillingCity = billingAddress.City;
        BillingState = billingAddress.State;
        BillingZipCode = billingAddress.ZipCode;
        BillingCountry = billingAddress.Country;

        ShippingStreet = shippingAddress.Street;
        ShippingCity = shippingAddress.City;
        ShippingState = shippingAddress.State;
        ShippingZipCode = shippingAddress.ZipCode;
        ShippingCountry = shippingAddress.Country;

        OrderDate = orderPayment.OrderDate;
        PaymentMethod = orderPayment.PaymentMethod;
        Currency = orderPayment.Currency;
        SubTotal = orderPayment.SubTotal;
        DiscountAmount = orderPayment.DiscountAmount;
        TaxAmount = orderPayment.TaxAmount;
        TotalAmount = orderPayment.TotalAmount;
    }

    public override string ToString()
        => $"Invoice {InvoiceId}: {CustomerName}, Total={Currency} {TotalAmount:F2}";
}
