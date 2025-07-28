namespace OpenEchoSystem.GuardClauses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides extension methods for <see cref="IGuardClause"/> to validate string values.
    /// </summary>
    public static class GuardClauseStringExtensions
    {
       private static readonly Regex EmailRegex = new(
           @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$",
           RegexOptions.Compiled | RegexOptions.IgnoreCase,
           TimeSpan.FromMilliseconds(250));

       private static readonly Regex UrlRegex = new(
           @"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)",
           RegexOptions.Compiled | RegexOptions.IgnoreCase,
           TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided string <paramref name="input"/> does not match the specified <paramref name="regexPattern"/>.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated.</param>
        /// <param name="regexPattern">The regular expression pattern to match against the input string.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> does not match the <paramref name="regexPattern"/>.</exception>
        public static void InvalidFormat(this IGuardClause guard, string input, string regexPattern, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            InvalidFormat(guard, input, regexPattern, $"Input {parameterName} does not match the required format.", parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided string <paramref name="input"/> does not match the specified <paramref name="regexPattern"/>.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated.</param>
        /// <param name="regexPattern">The regular expression pattern to match against the input string.</param>
        /// <param name="userMessage">The error message to use if the exception is thrown.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> does not match the <paramref name="regexPattern"/>.</exception>
        public static void InvalidFormat(this IGuardClause guard, string input, string regexPattern, string userMessage, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            // This overload now calls the core implementation, ensuring parameterName is correctly passed.
            // The CallerArgumentExpression on this method will capture the expression for 'input' from the calling site.
            // However, if this method is called directly, 'parameterName' will be inferred.
            // If it's called from another overload, 'parameterName' will be explicitly passed.
            InvalidFormatInternal(guard, input, regexPattern, userMessage, parameterName);
        }

        /// <summary>
        /// Core implementation for the InvalidFormat guard clause.
        /// Throws an <see cref="ArgumentException"/> if the provided string <paramref name="input"/> does not match the specified <paramref name="regexPattern"/>.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated.</param>
        /// <param name="regexPattern">The regular expression pattern to match against the input string.</param>
        /// <param name="message">The error message to use if the exception is thrown.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> does not match the <paramref name="regexPattern"/>.</exception>
        private static void InvalidFormatInternal(IGuardClause guard, string input, string regexPattern, string message, string? parameterName)
        {
           if (string.IsNullOrEmpty(input))
           {
               return;
           }

            if (!Regex.IsMatch(input, regexPattern))
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided string <paramref name="input"/> is not a valid email address.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated as an email address.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is null, whitespace, or not a valid email format.</exception>
        public static void InvalidEmail(this IGuardClause guard, string input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
           if (string.IsNullOrWhiteSpace(input))
           {
               return;
           }

            if (!EmailRegex.IsMatch(input))
            {
                throw new ArgumentException($"Input {parameterName} is not a valid email address.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided string <paramref name="input"/> is not a valid absolute URL.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated as a URL.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is null, whitespace, or not a valid absolute URL format.</exception>
        public static void InvalidUrl(this IGuardClause guard, string input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
           if (string.IsNullOrWhiteSpace(input))
           {
               return;
           }

            if (!UrlRegex.IsMatch(input))
            {
                throw new ArgumentException($"Input {parameterName} is not a valid URL.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the provided string <paramref name="input"/> is null,
        /// or an <see cref="ArgumentException"/> if it is empty.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is an empty string.</exception>
        public static void NullOrEmpty(this IGuardClause guard, [NotNull] string? input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (input == string.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the provided string <paramref name="input"/> is null,
        /// or an <see cref="ArgumentException"/> if it is empty or consists only of white-space characters.
        /// </summary>
        /// <param name="guard">The <see cref="IGuardClause"/> instance.</param>
        /// <param name="input">The string to be validated.</param>
        /// <param name="parameterName">The name of the parameter, captured automatically by <see cref="CallerArgumentExpressionAttribute"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="input"/> is an empty or white-space string.</exception>
        public static void NullOrWhiteSpace(this IGuardClause guard, [NotNull] string? input, [CallerArgumentExpression("input")] string? parameterName = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"Required input {parameterName} was empty or white-space.", parameterName);
            }
        }
    }
}