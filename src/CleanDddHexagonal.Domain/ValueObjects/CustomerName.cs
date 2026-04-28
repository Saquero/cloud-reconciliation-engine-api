using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class CustomerName
{
    public string Value { get; }

    private CustomerName(string value)
    {
        Value = value;
    }

    public static CustomerName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidDomainValueException("Customer name is required.");

        if (value.Trim().Length > 120)
            throw new InvalidDomainValueException("Customer name cannot exceed 120 characters.");

        return new CustomerName(value.Trim());
    }

    public override string ToString() => Value;
}
