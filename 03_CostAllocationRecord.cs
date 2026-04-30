namespace CleanDddHexagonal.Domain.CostAllocation;

public class CostAllocationRecord
{
    public int Id { get; private set; }
    public int TenantId { get; private set; }
    public int CustomerId { get; private set; }
    public string ServiceSku { get; private set; }
    public decimal ActualCost { get; private set; }
    public decimal BilledPrice { get; private set; }
    public decimal MarginAmount => BilledPrice - ActualCost;
    public decimal MarginPercentage => ActualCost > 0 ? (MarginAmount / ActualCost * 100) : 0;
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public CostAllocationRecord(
        int tenantId,
        int customerId,
        string serviceSku,
        decimal actualCost,
        decimal billedPrice,
        DateTime periodStart,
        DateTime periodEnd)
    {
        TenantId = tenantId;
        CustomerId = customerId;
        ServiceSku = serviceSku;
        ActualCost = actualCost;
        BilledPrice = billedPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        CreatedAt = DateTime.UtcNow;
    }
}
