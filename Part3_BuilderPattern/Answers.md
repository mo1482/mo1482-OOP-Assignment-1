# Task 3 — Answers

## Task 3.1 — Why is a 20-parameter constructor a problem?

A 20-parameter constructor is hard to read at the call site because the meaning of each argument is not always visible. The risk is especially high when several parameters have the same type, such as many `string` values or several `decimal` amounts. The code can compile even when two values are passed in the wrong order.

It also becomes fragile when a new property is added. Every constructor call must be updated, and callers have to remember the position and meaning of the new argument. Optional values make the situation even worse because the caller may need many overloads or placeholder values.

The deeper issue is not only constructor length. The invoice contains several natural groups of related information: customer identity, a five-field billing address, a five-field shipping address, and order/payment information. The sample `Invoice` therefore keeps 21 read-only properties, grouped conceptually in those areas. Putting all of those concerns into one construction step makes the object harder to understand and makes validation responsibilities less clear.

## Task 3.2 — Why does the single Builder help?

The fluent builder replaces a long positional constructor call with named-looking method calls. For example, `SetBillingAddress(...)` communicates more clearly than passing a group of address strings in the correct positions.

The `Build()` method also validates mandatory values and rejects incomplete or invalid state immediately instead of allowing an incomplete Invoice object to be created.

The downside is that the single builder can still become large when it owns every group of the invoice. It improves the call site but does not fully separate the different responsibilities.

## Task 3.3 — Why is the composed version better?

### Single responsibility

`AddressBuilder` knows only how to construct and validate an `Address`. `OrderBuilder` knows only how to construct and validate order/payment information. `ComposedInvoiceBuilder` combines those completed pieces with customer-level invoice information.

### Independent validation

`AddressBuilder.Build()` can guarantee that street, city, state, ZIP code, and country are present without the Invoice builder having to know the validation details of an address.

`OrderBuilder.Build()` does the same for order/payment fields and their numeric validation.

### Reuse

The same `AddressBuilder` type is used for both billing and shipping. Without it, the invoice builder would need to duplicate the same street/city/state/ZIP/country construction logic twice.

### Readability

The composed version makes the domain groups visible in the calling code. The billing and shipping addresses are built as addresses, and the payment/order data is built as a separate unit. The final invoice builder then combines those meaningful objects instead of managing every low-level field itself.

### Result

The composed design gives a clearer separation of responsibilities while preserving the fluent style. It is easier to maintain because a change to address rules belongs to `AddressBuilder`, while a change to payment/order validation belongs to `OrderBuilder`.
