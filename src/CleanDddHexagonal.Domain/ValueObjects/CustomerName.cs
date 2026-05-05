using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class CustomerName : IEquatable<CustomerName>
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

    // ✅ NUEVO: Igualdad por valor
    public override bool Equals(object? obj) => Equals(obj as CustomerName);

    public bool Equals(CustomerName? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    // ✅ NUEVO: Hash para colecciones
    public override int GetHashCode() => Value.GetHashCode();

    // ✅ NUEVO: Operadores de igualdad
    public static bool operator ==(CustomerName? left, CustomerName? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(CustomerName? left, CustomerName? right) => !(left == right);

    public override string ToString() => Value;
}
