namespace CleanDddHexagonal.Infrastructure.Repositories;

using CleanDddHexagonal.Domain.CostAllocation;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class EfCostAllocationRepository : ICostAllocationRepository
{
    private readonly AppDbContext _context;

    public EfCostAllocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CostAllocationRecord> AddAsync(CostAllocationRecord record)
    {
        _context.CostAllocations.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<IEnumerable<CostAllocationRecord>> GetByCustomerAsync(
        int customerId,
        DateTime startDate,
        DateTime endDate)
    {
        return await _context.CostAllocations
            .Where(c => c.CustomerId == customerId
                && c.PeriodStart >= startDate
                && c.PeriodEnd <= endDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<CostAllocationRecord>> GetByTenantAsync(
        int tenantId,
        DateTime startDate,
        DateTime endDate)
    {
        return await _context.CostAllocations
            .Where(c => c.TenantId == tenantId
                && c.PeriodStart >= startDate
                && c.PeriodEnd <= endDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalMarginAsync(int tenantId, DateTime startDate, DateTime endDate)
    {
        return await _context.CostAllocations
            .Where(c => c.TenantId == tenantId
                && c.PeriodStart >= startDate
                && c.PeriodEnd <= endDate)
            .SumAsync(c => c.MarginAmount);
    }
}
