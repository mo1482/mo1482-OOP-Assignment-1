# Task 1.1 — Critique of the Procedural C++ Program

The source program works, but it stores the whole domain in global variables and uses free functions to coordinate those variables. The repository itself describes it as a procedural C++ example with Customer, Product, Order, and order-line data represented without classes or structs.

## 1. Global mutable state

The program has global counters such as `customerCount`, `productCount`, and `orderCount`, plus global arrays holding the actual data. Any function can change them. This means the correctness of one operation depends on hidden state somewhere else in the program.

### What could go wrong?
A new function could accidentally modify a count or array entry and break another part of the program. Testing is also harder because functions cannot be reasoned about independently from the entire global state.

## 2. Parallel arrays represent one object

A customer is spread across `customerIds`, `customerNames`, `customerEmails`, `customerCities`, and `customerIsVip`. Products and orders are split in the same way. An index becomes the implicit identity connecting pieces of data.

### What could go wrong?
If two arrays become out of sync, one customer's ID could be displayed with another customer's name or VIP flag. The compiler cannot protect this representation because there is no Customer/Product/Order object grouping the related values.

## 3. Fixed-size limits are built into the data model

The program uses constants such as `MAX_CUSTOMERS`, `MAX_PRODUCTS`, `MAX_ORDERS`, and `MAX_LINES_PER_ORDER`.

### What could go wrong?
The system rejects additional valid data simply because the preselected array capacity was reached. Capacity management becomes mixed with business behavior.

## 4. Integer indexes are used as relationships

An order stores `orderCustomerIndexes`, and order lines store `lineProductIndexes`. These are indexes into other global arrays rather than references to real domain objects.

### What could go wrong?
Reordering or changing the underlying arrays can make relationships incorrect. Every function also has to perform manual lookup and index management.

## 5. Business rules are scattered through free functions

Rules such as “paid orders cannot be changed,” “quantity must be positive,” “stock must be sufficient,” and “an empty order cannot be paid” live inside functions that manipulate global arrays.

### What could go wrong?
Another function can bypass the rule because there is no Order or Product object protecting its own state. The same rule may later be duplicated in several places and drift apart.

## 6. Functions know too much about representation

`calculateOrderTotal` directly navigates multiple arrays and uses indexes to reconstruct an order. Printing an order also needs the order arrays, customer arrays, product arrays, and line arrays at the same time.

### What could go wrong?
A small change to the data representation forces changes in many unrelated functions.

## 7. Weak encapsulation

The data has no object boundary. There is nothing preventing a future function from writing `orderIsPaid`, changing stock directly, or changing one field while forgetting the related fields.

### What could go wrong?
Invalid state can be created without going through the intended business operations.

## 8. Too much responsibility in procedural functions

Functions both locate data, validate business rules, mutate storage, and format output. For example, order-line creation handles order lookup, payment checks, capacity checks, product lookup, quantity validation, stock validation, stock mutation, and array writes.

### What could go wrong?
Such functions become difficult to change and test because several responsibilities are coupled together.

## 9. Behavior is not owned by the data it protects

The important operations are really domain behavior: an Order can add a line and become paid; a Product can decrease stock; a Customer can expose its identity and VIP status. In the procedural version, these behaviors are external functions.

### What could go wrong?
The program structure does not make it obvious which object owns which rule. This increases the chance of putting future logic in the wrong place.

## 10. Error handling is based mainly on messages and return values

Invalid operations often print an error and return. This is workable for the small console application but makes it easier for callers to ignore a failure unless they remember to inspect the return value.

### Better design direction
The C# version gives each concept a class, moves state into objects, uses object references instead of parallel-array indexes, and makes the system object own the collections of customers, products, and orders. The user-facing menu remains similar while the internal representation becomes object-oriented.
