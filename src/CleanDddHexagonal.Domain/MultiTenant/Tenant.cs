namespace CleanDddHexagonal.Domain.MultiTenant;

public class Tenant
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Tenant(string name)
    {
        Name = name;
        Slug = name.ToLower().Replace(" ", "-");
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
