namespace OpenEchoSystem.GuardClauses
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate objects.
    /// </summary>
    public static class GuardClauseObjectExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the provided object <paramref name="input"/> is null.
        /// </summary>
        /// <typeparam name="T">The type of the object, which must be a reference type.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The object to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
        public static void Null<T>(this IGuardClause guard, T? input, [CallerArgumentExpression("input")] string? parameterName = null) where T : class
        {
            if (input == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided object <paramref name="input"/> is null, indicating that an item was not found.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The object representing the looked-up item to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is null, indicating the item was not found.</exception>
        public static void NotFound(this IGuardClause guard, object? input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input == null)
            {
                throw new ArgumentException($"Item was not found.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the provided collection <paramref name="input"/> is null, or an <see cref="ArgumentException"/> if it is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The collection to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is an empty collection.</exception>
        public static void NullOrEmpty<T>(this IGuardClause guard, IEnumerable<T>? input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input == null)
            {
                throw new ArgumentNullException(parameterName, $"Required input {parameterName} cannot be null.");
            }

            if (!input.Any())
            {
                throw new ArgumentException($"Required input {parameterName} cannot be an empty collection.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the provided <see cref="DateTime"/> <paramref name="input"/> is outside the specified <paramref name="minimum"/> and <paramref name="maximum"/> range (inclusive).
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The <see cref="DateTime"/> value to be validated.</param>
        /// <param name="minimum">The minimum allowed <see cref="DateTime"/>.</param>
        /// <param name="maximum">The maximum allowed <see cref="DateTime"/>.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="input"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.</exception>
        public static void OutOfRange(this IGuardClause guard, DateTime input, DateTime minimum, DateTime maximum, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input < minimum || input > maximum)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input parameter '{parameterName}' with value {input} is out of range. Allowed range: {minimum}-{maximum}.");
            }
        }
    }
}