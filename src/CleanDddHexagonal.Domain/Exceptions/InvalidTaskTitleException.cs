namespace CleanDddHexagonal.Domain.Exceptions;

public sealed class InvalidTaskTitleException : DomainException
{
    public InvalidTaskTitleException(string message) : base(message)
    {
    }
}
