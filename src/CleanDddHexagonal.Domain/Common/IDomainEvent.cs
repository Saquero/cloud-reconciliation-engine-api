namespace CleanDddHexagonal.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredAtUtc { get; }
}
