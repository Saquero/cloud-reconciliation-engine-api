using CleanDddHexagonal.Domain.Common;

namespace CleanDddHexagonal.Application.Ports;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
}
