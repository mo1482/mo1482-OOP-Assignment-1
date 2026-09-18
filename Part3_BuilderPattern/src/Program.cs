using Part3_BuilderPattern;

Console.WriteLine("=== Task 3.2 — Single Big Builder ===");

Invoice invoice1 = new InvoiceBuilder()
    .SetInvoiceId("INV-1001")
    .SetCustomerName("Ahmed Ali")
    .SetCustomerEmail("ahmed@example.com")
    .SetCustomerPhone("01000000000")
    .SetBillingStreet("10 Nile Street")
    .SetBillingCity("Cairo")
    .SetBillingState("Cairo")
    .SetBillingZipCode("11511")
    .SetBillingCountry("Egypt")
    .SetShippingStreet("20 Tahrir Street")
    .SetShippingCity("Giza")
    .SetShippingState("Giza")
    .SetShippingZipCode("12511")
    .SetShippingCountry("Egypt")
    .SetOrderDate(new DateTime(2026, 9, 18))
    .SetPaymentMethod("Visa")
    .SetCurrency("EGP")
    .SetSubTotal(5000m)
    .SetDiscountAmount(250m)
    .SetTaxAmount(700m)
    .SetTotalAmount(5450m)
    .Build();

Console.WriteLine(invoice1);

Console.WriteLine("\n=== Task 3.3 — Composed Builders ===");

Address billing = new AddressBuilder()
    .SetStreet("10 Nile Street")
    .SetCity("Cairo")
    .SetState("Cairo")
    .SetZipCode("11511")
    .SetCountry("Egypt")
    .Build();

Address shipping = new AddressBuilder()
    .SetStreet("20 Tahrir Street")
    .SetCity("Giza")
    .SetState("Giza")
    .SetZipCode("12511")
    .SetCountry("Egypt")
    .Build();

OrderPaymentInfo orderPayment = new OrderBuilder()
    .SetOrderDate(new DateTime(2026, 9, 18))
    .SetPaymentMethod("Visa")
    .SetCurrency("EGP")
    .SetSubTotal(5000m)
    .SetDiscountAmount(250m)
    .SetTaxAmount(700m)
    .SetTotalAmount(5450m)
    .Build();

Invoice invoice2 = new ComposedInvoiceBuilder()
    .SetInvoiceId("INV-1002")
    .SetCustomerName("Mona Hassan")
    .SetCustomerEmail("mona@example.com")
    .SetCustomerPhone("01111111111")
    .SetBillingAddress(billing)
    .SetShippingAddress(shipping)
    .SetOrderPayment(orderPayment)
    .Build();

Console.WriteLine(invoice2);

Console.WriteLine("\n=== Mandatory-property validation ===");
try
{
    _ = new ComposedInvoiceBuilder()
        .SetInvoiceId("INV-ERROR")
        .SetCustomerName("Test")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Build rejected: {ex.Message}");
}
