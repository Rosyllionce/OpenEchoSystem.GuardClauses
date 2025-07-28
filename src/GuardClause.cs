namespace OpenEchoSystem.GuardClauses
{
    /// <summary>
    /// The default, internal implementation of IGuardClause.
    /// This is a lightweight, sealed class to prevent external instantiation or inheritance.
    /// </summary>
    internal sealed class GuardClause : IGuardClause
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClause"/> class.
        /// </summary>
        internal GuardClause() { }
    }
}