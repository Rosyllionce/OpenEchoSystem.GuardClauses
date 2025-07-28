namespace OpenEchoSystem.GuardClauses
{
    /// <summary>
    /// The primary entry point for all guard clauses.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Provides a singleton instance of the IGuardClause implementation.
        /// </summary>
        public static IGuardClause Against { get; } = new GuardClause();
    }
}