# Design Patterns Demo - Beverage E-Commerce System

## ?? Overview
This project implements 2 popular Design Patterns in software development:
1. **Factory Method Pattern** - Beverage Product Management
2. **Adapter Pattern** - Multi-Payment System Integration

## ?? Objectives
- Apply **SOLID Principles**
- Write **Clean Code**
- Separation of Concerns
- Easy to extend and maintain

---

## ??? Project Structure

### 1?? Factory Method Pattern

```
FactoryMethod/
??? Interfaces/
?   ??? IBeverage.cs                 # Product interface
?   ??? IBeverageRepository.cs       # Repository interface
??? Models/
?   ??? SoftDrink.cs                 # Concrete Product: Soft Drinks
?   ??? Juice.cs                     # Concrete Product: Juices
?   ??? MilkTea.cs                   # Concrete Product: Milk Tea
??? Factories/
?   ??? BeverageFactory.cs           # Abstract Factory
?   ??? SoftDrinkFactory.cs          # Concrete Factory
?   ??? JuiceFactory.cs              # Concrete Factory
?   ??? MilkTeaFactory.cs            # Concrete Factory
??? Repositories/
?   ??? BeverageRepository.cs        # In-memory repository
??? Services/
?   ??? BeverageService.cs           # Business logic
??? Program.cs                        # Entry point with interactive menu
```

### 2?? Adapter Pattern

```
Adapter/
??? Interfaces/
?   ??? IPaymentProcessor.cs         # Target Interface
?   ??? IOrderRepository.cs          # Repository Interface
??? Models/
?   ??? Order.cs                     # Order Entity
??? ThirdPartyServices/
?   ??? MoMoPaymentService.cs        # Adaptee 1
?   ??? ZaloPayGateway.cs            # Adaptee 2
?   ??? BankingPaymentSystem.cs      # Adaptee 3
??? Adapters/
?   ??? MoMoPaymentAdapter.cs        # Adapter for MoMo
?   ??? ZaloPayAdapter.cs            # Adapter for ZaloPay
?   ??? BankingPaymentAdapter.cs     # Adapter for Banking
??? Repositories/
?   ??? OrderRepository.cs           # In-memory repository
??? Services/
?   ??? OrderService.cs              # Business logic
??? Program.cs                        # Entry point with interactive menu
```

---

## ?? How to Run

### System Requirements
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Run Factory Method Pattern
```bash
cd FactoryMethod
dotnet run
```

### Run Adapter Pattern
```bash
cd Adapter
dotnet run
```

---

## ?? Design Pattern Details

### 1?? Factory Method Pattern

#### ?? Purpose
Define an interface for creating an object, but let subclasses decide which class to instantiate.

#### ?? Main Components

**Product (IBeverage)**
```csharp
public interface IBeverage
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
    int Quantity { get; set; }
    string GetBeverageType();
    void DisplayInfo();
    decimal CalculateTotal();
}
```

**Concrete Products**
- `SoftDrink`: Soft drinks (Brand, Volume)
- `Juice`: Fruit juices (Fruit, IsOrganic)
- `MilkTea`: Milk tea (Size, Topping)

**Abstract Creator (BeverageFactory)**
- Factory Method: `CreateBeverageInstance()`
- Template Methods: `CreateBeverage()`, `UpdateBeverage()`, `DeleteBeverage()`

**Concrete Creators**
- `SoftDrinkFactory`
- `JuiceFactory`
- `MilkTeaFactory`

#### ? SOLID Principles Applied

**Single Responsibility Principle (SRP)**
- Each class has a single responsibility
- `BeverageRepository`: Data management
- `BeverageService`: Display and calculations
- `BeverageFactory`: Product creation

**Open/Closed Principle (OCP)**
- Open for extension: Add new beverage types (e.g., Coffee) by creating new classes only
- Closed for modification: No need to modify existing code

