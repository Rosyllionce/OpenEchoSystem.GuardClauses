using System;

namespace OpenEchoSystem.GuardClauses
{
    public static class CustomConditionGuardExtensions
    {
        public static void CustomCondition(this IGuardClause guardClause, bool condition, string parameterName)
        {
            if (condition)
            {
                throw new ArgumentException("The specified condition was met, indicating an invalid state.", parameterName);
            }
        }
    }
}