using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Domain.Common;
using Microsoft.Extensions.Logging;

namespace CleanDddHexagonal.Infrastructure.Events;

public sealed class LoggingDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<LoggingDomainEventDispatcher> _logger;

    public LoggingDomainEventDispatcher(ILogger<LoggingDomainEventDispatcher> logger)
    {
        _logger = logger;
    }

    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation(
                "Domain event dispatched: {DomainEventType} at {OccurredAtUtc}",
                domainEvent.GetType().Name,
                domainEvent.OccurredAtUtc);
        }

        return Task.CompletedTask;
    }
}
