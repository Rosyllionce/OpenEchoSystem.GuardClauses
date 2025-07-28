using System;
using OpenEchoSystem.GuardClauses.Exceptions;

namespace OpenEchoSystem.GuardClauses
{
    public static class NotFoundGuardExtensions
    {
        public static void NotFound<TKey, TValue>(this IGuardClause guardClause, TKey key, Func<TKey, TValue?> lookup) where TValue : class
        {
            if (lookup(key) is null)
            {
                throw new NotFoundException($"Resource with key '{key}' was not found.");
            }
        }
    }
}