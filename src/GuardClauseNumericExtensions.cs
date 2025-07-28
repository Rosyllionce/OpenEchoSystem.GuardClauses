namespace OpenEchoSystem.GuardClauses
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate numeric values.
    /// </summary>
    public static class GuardClauseNumericExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the provided numeric <paramref name="value"/> is outside the specified <paramref name="minimum"/> and <paramref name="maximum"/> range (inclusive).
        /// </summary>
        /// <typeparam name="T">The type of the numeric value, which must implement <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="value">The numeric value to be validated.</param>
        /// <param name="minimum">The minimum allowed value.</param>
        /// <param name="maximum">The maximum allowed value.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than <paramref name="minimum"/> or greater than <paramref name="maximum"/>.</exception>
        public static void OutOfRange<T>(this IGuardClause guard, T value, T minimum, T maximum, [CallerArgumentExpression("value")] string? parameterName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input parameter '{parameterName}' with value {value} is out of range. Allowed range: {minimum}-{maximum}.");
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided numeric <paramref name="value"/> is zero.
        /// </summary>
        /// <typeparam name="T">The type of the numeric value, which must implement <see cref="INumber{T}"/>.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="value">The numeric value to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is zero.</exception>
        public static void Zero<T>(this IGuardClause guard, T value, [CallerArgumentExpression("value")] string? parameterName = null)
            where T : INumber<T>
        {
            if (value == T.Zero)
            {
                throw new ArgumentException($"Input parameter '{parameterName}' with value '{value}' cannot be zero.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided numeric <paramref name="value"/> is negative.
        /// </summary>
        /// <typeparam name="T">The type of the numeric value, which must implement <see cref="INumber{T}"/>.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="value">The numeric value to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is negative.</exception>
        public static void Negative<T>(this IGuardClause guard, T value, [CallerArgumentExpression("value")] string? parameterName = null)
            where T : INumber<T>
        {
            if (T.IsNegative(value))
            {
                throw new ArgumentException($"Input parameter '{parameterName}' with value '{value}' cannot be negative.", parameterName);
            }
        }
    }
}