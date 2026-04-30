namespace CleanDddHexagonal.Application.DTOs.Providers;

public class RegisterProviderCredentialsRequest
{
    public int TenantId { get; set; }
    public string Provider { get; set; }
    public string ProviderAccountId { get; set; }
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string SubscriptionId { get; set; }
}

public class ProviderCredentialsDto
{
    public int Id { get; set; }
    public string Provider { get; set; }
    public string ProviderAccountId { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastValidatedAt { get; set; }
}

public class CostAllocationReportDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalActualCost { get; set; }
    public decimal TotalBilledPrice { get; set; }
    public decimal TotalMargin { get; set; }
    public decimal MarginPercentage { get; set; }
}

public class AzureUsageDto
{
    public string ResourceId { get; set; }
    public string ServiceName { get; set; }
    public decimal Cost { get; set; }
    public DateTime MeterDate { get; set; }
}

public class AWSCostDto
{
    public string Service { get; set; }
    public string ResourceId { get; set; }
    public decimal Cost { get; set; }
    public DateTime Date { get; set; }
}
