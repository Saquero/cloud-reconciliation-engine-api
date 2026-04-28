namespace CleanDddHexagonal.Domain.Exceptions;

public sealed class IssueAlreadyResolvedException : DomainException
{
    public IssueAlreadyResolvedException() : base("Reconciliation issue is already resolved.")
    {
    }
}