**Liskov Substitution Principle (LSP)**
- All concrete products can substitute IBeverage
- All concrete factories can substitute BeverageFactory

**Interface Segregation Principle (ISP)**
- Small and focused interfaces
- `IBeverage` only contains necessary methods for products

**Dependency Inversion Principle (DIP)**
- Depend on abstractions (IBeverage, IBeverageRepository)
- Not dependent on concrete classes

#### ?? Usage Example

```csharp
// Dependency Injection
IBeverageRepository repository = new BeverageRepository();
var softDrinkFactory = new SoftDrinkFactory(repository);

// CREATE
softDrinkFactory.CreateBeverage("Coca Cola", 1.5m, 50, new Dictionary<string, object>
{
    { "Brand", "Coca-Cola" },
    { "Volume", 330 }
});

// READ
var beverages = softDrinkFactory.GetAllBeverages();

// UPDATE
softDrinkFactory.UpdateBeverage(1, "Coca Cola Zero", 1.7m, 60, data);

// DELETE
softDrinkFactory.DeleteBeverage(1);
```

---

### 2?? Adapter Pattern

#### ?? Purpose
Allow incompatible interfaces to work together by creating a wrapper.

#### ?? Main Components

**Target Interface (IPaymentProcessor)**
```csharp
public interface IPaymentProcessor
{
    string PaymentMethodName { get; }
    bool ProcessPayment(int orderId, decimal amount, string customerName);
    bool RefundPayment(int orderId);
    string GetPaymentStatus(int orderId);
    decimal GetTransactionFee(decimal amount);
}
```

**Adaptees (Third Party Services)**
- `MoMoPaymentService`: MoMo API
- `ZaloPayGateway`: ZaloPay API
- `BankingPaymentSystem`: Banking API

**Adapters**
- `MoMoPaymentAdapter`: Convert IPaymentProcessor ? MoMo API
- `ZaloPayAdapter`: Convert IPaymentProcessor ? ZaloPay API
- `BankingPaymentAdapter`: Convert IPaymentProcessor ? Banking API

**Client (OrderService)**
```csharp
public class OrderService
{
    private IPaymentProcessor _paymentProcessor;
    
    public void SetPaymentProcessor(IPaymentProcessor processor)
    {
        _paymentProcessor = processor;
    }
    
    public Order CreateOrder(...)
    {
        // Use unified payment processor
        _paymentProcessor.ProcessPayment(...);
    }
}
```

#### ? SOLID Principles Applied

**Single Responsibility Principle (SRP)**
- `OrderService`: Order management
- `Adapters`: Interface conversion
- `ThirdPartyServices`: Payment processing

**Open/Closed Principle (OCP)**
- Adding new payment method doesn't require modifying OrderService
- Just create a new Adapter

**Liskov Substitution Principle (LSP)**
- All adapters can substitute IPaymentProcessor

**Interface Segregation Principle (ISP)**
- IPaymentProcessor only contains necessary methods
- No forced implementation of unused methods

**Dependency Inversion Principle (DIP)**
- OrderService depends on IPaymentProcessor (abstraction)
- Not dependent on concrete adapters

#### ?? Usage Example

```csharp
// Initialize
IOrderRepository orderRepository = new OrderRepository();
IPaymentProcessor momoAdapter = new MoMoPaymentAdapter();
var orderService = new OrderService(orderRepository, momoAdapter);

// CREATE Order with MoMo
orderService.CreateOrder("John Doe", "Coca Cola", 1.5m, 2);

// Switch to ZaloPay
IPaymentProcessor zaloPayAdapter = new ZaloPayAdapter();
orderService.SetPaymentProcessor(zaloPayAdapter);

// CREATE Order with ZaloPay
orderService.CreateOrder("Jane Smith", "Milk Tea", 3.5m, 1);

// CANCEL Order (auto refund)
orderService.CancelOrder(1);
```

---

## ?? Clean Code Practices

