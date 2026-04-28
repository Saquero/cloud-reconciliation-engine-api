using CleanDddHexagonal.Domain.ValueObjects;

namespace CleanDddHexagonal.Domain.Entities;

public sealed class CustomerAccount
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }

    private CustomerAccount() { }

    private CustomerAccount(Guid id, CustomerName name, DateTime createdAtUtc)
    {
        Id = id;
        Name = name.Value;
        CreatedAtUtc = createdAtUtc;
    }

    public static CustomerAccount Create(CustomerName name, DateTime createdAtUtc)
    {
        return new CustomerAccount(Guid.NewGuid(), name, createdAtUtc);
    }
}
