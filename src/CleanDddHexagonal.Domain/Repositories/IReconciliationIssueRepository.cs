using CleanDddHexagonal.Domain.Entities;

namespace CleanDddHexagonal.Domain.Repositories;

public interface IReconciliationIssueRepository
{
    Task AddRangeAsync(IEnumerable<ReconciliationIssue> issues);
    Task<ReconciliationIssue?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ReconciliationIssue>> GetOpenAsync();
    Task SaveChangesAsync();
}
