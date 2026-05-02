namespace CleanDddHexagonal.Tests;

using CleanDddHexagonal.Domain.CostAllocation;
using CleanDddHexagonal.Domain.Providers;
using Xunit;

public class CostAllocationDomainTests
{
    [Fact]
    public void CreateCostAllocationRecord_WithValidData_ShouldCalculateMarginCorrectly()
    {
        // Arrange
        int tenantId = 1;
        int customerId = 100;
        string serviceSku = "Virtual-Machines";
        decimal actualCost = 1000m;
        decimal billedPrice = 1250m;
        DateTime periodStart = DateTime.UtcNow.AddMonths(-1);
        DateTime periodEnd = DateTime.UtcNow;

        // Act
        var record = new CostAllocationRecord(
            tenantId,
            customerId,
            serviceSku,
            actualCost,
            billedPrice,
            periodStart,
            periodEnd);

        // Assert
        Assert.Equal(250m, record.MarginAmount);
        Assert.Equal(25m, record.MarginPercentage);
        Assert.Equal(actualCost, record.ActualCost);
        Assert.Equal(billedPrice, record.BilledPrice);
    }

    [Fact]
    public void CreateCostAllocationRecord_WithZeroActualCost_ShouldHandleGracefully()
    {
        // Arrange
        int tenantId = 1;
        int customerId = 100;
        string serviceSku = "Free-Service";
        decimal actualCost = 0m;
        decimal billedPrice = 100m;
        DateTime periodStart = DateTime.UtcNow.AddMonths(-1);
        DateTime periodEnd = DateTime.UtcNow;

        // Act
        var record = new CostAllocationRecord(
            tenantId,
            customerId,
            serviceSku,
            actualCost,
            billedPrice,
            periodStart,
            periodEnd);

        // Assert
        Assert.Equal(0, record.MarginPercentage);
    }
}

public class ProviderCredentialsDomainTests
{
    [Fact]
    public void CreateProviderCredentials_WithValidData_ShouldInitializeCorrectly()
    {
        // Arrange
        int tenantId = 1;
        var provider = CleanDddHexagonal.Domain.Providers.CloudProvider.Azure;
        string accountId = "test@example.com";
        string apiKey = "encrypted_key";
        string secretKey = "encrypted_secret";
        string subscriptionId = "sub-123";

        // Act
        var credentials = new ProviderCredentials(
            tenantId,
            provider,
            accountId,
            apiKey,
            secretKey,
            subscriptionId);

        // Assert
        Assert.Equal(tenantId, credentials.TenantId);
        Assert.Equal(provider, credentials.Provider);
        Assert.Equal(accountId, credentials.ProviderAccountId);
        Assert.True(credentials.IsActive);
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var credentials = new ProviderCredentials(
            1,
            CleanDddHexagonal.Domain.Providers.CloudProvider.Azure,
            "test@example.com",
            "key",
            "secret",
            "sub-123");

        // Act
        credentials.Deactivate();

        // Assert
        Assert.False(credentials.IsActive);
    }

    [Fact]
    public void ValidateCredentials_ShouldUpdateLastValidatedAt()
    {
        // Arrange
        var credentials = new ProviderCredentials(
            1,
            CleanDddHexagonal.Domain.Providers.CloudProvider.Azure,
            "test@example.com",
            "key",
            "secret",
            "sub-123");

        var beforeValidation = credentials.LastValidatedAt;
        System.Threading.Thread.Sleep(100);

        // Act
        credentials.ValidateCredentials();

        // Assert
        Assert.True(credentials.LastValidatedAt > beforeValidation);
    }
}
