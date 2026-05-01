namespace CleanDddHexagonal.Application.UseCases.CostAllocation;

using CleanDddHexagonal.Application.Common;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Domain.CostAllocation;
using CleanDddHexagonal.Domain.Repositories;

public class SyncAzureCostsUseCase
{
    private readonly IAzureUsageClient _azureClient;
    private readonly IProviderCredentialsRepository _providerRepository;
    private readonly ICostAllocationRepository _costAllocationRepository;
    private readonly ICustomerAccountRepository _customerRepository;

    public SyncAzureCostsUseCase(
        IAzureUsageClient azureClient,
        IProviderCredentialsRepository providerRepository,
        ICostAllocationRepository costAllocationRepository,
        ICustomerAccountRepository customerRepository)
    {
        _azureClient = azureClient;
        _providerRepository = providerRepository;
        _costAllocationRepository = costAllocationRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<SyncResultDto>> ExecuteAsync(int providerId, int tenantId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var provider = await _providerRepository.GetByIdAsync(providerId);
            if (provider == null)
                return Result<SyncResultDto>.Fail("Provider not found");

            if (provider.TenantId != tenantId)
                return Result<SyncResultDto>.Fail("Provider does not belong to this tenant");

            var subscriptionId = provider.SubscriptionId;
            if (string.IsNullOrEmpty(subscriptionId))
                return Result<SyncResultDto>.Fail("Azure Subscription ID not configured");

            var usageData = await _azureClient.GetUsageDataAsync(subscriptionId, startDate, endDate);

            var customers = await _customerRepository.GetAllAsync();
            int recordsCreated = 0;

            foreach (var usage in usageData)
            {
                var customer = customers.FirstOrDefault();
                if (customer == null) continue;

                var actualCost = usage.Cost;
                var billedPrice = actualCost * 1.25m;

                var costRecord = new CostAllocationRecord(
                    tenantId,
                    customer.Id.GetHashCode(),
                    usage.ServiceName,
                    actualCost,
                    billedPrice,
                    startDate,
                    endDate);

                await _costAllocationRepository.AddAsync(costRecord);
                recordsCreated++;
            }

            provider.ValidateCredentials();
            await _providerRepository.UpdateAsync(provider);

            return Result<SyncResultDto>.Ok(new SyncResultDto
            {
                ProviderId = providerId,
                RecordsCreated = recordsCreated,
                TotalActualCost = usageData.Sum(u => u.Cost),
                SyncDate = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return Result<SyncResultDto>.Fail($"Sync failed: {ex.Message}");
        }
    }
}

public class SyncResultDto
{
    public int ProviderId { get; set; }
    public int RecordsCreated { get; set; }
    public decimal TotalActualCost { get; set; }
    public DateTime SyncDate { get; set; }
}
