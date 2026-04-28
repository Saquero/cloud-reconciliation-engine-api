using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanDddHexagonal.Infrastructure.Repositories;

public sealed class EfCustomerAccountRepository : ICustomerAccountRepository
{
    private readonly AppDbContext _dbContext;

    public EfCustomerAccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerAccount?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<CustomerAccount>> GetAllAsync()
    {
        return await _dbContext.Customers
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task AddAsync(CustomerAccount customer)
    {
        await _dbContext.Customers.AddAsync(customer);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
