using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Enums;

namespace CleanDddHexagonal.Domain.Services;

public sealed class UsageReconciliationService
{
    public IReadOnlyList<ReconciliationIssue> Compare(
        IReadOnlyList<InternalUsageRecord> internalRecords,
        IReadOnlyList<ExternalUsageSnapshot> externalSnapshots,
        DateTime detectedAtUtc)
    {
        var issues = new List<ReconciliationIssue>();

        foreach (var internalRecord in internalRecords)
        {
            var external = externalSnapshots.FirstOrDefault(snapshot =>
                snapshot.CustomerId == internalRecord.CustomerId &&
                snapshot.Provider == internalRecord.Provider &&
                snapshot.ServiceSku == internalRecord.ServiceSku);

            if (external is null)
            {
                issues.Add(ReconciliationIssue.Create(
                    internalRecord.CustomerId,
                    internalRecord.ServiceSku,
                    ReconciliationIssueType.MissingExternalSnapshot,
                    $"Internal record exists for {internalRecord.ServiceSku}, but no external provider snapshot was found.",
                    detectedAtUtc));

                continue;
            }

            if (internalRecord.SeatCount != external.SeatCount)
            {
                issues.Add(ReconciliationIssue.Create(
                    internalRecord.CustomerId,
                    internalRecord.ServiceSku,
                    ReconciliationIssueType.SeatCountMismatch,
                    $"Seat count mismatch for {internalRecord.ServiceSku}: internal={internalRecord.SeatCount}, external={external.SeatCount}.",
                    detectedAtUtc));
            }

            if (internalRecord.MonthlyCost != external.MonthlyCost || internalRecord.Currency != external.Currency)
            {
                issues.Add(ReconciliationIssue.Create(
                    internalRecord.CustomerId,
                    internalRecord.ServiceSku,
                    ReconciliationIssueType.CostMismatch,
                    $"Cost mismatch for {internalRecord.ServiceSku}: internal={internalRecord.MonthlyCost} {internalRecord.Currency}, external={external.MonthlyCost} {external.Currency}.",
                    detectedAtUtc));
            }
        }

        foreach (var external in externalSnapshots)
        {
            var internalRecord = internalRecords.FirstOrDefault(record =>
                record.CustomerId == external.CustomerId &&
                record.Provider == external.Provider &&
                record.ServiceSku == external.ServiceSku);

            if (internalRecord is null)
            {
                issues.Add(ReconciliationIssue.Create(
                    external.CustomerId,
                    external.ServiceSku,
                    ReconciliationIssueType.MissingInternalRecord,
                    $"External snapshot exists for {external.ServiceSku}, but no internal record was found.",
                    detectedAtUtc));
            }
        }

        return issues;
    }
}
