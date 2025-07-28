
# OpenEchoSystem.GuardClauses: A Modern .NET Guard Clause Library

OpenEchoSystem.GuardClauses is a lightweight, performant, and extensible .NET library designed to make your code more robust and readable by simplifying argument validation. It leverages modern C# features like `[CallerArgumentExpression]` to provide "syntax sugar" that makes guard clauses clean and intuitive.

# Getting Started

## Installation


You can install the package via the NuGet Package Manager console:

```powershell
Install-Package OpenEchoSystem.GuardClauses
```

Or via the .NET CLI:

```bash
dotnet add package OpenEchoSystem.GuardClauses
```

## Usage Examples

The library is accessed through the static `Guard.Against` entry point.

### Null Checks

#### `Guard.Against.Null<T>()`
Throws an `ArgumentNullException` if the input is null.

```csharp
public void ProcessOrder(Order order)
{
    Guard.Against.Null(order);
    // ...
}
```

#### `Guard.Against.NullOrEmpty()` (for Collections and Strings)
Throws an `ArgumentNullException` for null input or an `ArgumentException` for an empty collection or string.

```csharp
public void SetTags(List<string> tags)
{
    Guard.Against.NullOrEmpty(tags);
    // ...
}

public void SetName(string name)
{
    Guard.Against.NullOrEmpty(name);
    // ...
}
```

#### `Guard.Against.NullOrWhiteSpace()`
Throws an `ArgumentNullException` for null strings, or an `ArgumentException` for empty or whitespace-only strings.

```csharp
public void SetUsername(string username)
{
    Guard.Against.NullOrWhiteSpace(username);
    // ...
}
```

### Numeric Checks

#### `Guard.Against.Zero<T>()`
Throws an `ArgumentException` if the numeric input is zero.

```csharp
public void SetQuantity(int quantity)
{
    Guard.Against.Zero(quantity);
    // ...
}
```

#### `Guard.Against.Negative<T>()`
Throws an `ArgumentException` if the numeric input is negative.

```csharp
public void SetPrice(decimal price)
{
    Guard.Against.Negative(price);
    // ...
}
```

#### `Guard.Against.OutOfRange<T>()`
Throws an `ArgumentOutOfRangeException` if the value is outside the specified range.

```csharp
public void SetAge(int age)
{
    Guard.Against.OutOfRange(age, 18, 99);
    // ...
}
```

### String Format Checks

#### `Guard.Against.InvalidFormat()`
Throws an `ArgumentException` if the input string does not match the provided regex pattern.

```csharp
public void SetZipCode(string zipCode)
{
    Guard.Against.InvalidFormat(zipCode, @"^\d{5}$");
    // ...
}
```

#### `Guard.Against.InvalidEmail()`
Uses a pre-compiled regex to efficiently validate email addresses.

```csharp
public void SetUserEmail(string email)
{
    Guard.Against.InvalidEmail(email);
    // ...
}
```

#### `Guard.Against.InvalidUrl()`
Uses a pre-compiled regex to efficiently validate URLs.

```csharp
public void SetWebsite(string url)
{
    Guard.Against.InvalidUrl(url);
    // ...
}
```

### Other Guard Clauses

#### `Guard.Against.NotFound()`
A specialized null check that throws a custom `NotFoundException` (or `ArgumentException` if not configured) to indicate a resource was not found.

```csharp
public void GetProduct(int productId)
{
    var product = _productRepository.GetById(productId);
    Guard.Against.NotFound(product);
    // ...
}
```

#### `Guard.Against.InvalidFormat()` (for Guids)
Throws an `ArgumentException` if a `Guid` is empty.

```csharp
public void SetCorrelationId(Guid correlationId)
{
    Guard.Against.InvalidFormat(correlationId);
    // ...
}
```

#### `Guard.Against.OutOfRange()` (for Enums)
Throws an `ArgumentOutOfRangeException` if an enum value is not a defined member of its type.

```csharp
public void SetStatus(OrderStatus status)
{
    Guard.Against.OutOfRange(status);
    // ...
}
```

#### `Guard.Against.CustomCondition()`
Provides a way to implement custom validation logic easily.

```csharp
public void ProcessValue(int value)
{
    Guard.Against.CustomCondition(value % 2 != 0, "Value must be an even number.");
    // ...
}
```

## Extensibility

You can easily create your own custom guard clauses by extending the `IGuardClause` interface.

```csharp
public static class CustomGuardExtensions
{
    public static void IsOdd(this IGuardClause guardClause, int input, [CallerArgumentExpression("input")] string? parameterName = null)
    {
        if (input % 2 == 0)
        {
            throw new ArgumentException("Value must be odd.", parameterName);
        }
    }
}

// Usage
public void SetOddNumber(int myNumber)
{
    Guard.Against.IsOdd(myNumber);
}
```

## Best Practices

*   **Fail Fast:** Use guard clauses at the beginning of your methods to validate inputs and fail as early as possible.
*   **Clarity:** Use the most specific guard clause for your validation to improve code readability and provide clear exception messages.
*   **Performance:** The library is designed to be lightweight. For performance-critical code paths, be mindful of complex custom validations. The built-in regex guards are pre-compiled for optimal performance.



## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue.

## License

This project is licensed under the MIT License.

