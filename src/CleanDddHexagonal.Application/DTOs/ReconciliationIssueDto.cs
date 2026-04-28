using CleanDddHexagonal.Domain.Enums;

namespace CleanDddHexagonal.Application.DTOs;

public sealed record ReconciliationIssueDto(
    Guid Id,
    Guid CustomerId,
    string ServiceSku,
    ReconciliationIssueType Type,
    ReconciliationIssueStatus Status,
    string Description,
    DateTime DetectedAtUtc,
    DateTime? ResolvedAtUtc,
    string? ResolutionNote
);
