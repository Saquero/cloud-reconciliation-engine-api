using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Enums;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanDddHexagonal.Infrastructure.Repositories;

public sealed class EfReconciliationIssueRepository : IReconciliationIssueRepository
{
    private readonly AppDbContext _dbContext;

    public EfReconciliationIssueRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<ReconciliationIssue> issues)
    {
        await _dbContext.ReconciliationIssues.AddRangeAsync(issues);
    }

    public async Task<ReconciliationIssue?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ReconciliationIssues.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<ReconciliationIssue>> GetOpenAsync()
    {
        return await _dbContext.ReconciliationIssues
            .Where(x => x.Status == ReconciliationIssueStatus.Open)
            .OrderByDescending(x => x.DetectedAtUtc)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
