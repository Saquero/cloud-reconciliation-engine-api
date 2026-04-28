using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Enums;

namespace CleanDddHexagonal.Domain.Repositories;

public interface IUsageRecordRepository
{
    Task AddInternalAsync(InternalUsageRecord record);
    Task AddExternalAsync(ExternalUsageSnapshot snapshot);

    Task<IReadOnlyList<InternalUsageRecord>> GetInternalByCustomerAsync(Guid customerId);
    Task<IReadOnlyList<ExternalUsageSnapshot>> GetExternalByCustomerAsync(Guid customerId);

    Task SaveChangesAsync();
}
