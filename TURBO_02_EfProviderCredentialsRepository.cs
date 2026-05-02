namespace CleanDddHexagonal.Infrastructure.Repositories;

using CleanDddHexagonal.Domain.Providers;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class EfProviderCredentialsRepository : IProviderCredentialsRepository
{
    private readonly AppDbContext _context;

    public EfProviderCredentialsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProviderCredentials> AddAsync(ProviderCredentials credentials)
    {
        _context.ProviderCredentials.Add(credentials);
        await _context.SaveChangesAsync();
        return credentials;
    }

    public async Task<ProviderCredentials> GetByIdAsync(int id)
    {
        return await _context.ProviderCredentials.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProviderCredentials>> GetByTenantAsync(int tenantId)
    {
        return await _context.ProviderCredentials
            .Where(p => p.TenantId == tenantId && p.IsActive)
            .ToListAsync();
    }

    public async Task<ProviderCredentials> UpdateAsync(ProviderCredentials credentials)
    {
        _context.ProviderCredentials.Update(credentials);
        await _context.SaveChangesAsync();
        return credentials;
    }

    public async Task DeleteAsync(int id)
    {
        var credentials = await GetByIdAsync(id);
        if (credentials != null)
        {
            credentials.Deactivate();
            await UpdateAsync(credentials);
        }
    }
}
