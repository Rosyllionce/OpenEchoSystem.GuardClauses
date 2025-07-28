# Best Practices for Using OpenEchoSystem.GuardClauses

The `OpenEchoSystem.GuardClauses` library is a powerful tool for making your code more robust and readable. To get the most out of it, follow these best practices.

## 1. Fail Fast and Early

The primary purpose of guard clauses is to validate method arguments and fail as early as possible if the inputs are invalid. This prevents invalid data from propagating through your system, which can lead to unexpected behavior and difficult-to-debug errors.

**Do:** Place guard clauses at the very beginning of your methods, before any other logic.

```csharp
public void UpdateUserDetails(int userId, string newEmail)
{
    // Guards are the first thing in the method.
    Guard.Against.Zero(userId);
    Guard.Against.InvalidEmail(newEmail);

    // Method logic follows...
    var user = _userRepository.GetById(userId);
    user.Email = newEmail;
    _userRepository.Save(user);
}
```

**Don't:** Mix guard clauses with business logic.

```csharp
public void UpdateUserDetails(int userId, string newEmail)
{
    // Don't do this.
    var user = _userRepository.GetById(userId);
    Guard.Against.Zero(userId); // Too late!
    Guard.Against.InvalidEmail(newEmail);
    
    user.Email = newEmail;
    _userRepository.Save(user);
}
```

## 2. Be Specific with Your Guards

Use the most specific guard clause available for your validation. This improves code readability and ensures that the most appropriate exception is thrown, making debugging easier.

**Do:** Use `Guard.Against.NullOrWhiteSpace` for strings that must have content.

```csharp
public void SetTitle(string title)
{
    Guard.Against.NullOrWhiteSpace(title);
}
```

**Don't:** Use a more generic guard when a specific one exists.

```csharp
public void SetTitle(string title)
{
    // Less clear and throws a more generic exception.
    Guard.Against.CustomCondition(string.IsNullOrWhiteSpace(title), "Title cannot be empty.");
}
```

## 3. Keep Guard Clauses for Preconditions

Guard clauses are intended for validating *preconditions*—the state required for a method to execute correctly. They are not meant to replace all forms of validation or business logic.

*   **Guard Clauses:** Validate that the inputs to your method are in a valid state. They typically throw exceptions like `ArgumentNullException` or `ArgumentException`.
*   **Business Logic Validation:** Enforces rules about the state of your application. This might involve returning a validation result object or throwing a custom domain-specific exception.

**Example:**

```csharp
public class RegistrationService
{
    public RegistrationResult Register(string email, string password)
    {
        // Use guard clauses for preconditions.
        Guard.Against.InvalidEmail(email);
        Guard.Against.NullOrEmpty(password);

        // Use business logic for domain validation.
        if (_userRepository.EmailExists(email))
        {
            return RegistrationResult.Failure("Email address is already in use.");
        }

        // ... continue with registration
        return RegistrationResult.Success();
    }
}
```

## 4. Performance Considerations

The `OpenEchoSystem.GuardClauses` library is designed to be lightweight. However, be mindful of the performance implications of your validation logic, especially in performance-critical code.

*   The built-in guards for `InvalidEmail` and `InvalidUrl` use pre-compiled regular expressions for optimal performance.
*   When creating custom guards, avoid long-running or complex operations. If a validation requires a database lookup or a web service call, it is likely business logic, not a precondition, and should not be in a guard clause.

## 5. Leverage Extensibility for Readability

Create custom guard clauses for domain-specific validations that you use frequently. This encapsulates the validation logic and makes your code more expressive and readable.

**Do:** Create a custom guard for a specific format, like a product SKU.

```csharp
public static class ProductGuardExtensions
{
    public static void InvalidSku(this IGuardClause guardClause, string sku)
    {
        if (!Regex.IsMatch(sku, @"^[A-Z]{3}-\d{5}$"))
        {
            throw new ArgumentException("Invalid SKU format.", nameof(sku));
        }
    }
}

// Usage:
Guard.Against.InvalidSku(product.Sku);
```

This is much cleaner than scattering the same regex validation throughout your codebase.