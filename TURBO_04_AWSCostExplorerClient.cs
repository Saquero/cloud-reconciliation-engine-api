namespace CleanDddHexagonal.Infrastructure.ExternalProviders.AWS;

public interface IAWSCostExplorerClient
{
    Task<IEnumerable<AWSCostData>> GetCostDataAsync(
        string accountId,
        DateTime startDate,
        DateTime endDate);
}

public class AWSCostData
{
    public string Service { get; set; }
    public string ResourceId { get; set; }
    public decimal Cost { get; set; }
    public DateTime Date { get; set; }
}

public class AWSCostExplorerClientMock : IAWSCostExplorerClient
{
    public async Task<IEnumerable<AWSCostData>> GetCostDataAsync(
        string accountId,
        DateTime startDate,
        DateTime endDate)
    {
        await Task.Delay(100);

        var mockData = new List<AWSCostData>
        {
            new AWSCostData
            {
                Service = "EC2",
                ResourceId = "i-0123456789abcdef0",
                Cost = 512.50m,
                Date = startDate.AddDays(1)
            },
            new AWSCostData
            {
                Service = "S3",
                ResourceId = "arn:aws:s3:::bucket-prod",
                Cost = 85.75m,
                Date = startDate.AddDays(2)
            },
            new AWSCostData
            {
                Service = "RDS",
                ResourceId = "arn:aws:rds:us-east-1:123456789:db:maindb",
                Cost = 350.00m,
                Date = startDate.AddDays(3)
            },
            new AWSCostData
            {
                Service = "Lambda",
                ResourceId = "arn:aws:lambda:us-east-1:123456789:function:api",
                Cost = 45.25m,
                Date = startDate.AddDays(4)
            },
            new AWSCostData
            {
                Service = "DynamoDB",
                ResourceId = "arn:aws:dynamodb:us-east-1:123456789:table/users",
                Cost = 120.00m,
                Date = startDate.AddDays(5)
            }
        };

        return mockData;
    }
}
