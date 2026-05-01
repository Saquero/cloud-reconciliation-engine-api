namespace CleanDddHexagonal.Domain.Repositories;

using CleanDddHexagonal.Domain.CostAllocation;

public interface ICostAllocationRepository
{
    Task<CostAllocationRecord> AddAsync(CostAllocationRecord record);
    Task<IEnumerable<CostAllocationRecord>> GetByCustomerAsync(int customerId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<CostAllocationRecord>> GetByTenantAsync(int tenantId, DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalMarginAsync(int tenantId, DateTime startDate, DateTime endDate);
}
