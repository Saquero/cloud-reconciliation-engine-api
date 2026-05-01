namespace CleanDddHexagonal.Domain.Repositories;

using CleanDddHexagonal.Domain.Providers;

public interface IProviderCredentialsRepository
{
    Task<ProviderCredentials> AddAsync(ProviderCredentials credentials);
    Task<ProviderCredentials> GetByIdAsync(int id);
    Task<IEnumerable<ProviderCredentials>> GetByTenantAsync(int tenantId);
    Task<ProviderCredentials> UpdateAsync(ProviderCredentials credentials);
    Task DeleteAsync(int id);
}
