using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Domain.Entities;

namespace CleanDddHexagonal.Application.UseCases;

public static class ReconciliationMapper
{
    public static CustomerDto ToDto(CustomerAccount customer)
    {
        return new CustomerDto(customer.Id, customer.Name, customer.CreatedAtUtc);
    }

    public static UsageRecordDto ToDto(InternalUsageRecord record)
    {
        return new UsageRecordDto(
            record.Id,
            record.CustomerId,
            record.Provider,
            record.ServiceSku,
            record.SeatCount,
            record.MonthlyCost,
            record.Currency,
            record.RecordedAtUtc);
    }

    public static UsageRecordDto ToDto(ExternalUsageSnapshot snapshot)
    {
        return new UsageRecordDto(
            snapshot.Id,
            snapshot.CustomerId,
            snapshot.Provider,
            snapshot.ServiceSku,
            snapshot.SeatCount,
            snapshot.MonthlyCost,
            snapshot.Currency,
            snapshot.SnapshotAtUtc);
    }

    public static ReconciliationIssueDto ToDto(ReconciliationIssue issue)
    {
        return new ReconciliationIssueDto(
            issue.Id,
            issue.CustomerId,
            issue.ServiceSku,
            issue.Type,
            issue.Status,
            issue.Description,
            issue.DetectedAtUtc,
            issue.ResolvedAtUtc,
            issue.ResolutionNote);
    }
}
