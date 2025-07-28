namespace OpenEchoSystem.GuardClauses
{
    using System;

    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate <see cref="Guid"/> values.
    /// </summary>
    public static class GuardClauseGuidExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided <see cref="Guid"/> <paramref name="value"/> is empty.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="value">The <see cref="Guid"/> value to be validated.</param>
        /// <param name="parameterName">The name of the parameter being validated.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is an empty <see cref="Guid"/>.</exception>
        public static void Empty(this IGuardClause guard, Guid value, string parameterName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException($"'{parameterName}' cannot be an empty GUID.", parameterName);
            }
        }
    }
}