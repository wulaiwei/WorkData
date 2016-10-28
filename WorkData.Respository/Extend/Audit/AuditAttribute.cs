namespace System.Data.Entity.Audit
{
    /// <summary>
    /// Indicates that a class can be audited.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="NotAuditedAttribute" /> attribute to prevent a field from being included in the audit
    /// </remarks>
    /// <seealso cref="NotAuditedAttribute" />
    /// <seealso cref="AlwaysAuditAttribute" />
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public class AuditAttribute : Attribute
    {
    }
}