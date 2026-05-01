namespace CleanDddHexagonal.Application.Ports;

public interface IAzureUsageClient
{
    Task<IEnumerable<AzureUsageData>> GetUsageDataAsync(
        string subscriptionId,
        DateTime startDate,
        DateTime endDate);
}

public class AzureUsageData
{
    public string ResourceId { get; set; }
    public string ServiceName { get; set; }
    public decimal Cost { get; set; }
    public DateTime MeterDate { get; set; }
}
