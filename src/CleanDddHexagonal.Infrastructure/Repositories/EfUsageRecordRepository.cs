using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanDddHexagonal.Infrastructure.Repositories;

public sealed class EfUsageRecordRepository : IUsageRecordRepository
{
    private readonly AppDbContext _dbContext;

    public EfUsageRecordRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddInternalAsync(InternalUsageRecord record)
    {
        await _dbContext.InternalUsageRecords.AddAsync(record);
    }

    public async Task AddExternalAsync(ExternalUsageSnapshot snapshot)
    {
        await _dbContext.ExternalUsageSnapshots.AddAsync(snapshot);
    }

    public async Task<IReadOnlyList<InternalUsageRecord>> GetInternalByCustomerAsync(Guid customerId)
    {
        return await _dbContext.InternalUsageRecords
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ExternalUsageSnapshot>> GetExternalByCustomerAsync(Guid customerId)
    {
        return await _dbContext.ExternalUsageSnapshots
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
