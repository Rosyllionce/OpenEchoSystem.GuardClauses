namespace OpenEchoSystem.GuardClauses
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate boolean conditions.
    /// </summary>
    public static class GuardClauseBooleanExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="condition">The boolean condition to be evaluated. If true, an exception is thrown.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <param name="message">Optional custom error message.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="condition"/> is true.</exception>
        public static void CustomCondition(this IGuardClause guard, bool condition, [CallerArgumentExpression("condition")] string? parameterName = null, string? message = null)
        {
            if (condition)
            {
                throw new ArgumentException(message ?? $"Input parameter '{parameterName}' failed custom condition.", parameterName);
            }
        }
    }
}