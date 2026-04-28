namespace CleanDddHexagonal.Domain.Exceptions;

public sealed class TaskAlreadyCompletedException : DomainException
{
    public TaskAlreadyCompletedException() : base("Task is already completed.")
    {
    }
}
