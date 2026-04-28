using CleanDddHexagonal.Domain.Common;
using CleanDddHexagonal.Domain.Enums;
using CleanDddHexagonal.Domain.Events;
using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.Entities;

public sealed class ReconciliationIssue
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string ServiceSku { get; private set; } = string.Empty;
    public ReconciliationIssueType Type { get; private set; }
    public ReconciliationIssueStatus Status { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime DetectedAtUtc { get; private set; }
    public DateTime? ResolvedAtUtc { get; private set; }
    public string? ResolutionNote { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private ReconciliationIssue() { }

    private ReconciliationIssue(
        Guid id,
        Guid customerId,
        string serviceSku,
        ReconciliationIssueType type,
        string description,
        DateTime detectedAtUtc)
    {
        Id = id;
        CustomerId = customerId;
        ServiceSku = serviceSku;
        Type = type;
        Description = description;
        Status = ReconciliationIssueStatus.Open;
        DetectedAtUtc = detectedAtUtc;

        _domainEvents.Add(new UsageMismatchDetectedDomainEvent(
            Id,
            CustomerId,
            ServiceSku,
            Type,
            Description,
            detectedAtUtc
        ));
    }

    public static ReconciliationIssue Create(
        Guid customerId,
        string serviceSku,
        ReconciliationIssueType type,
        string description,
        DateTime detectedAtUtc)
    {
        return new ReconciliationIssue(
            Guid.NewGuid(),
            customerId,
            serviceSku,
            type,
            description,
            detectedAtUtc);
    }

    public void Resolve(string resolutionNote, DateTime resolvedAtUtc)
    {
        if (Status == ReconciliationIssueStatus.Resolved)
            throw new IssueAlreadyResolvedException();

        if (string.IsNullOrWhiteSpace(resolutionNote))
            throw new InvalidDomainValueException("Resolution note is required.");

        Status = ReconciliationIssueStatus.Resolved;
        ResolutionNote = resolutionNote.Trim();
        ResolvedAtUtc = resolvedAtUtc;

        _domainEvents.Add(new ReconciliationIssueResolvedDomainEvent(
            Id,
            CustomerId,
            ResolutionNote,
            resolvedAtUtc
        ));
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
