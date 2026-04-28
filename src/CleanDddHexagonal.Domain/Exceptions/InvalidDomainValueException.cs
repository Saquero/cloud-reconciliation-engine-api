namespace CleanDddHexagonal.Domain.Exceptions;

public sealed class InvalidDomainValueException : DomainException
{
    public InvalidDomainValueException(string message) : base(message)
    {
    }
}
