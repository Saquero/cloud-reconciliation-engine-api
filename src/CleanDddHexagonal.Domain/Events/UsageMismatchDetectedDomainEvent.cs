using CleanDddHexagonal.Domain.Common;
using CleanDddHexagonal.Domain.Enums;

namespace CleanDddHexagonal.Domain.Events;

public sealed record UsageMismatchDetectedDomainEvent(
    Guid IssueId,
    Guid CustomerId,
    string ServiceSku,
    ReconciliationIssueType IssueType,
    string Description,
    DateTime OccurredAtUtc
) : IDomainEvent;
