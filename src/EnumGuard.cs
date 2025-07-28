using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace OpenEchoSystem.GuardClauses
{
    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate enum values.
    /// </summary>
    public static class EnumGuardExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the provided enum <paramref name="input"/> is not a defined value for its enum type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The enum value to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="input"/> is not a defined value for <typeparamref name="TEnum"/>.</exception>
        public static void OutOfRange<TEnum>(this IGuardClause guard, TEnum input, [CallerArgumentExpression("input")] string? parameterName = null)
    where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            bool isFlags = enumType.IsDefined(typeof(FlagsAttribute), false);

            if (isFlags)
            {
                long allFlags = 0;
                foreach (var value in Enum.GetValues(enumType))
                {
                    allFlags |= Convert.ToInt64(value, CultureInfo.InvariantCulture);
                }

                if ((Convert.ToInt64(input, CultureInfo.InvariantCulture) & ~allFlags) != 0)
                {
                    throw new ArgumentOutOfRangeException(parameterName, $"Input parameter '{parameterName}' with value {input} contains undefined flags for enum type {enumType.Name}.");
                }
            }
            else if (!Enum.IsDefined(input))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input parameter '{parameterName}' with value {input} is not a valid value for enum type {enumType.Name}.");
            }
        }
    }
}
