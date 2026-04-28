using CleanDddHexagonal.Domain.Enums;
using CleanDddHexagonal.Domain.ValueObjects;

namespace CleanDddHexagonal.Domain.Entities;

public sealed class ExternalUsageSnapshot
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public CloudProvider Provider { get; private set; }
    public string ServiceSku { get; private set; } = string.Empty;
    public int SeatCount { get; private set; }
    public decimal MonthlyCost { get; private set; }
    public string Currency { get; private set; } = "EUR";
    public DateTime SnapshotAtUtc { get; private set; }

    private ExternalUsageSnapshot() { }

    private ExternalUsageSnapshot(
        Guid id,
        Guid customerId,
        CloudProvider provider,
        ServiceSku serviceSku,
        int seatCount,
        MoneyAmount monthlyCost,
        DateTime snapshotAtUtc)
    {
        Id = id;
        CustomerId = customerId;
        Provider = provider;
        ServiceSku = serviceSku.Value;
        SeatCount = seatCount;
        MonthlyCost = monthlyCost.Value;
        Currency = monthlyCost.Currency;
        SnapshotAtUtc = snapshotAtUtc;
    }

    public static ExternalUsageSnapshot Create(
        Guid customerId,
        CloudProvider provider,
        ServiceSku serviceSku,
        int seatCount,
        MoneyAmount monthlyCost,
        DateTime snapshotAtUtc)
    {
        if (seatCount < 0)
            throw new ArgumentException("Seat count cannot be negative.");

        return new ExternalUsageSnapshot(
            Guid.NewGuid(),
            customerId,
            provider,
            serviceSku,
            seatCount,
            monthlyCost,
            snapshotAtUtc);
    }
}
