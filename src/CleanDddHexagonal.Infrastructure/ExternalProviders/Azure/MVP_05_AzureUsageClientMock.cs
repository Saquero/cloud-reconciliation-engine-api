namespace CleanDddHexagonal.Infrastructure.ExternalProviders.Azure;

public class AzureUsageClientMock : IAzureUsageClient
{
    public async Task<IEnumerable<AzureUsageData>> GetUsageDataAsync(
        string subscriptionId,
        DateTime startDate,
        DateTime endDate)
    {
        await Task.Delay(100);

        var mockData = new List<AzureUsageData>
        {
            new AzureUsageData
            {
                ResourceId = "/subscriptions/test-sub-123/resourceGroups/prod/providers/Microsoft.Compute/virtualMachines/vm-prod-01",
                ServiceName = "Virtual Machines",
                Cost = 450.75m,
                MeterDate = startDate.AddDays(1)
            },
            new AzureUsageData
            {
                ResourceId = "/subscriptions/test-sub-123/resourceGroups/prod/providers/Microsoft.Storage/storageAccounts/storage01",
                ServiceName = "Storage",
                Cost = 125.50m,
                MeterDate = startDate.AddDays(2)
            },
            new AzureUsageData
            {
                ResourceId = "/subscriptions/test-sub-123/resourceGroups/prod/providers/Microsoft.Sql/servers/sqlserver01/databases/maindb",
                ServiceName = "Azure SQL Database",
                Cost = 320.25m,
                MeterDate = startDate.AddDays(3)
            },
            new AzureUsageData
            {
                ResourceId = "/subscriptions/test-sub-123/resourceGroups/prod/providers/Microsoft.Web/sites/webapp-prod",
                ServiceName = "App Service",
                Cost = 89.99m,
                MeterDate = startDate.AddDays(4)
            },
            new AzureUsageData
            {
                ResourceId = "/subscriptions/test-sub-123/resourceGroups/dev/providers/Microsoft.Compute/virtualMachines/vm-dev-01",
                ServiceName = "Virtual Machines",
                Cost = 225.00m,
                MeterDate = startDate.AddDays(5)
            }
        };

        return mockData;
    }
}
