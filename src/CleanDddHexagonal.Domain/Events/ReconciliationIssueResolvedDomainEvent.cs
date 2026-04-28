using CleanDddHexagonal.Domain.Common;

namespace CleanDddHexagonal.Domain.Events;

public sealed record ReconciliationIssueResolvedDomainEvent(
    Guid IssueId,
    Guid CustomerId,
    string ResolutionNote,
    DateTime OccurredAtUtc
) : IDomainEvent;
