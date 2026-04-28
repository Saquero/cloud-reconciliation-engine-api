using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class ServiceSku
{
    public string Value { get; }

    private ServiceSku(string value)
    {
        Value = value;
    }

    public static ServiceSku Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidDomainValueException("Service SKU is required.");

        if (value.Trim().Length > 80)
            throw new InvalidDomainValueException("Service SKU cannot exceed 80 characters.");

        return new ServiceSku(value.Trim().ToUpperInvariant());
    }

    public override string ToString() => Value;
}
