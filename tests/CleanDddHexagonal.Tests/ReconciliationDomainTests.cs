using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Enums;
using CleanDddHexagonal.Domain.Events;
using CleanDddHexagonal.Domain.Services;
using CleanDddHexagonal.Domain.ValueObjects;

namespace CleanDddHexagonal.Tests;

public class ReconciliationDomainTests
{
    private static readonly DateTime FixedDate = new(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Compare_ShouldDetectSeatCountMismatch()
    {
        var customerId = Guid.NewGuid();

        var internalRecords = new List<InternalUsageRecord>
        {
            InternalUsageRecord.Create(
                customerId,
                CloudProvider.MicrosoftCsp,
                ServiceSku.Create("MS-365-BUSINESS-PREMIUM"),
                20,
                MoneyAmount.Create(300, "EUR"),
                FixedDate)
        };

        var externalSnapshots = new List<ExternalUsageSnapshot>
        {
            ExternalUsageSnapshot.Create(
                customerId,
                CloudProvider.MicrosoftCsp,
                ServiceSku.Create("MS-365-BUSINESS-PREMIUM"),
                23,
                MoneyAmount.Create(300, "EUR"),
                FixedDate)
        };

        var service = new UsageReconciliationService();

        var issues = service.Compare(internalRecords, externalSnapshots, FixedDate);

        var issue = Assert.Single(issues);
        Assert.Equal(ReconciliationIssueType.SeatCountMismatch, issue.Type);
        Assert.Equal(ReconciliationIssueStatus.Open, issue.Status);
        Assert.Contains("internal=20, external=23", issue.Description);
    }

    [Fact]
    public void ReconciliationIssue_ShouldRaiseDomainEvent_WhenCreated()
    {
        var issue = ReconciliationIssue.Create(
            Guid.NewGuid(),
            "MS-365-BUSINESS-PREMIUM",
            ReconciliationIssueType.CostMismatch,
            "Cost mismatch detected.",
            FixedDate);

        var domainEvent = Assert.Single(issue.DomainEvents);
        Assert.IsType<UsageMismatchDetectedDomainEvent>(domainEvent);
    }

    [Fact]
    public void ReconciliationIssue_ShouldRaiseDomainEvent_WhenResolved()
    {
        var issue = ReconciliationIssue.Create(
            Guid.NewGuid(),
            "AZURE-COMPUTE",
            ReconciliationIssueType.CostMismatch,
            "Cost mismatch detected.",
            FixedDate);

        issue.ClearDomainEvents();

        issue.Resolve("External provider invoice confirmed.", FixedDate.AddHours(1));

        Assert.Equal(ReconciliationIssueStatus.Resolved, issue.Status);
        Assert.NotNull(issue.ResolvedAtUtc);
        Assert.Single(issue.DomainEvents);
        Assert.IsType<ReconciliationIssueResolvedDomainEvent>(issue.DomainEvents.First());
    }
}
