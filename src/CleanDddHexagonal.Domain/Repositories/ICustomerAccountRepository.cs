using CleanDddHexagonal.Domain.Entities;

namespace CleanDddHexagonal.Domain.Repositories;

public interface ICustomerAccountRepository
{
    Task<CustomerAccount?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<CustomerAccount>> GetAllAsync();
    Task AddAsync(CustomerAccount customer);
    Task SaveChangesAsync();
}
