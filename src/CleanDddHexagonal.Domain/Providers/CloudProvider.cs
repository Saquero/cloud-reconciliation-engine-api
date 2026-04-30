namespace CleanDddHexagonal.Domain.Providers;

public enum CloudProvider
{
    Azure = 1,
    AWS = 2,
    GoogleCloud = 3,
    Custom = 4
}

public enum AzureServiceType
{
    VirtualMachine,
    Storage,
    Database,
    AppService,
    FunctionApp,
    Other
}

public enum AWSServiceType
{
    EC2,
    S3,
    RDS,
    Lambda,
    DynamoDB,
    Other
}
