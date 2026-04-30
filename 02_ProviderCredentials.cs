namespace CleanDddHexagonal.Domain.Providers;

public class ProviderCredentials
{
    public int Id { get; private set; }
    public int TenantId { get; private set; }
    public CloudProvider Provider { get; private set; }
    public string ProviderAccountId { get; private set; }
    public string EncryptedApiKey { get; private set; }
    public string EncryptedSecretKey { get; private set; }
    public string SubscriptionId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastValidatedAt { get; private set; }

    public ProviderCredentials(
        int tenantId,
        CloudProvider provider,
        string providerAccountId,
        string encryptedApiKey,
        string encryptedSecretKey,
        string subscriptionId = null)
    {
        TenantId = tenantId;
        Provider = provider;
        ProviderAccountId = providerAccountId;
        EncryptedApiKey = encryptedApiKey;
        EncryptedSecretKey = encryptedSecretKey;
        SubscriptionId = subscriptionId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        LastValidatedAt = DateTime.UtcNow;
    }

    public void ValidateCredentials()
    {
        LastValidatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
