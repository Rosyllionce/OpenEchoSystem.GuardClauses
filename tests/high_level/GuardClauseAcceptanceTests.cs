using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using OpenEchoSystem.GuardClauses; // Assuming the Guard class is in this namespace

// Define a custom exception for demonstration of custom guards
public class NotFoundException : ArgumentException
{
    public NotFoundException(string message, string paramName)
        : base(message, paramName) { }
}

// Extend IGuardClause for custom guards (for testing extensibility)
public static class CustomGuardExtensions
{
    public static void NotFound<T>(this IGuardClause guard, T input, string entityName, [CallerArgumentExpression("input")] string paramName = null)
        where T : class
    {
        if (input == null)
            throw new NotFoundException($"{entityName} not found.", paramName);
    }

    public static void IsTrue(this IGuardClause guard, bool condition, string message, [CallerArgumentExpression("condition")] string paramName = null)
    {
        if (!condition)
            throw new ArgumentException(message, paramName);
    }
}

namespace OpenEchoSystem.GuardClauses.AcceptanceTests
{
    public class GuardClauseAcceptanceTests
    {
        private readonly ITestOutputHelper _output;

        public GuardClauseAcceptanceTests(ITestOutputHelper output)
        {
            _output = output;
        }

        // --- Phase 1: Core Guard Clause Functionality Verification ---

        [Fact]
        public void AgainstNull_WhenObjectIsNull_ThrowsArgumentNullException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            object nullObject = null;
            string paramName = nameof(nullObject);

            Action act = () => Guard.Against.Null(nullObject, paramName);

            act.Should().ThrowExactly<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNull_WhenObjectIsNull_ThrowsArgumentNullException for '{paramName}'");
        }

        [Fact]
        public void AgainstNull_WhenObjectIsNotNull_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            object nonNullObject = new object();
            string paramName = nameof(nonNullObject);