### 1. Naming Conventions
- **Classes**: PascalCase (e.g., `BeverageFactory`, `OrderService`)
- **Interfaces**: IPascalCase (e.g., `IBeverage`, `IPaymentProcessor`)
- **Methods**: PascalCase (e.g., `CreateBeverage()`, `ProcessPayment()`)
- **Variables**: camelCase (e.g., `totalAmount`, `customerName`)
- **Private fields**: _camelCase (e.g., `_repository`, `_momoService`)

### 2. Method Size
- Each method < 20 lines
- One method does one thing
- Method name describes functionality

### 3. Comments
- Self-documenting code
- Only comment when explaining complex logic
- Don't comment obvious code

### 4. Error Handling
- Check null before using
- Return early pattern
- Clear error messages for users

### 5. Dependency Injection
- Constructor injection
- Depend on abstractions, not concretions
- Easy to test and mock

---

## ?? Test Cases (Suggested)

### Factory Method Pattern
```csharp
[Test]
public void CreateBeverage_ShouldReturnNewBeverage()
{
    // Arrange
    var repository = new BeverageRepository();
    var factory = new SoftDrinkFactory(repository);
    
    // Act
    var beverage = factory.CreateBeverage("Coca", 1.5m, 10, data);
    
    // Assert
    Assert.NotNull(beverage);
    Assert.Equal("Coca", beverage.Name);
}
```

### Adapter Pattern
```csharp
[Test]
public void ProcessPayment_WithMoMo_ShouldSucceed()
{
    // Arrange
    var momoAdapter = new MoMoPaymentAdapter();
    
    // Act
    var result = momoAdapter.ProcessPayment(1, 100m, "Test User");
    
    // Assert
    Assert.True(result);
    Assert.Equal("Paid", momoAdapter.GetPaymentStatus(1));
}
```

---

## ?? Expected Results

### Factory Method Output
```
??? ?? CREATE BEVERAGES ???

? Soft Drink 'Coca Cola' created successfully! (ID: 1)
? Soft Drink 'Pepsi' created successfully! (ID: 2)
? Juice 'Orange Juice' created successfully! (ID: 3)
...

??? ?? READ ALL BEVERAGES ???

  ?? Total Beverages: 6
  ????????????????????????????????????????????????????????????
  ?? [Soft Drink] ID: 1 | Coca Cola (Coca-Cola) | 330ml
     Price: $1.50 | Qty: 50 | Total: $75.00
...

  ?? Total Inventory Value: $500.00
  ?? Total Items: 225
```

### Adapter Pattern Output
```
??????????????????????????????????????????????????????????
  ?? SCENARIO 1: USING MOMO PAYMENT
??????????????????????????????????????????????????????????

  ?? Creating Order #1...
     Subtotal: $3.00 | Fee: $0.03 | Total: $3.03
  [MoMo API] Transaction created: MOMO-A1B2C3D4
  [MoMo API] Phone: 0987654321 | Amount: 72,720 VND
  ? Order #1 created successfully!
...
```

---

## ?? Future Enhancements

### Factory Method
- [ ] Add beverage types: Coffee, Smoothie, Tea
- [ ] Add discount system
- [ ] Add inventory alerts (low stock)
- [ ] Export to CSV/JSON

### Adapter Pattern
- [ ] Add payment methods: VNPay, PayPal, Stripe
- [ ] Payment history tracking
- [ ] Transaction logs
- [ ] Email notifications after payment

---

## ?? References

1. **Design Patterns: Elements of Reusable Object-Oriented Software** - Gang of Four
2. **Head First Design Patterns** - Eric Freeman
3. **Clean Code** - Robert C. Martin
4. **SOLID Principles** - Robert C. Martin

---

## ????? Author
- **Patterns**: Factory Method, Adapter
- **Technology**: C# .NET 8
- **Principles**: SOLID, Clean Code

---

## ?? License
This is a demo project for educational purposes.

---

**Happy Coding! ??**
