# Extensibility Guide for OpenEchoSystem.GuardClauses

One of the core design principles of the `OpenEchoSystem.GuardClauses` library is extensibility. You can easily create your own custom guard clauses to enforce project-specific validation rules while maintaining a consistent, fluent syntax.

## Creating a Custom Guard Clause

Custom guard clauses are implemented as extension methods for the `IGuardClause` interface. This marker interface is the entry point for all guard validations, accessed via `Guard.Against`.

### Step 1: Create a Static Extension Class

Create a new static class to house your custom guard clause extension methods. It's a good practice to place this in a namespace that is easily discoverable within your project.

```csharp
namespace YourProject.GuardExtensions
{
    public static class CustomGuardExtensions
    {
        // Your custom guard methods will go here
    }
}
```

### Step 2: Write the Extension Method

Inside your static class, create a public static method that extends `IGuardClause`.

*   The first parameter must be `this IGuardClause guardClause`.
*   The second parameter is the value you want to validate (e.g., `int input`).
*   Use the `[CallerArgumentExpression]` attribute to automatically capture the name of the argument being passed. This is crucial for providing clear and helpful exception messages.
*   Inside the method, implement your validation logic. If the validation fails, throw an appropriate exception (e.g., `ArgumentException`, `ArgumentOutOfRangeException`).

### Example: `IsOdd` Guard Clause

Let's create a custom guard clause that ensures an integer value is odd.

```csharp
using OpenEchoSystem.GuardClauses;
using System;
using System.Runtime.CompilerServices;

namespace YourProject.GuardExtensions
{
    public static class CustomGuardExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the input integer is not odd.
        /// </summary>
        /// <param name="guardClause">The IGuardClause instance.</param>
        /// <param name="input">The integer to validate.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically.</param>
        /// <exception cref="ArgumentException">Thrown when the input is an even number.</exception>
        public static void IsOdd(this IGuardClause guardClause, int input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input % 2 == 0)
            {
                throw new ArgumentException("Value must be odd.", parameterName);
            }
        }
    }
}
```

### Step 3: Use Your Custom Guard Clause

Once your extension method is created, you can use it just like any of the built-in guard clauses. Make sure to import the namespace where your custom extensions are defined.

```csharp
using YourProject.GuardExtensions;

public class OrderProcessor
{
    public void SetOddPriority(int priority)
    {
        Guard.Against.IsOdd(priority);

        // ... continue processing
    }
}
```

If you call `SetOddPriority(4)`, the `IsOdd` guard will throw an `ArgumentException` with the message "Value must be odd. (Parameter 'priority')".

## Best Practices for Custom Guards

1.  **Clear Naming:** Name your extension methods clearly to reflect the validation they perform (e.g., `HasMinimumLength`, `IsNotInTheFuture`).
2.  **Specific Exceptions:** Throw the most specific exception type possible. For general validation failures, `ArgumentException` is appropriate. For range issues, use `ArgumentOutOfRangeException`. You can also create your own custom exception types for domain-specific validation.
3.  **Use `[CallerArgumentExpression]`:** Always use this attribute to capture the parameter name. It significantly improves the debugging experience by providing context in the exception message.
4.  **Thorough Testing:** Write unit tests for your custom guard clauses to ensure they behave correctly in all scenarios, including both passing and failing cases.
5.  **Performance:** Keep your validation logic efficient. Avoid complex or long-running operations inside a guard clause, especially if it will be used in performance-critical code paths.