            Action act = () => Guard.Against.Null(nonNullObject, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstNull_WhenObjectIsNotNull_DoesNotThrowException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenStringIsNull_ThrowsArgumentNullException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string nullString = null;
            string paramName = nameof(nullString);

            Action act = () => Guard.Against.NullOrEmpty(nullString, paramName);

            act.Should().ThrowExactly<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenStringIsNull_ThrowsArgumentNullException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenStringIsEmpty_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string emptyString = string.Empty;
            string paramName = nameof(emptyString);

            Action act = () => Guard.Against.NullOrEmpty(emptyString, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"String cannot be empty. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenStringIsEmpty_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenStringIsPopulated_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            string populatedString = "test";
            string paramName = nameof(populatedString);

            Action act = () => Guard.Against.NullOrEmpty(populatedString, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenStringIsPopulated_DoesNotThrowException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenCollectionIsNull_ThrowsArgumentNullException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            List<int> nullCollection = null;
            string paramName = nameof(nullCollection);

            Action act = () => Guard.Against.NullOrEmpty(nullCollection, paramName);

            act.Should().ThrowExactly<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenCollectionIsNull_ThrowsArgumentNullException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenCollectionIsEmpty_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            List<int> emptyCollection = new List<int>();
            string paramName = nameof(emptyCollection);

            Action act = () => Guard.Against.NullOrEmpty(emptyCollection, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"Collection cannot be empty. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenCollectionIsEmpty_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrEmpty_WhenCollectionIsPopulated_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            List<int> populatedCollection = new List<int> { 1, 2, 3 };
            string paramName = nameof(populatedCollection);

            Action act = () => Guard.Against.NullOrEmpty(populatedCollection, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstNullOrEmpty_WhenCollectionIsPopulated_DoesNotThrowException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentNullException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string nullString = null;
            string paramName = nameof(nullString);

            Action act = () => Guard.Against.NullOrWhiteSpace(nullString, paramName);

            act.Should().ThrowExactly<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrWhiteSpace_WhenStringIsNull_ThrowsArgumentNullException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_WhenStringIsEmpty_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string emptyString = string.Empty;
            string paramName = nameof(emptyString);

            Action act = () => Guard.Against.NullOrWhiteSpace(emptyString, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"String cannot be empty or white space. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrWhiteSpace_WhenStringIsEmpty_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string whiteSpaceString = "   ";
            string paramName = nameof(whiteSpaceString);

            Action act = () => Guard.Against.NullOrWhiteSpace(whiteSpaceString, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"String cannot be empty or white space. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNullOrWhiteSpace_WhenStringIsWhiteSpace_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_WhenStringIsPopulated_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            string populatedString = "  hello  ";
            string paramName = nameof(populatedString);

            Action act = () => Guard.Against.NullOrWhiteSpace(populatedString, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstNullOrWhiteSpace_WhenStringIsPopulated_DoesNotThrowException for '{paramName}'");
        }

        [Theory]
        [InlineData(5, 1, 10)] // In range
        [InlineData(1, 1, 10)] // Lower bound inclusive
        [InlineData(10, 1, 10)] // Upper bound inclusive
        public void AgainstOutOfRange_WhenNumberIsInRange_DoesNotThrowException(int value, int lower, int upper)
        {
            // AI Verifiable: Checks for absence of any exception for various valid inputs.
            string paramName = nameof(value);
            Action act = () => Guard.Against.OutOfRange(value, lower, upper, paramName);
            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstOutOfRange_WhenNumberIsInRange_DoesNotThrowException for '{value}'");
        }

        [Theory]
        [InlineData(0, 1, 10)] // Below lower bound
        [InlineData(11, 1, 10)] // Above upper bound
        public void AgainstOutOfRange_WhenNumberIsOutOfRange_ThrowsArgumentOutOfRangeException(int value, int lower, int upper)
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string paramName = nameof(value);
            Action act = () => Guard.Against.OutOfRange(value, lower, upper, paramName);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage($"Input parameter '{paramName}' with value {value} is out of range. Allowed range: {lower}-{upper}.")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstOutOfRange_WhenNumberIsOutOfRange_ThrowsArgumentOutOfRangeException for '{value}'");
        }

        [Fact]
        public void AgainstZero_WhenNumberIsZero_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            int zero = 0;
            string paramName = nameof(zero);

            Action act = () => Guard.Against.Zero(zero, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"Input parameter '{paramName}' cannot be zero.")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstZero_WhenNumberIsZero_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void AgainstZero_WhenNumberIsNonZero_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            int nonZero = 5;
            string paramName = nameof(nonZero);

            Action act = () => Guard.Against.Zero(nonZero, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstZero_WhenNumberIsNonZero_DoesNotThrowException for '{paramName}'");
        }

        [Fact]
        public void AgainstNegative_WhenNumberIsNegative_ThrowsArgumentOutOfRangeException()
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            int negative = -5;
            string paramName = nameof(negative);

            Action act = () => Guard.Against.Negative(negative, paramName);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage($"Input parameter '{paramName}' cannot be negative.")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstNegative_WhenNumberIsNegative_ThrowsArgumentOutOfRangeException for '{paramName}'");
        }

        [Fact]
        public void AgainstNegative_WhenNumberIsPositiveOrZero_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception.
            int positive = 5;
            int zero = 0;
            string paramNamePositive = nameof(positive);
            string paramNameZero = nameof(zero);

            Action actPositive = () => Guard.Against.Negative(positive, paramNamePositive);
            Action actZero = () => Guard.Against.Negative(zero, paramNameZero);

            actPositive.Should().NotThrow();
            actZero.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstNegative_WhenNumberIsPositiveOrZero_DoesNotThrowException for '{paramNamePositive}' and '{paramNameZero}'");
        }

        [Theory]
        [InlineData("valid@example.com", @"^[^@\s]+@[^@\s]+\.[^@\s]+$")] // Valid email
        public void AgainstInvalidFormat_WhenStringMatchesRegex_DoesNotThrowException(string input, string regexPattern)
        {
            // AI Verifiable: Checks for absence of any exception.
            string paramName = nameof(input);
            Action act = () => Guard.Against.InvalidFormat(input, regexPattern, paramName);
            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: AgainstInvalidFormat_WhenStringMatchesRegex_DoesNotThrowException for '{input}'");
        }

        [Theory]
        [InlineData("invalid-email", @"^[^@\s]+@[^@\s]+\.[^@\s]+$")] // Invalid email
        [InlineData("123", @"^[a-zA-Z]+$")] // Invalid for alphabet only
        public void AgainstInvalidFormat_WhenStringDoesNotMatchRegex_ThrowsArgumentException(string input, string regexPattern)
        {
            // AI Verifiable: Checks for specific exception type, message content, and parameter name.
            string paramName = nameof(input);
            Action act = () => Guard.Against.InvalidFormat(input, regexPattern, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"Input parameter '{paramName}' with value '{input}' has an invalid format.")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: AgainstInvalidFormat_WhenStringDoesNotMatchRegex_ThrowsArgumentException for '{input}'");
        }

        // --- Phase 2: Library Extensibility and Integration Simulation ---

        [Fact]
        public void CustomGuard_NotFound_WhenObjectIsNull_ThrowsNotFoundException()
        {
            // AI Verifiable: Checks for custom exception type, message content, and parameter name for a custom guard.
            object result = null;
            string entityName = "User";
            string paramName = nameof(result);

            Action act = () => Guard.Against.NotFound(result, entityName, paramName);

            act.Should().ThrowExactly<NotFoundException>()
                .WithMessage($"{entityName} not found. (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: CustomGuard_NotFound_WhenObjectIsNull_ThrowsNotFoundException for '{entityName}'");
        }

        [Fact]
        public void CustomGuard_NotFound_WhenObjectIsNotNull_DoesNotThrowException()
        {
            // AI Verifiable: Checks for absence of any exception for a custom guard.
            object result = new object();
            string entityName = "User";
            string paramName = nameof(result);

            Action act = () => Guard.Against.NotFound(result, entityName, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: CustomGuard_NotFound_WhenObjectIsNotNull_DoesNotThrowException for '{entityName}'");
        }

        [Fact]
        public void CustomGuard_IsTrue_WhenConditionIsFalse_ThrowsArgumentException()
        {
            // AI Verifiable: Checks for custom guard's behavior with a false condition.
            bool condition = false;
            string message = "Condition must be true.";
            string paramName = nameof(condition);

            Action act = () => Guard.Against.IsTrue(condition, message, paramName);

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage($"{message} (Parameter '{paramName}')")
                .And.ParamName.Should().Be(paramName);
            _output.WriteLine($"Test Passed: CustomGuard_IsTrue_WhenConditionIsFalse_ThrowsArgumentException for '{paramName}'");
        }

        [Fact]
        public void CustomGuard_IsTrue_WhenConditionIsTrue_DoesNotThrowException()
        {
            // AI Verifiable: Checks for custom guard's behavior with a true condition.
            bool condition = true;
            string message = "Condition must be true.";
            string paramName = nameof(condition);

            Action act = () => Guard.Against.IsTrue(condition, message, paramName);

            act.Should().NotThrow();
            _output.WriteLine($"Test Passed: CustomGuard_IsTrue_WhenConditionIsTrue_DoesNotThrowException for '{paramName}'");
        }

        [Fact]
        public void PerformanceCheck_AgainstNull_ExecutesQuickly()
        {
            // AI Verifiable: Basic performance check. Executes a guard many times and asserts completion within a threshold.
            long iterations = 1_000_000;
            TimeSpan threshold = TimeSpan.FromMilliseconds(100); // Example threshold

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                object testObject = new object();
                Guard.Against.Null(testObject, nameof(testObject));
            }
            stopwatch.Stop();

            _output.WriteLine($"PerformanceCheck_AgainstNull_ExecutesQuickly: {stopwatch.ElapsedMilliseconds}ms for {iterations} iterations.");
            stopwatch.Elapsed.Should().BeLessThan(threshold, $"Guard.Against.Null should execute {iterations} times within {threshold.TotalMilliseconds}ms.");
        }
    }
}