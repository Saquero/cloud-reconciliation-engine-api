using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class ServiceSku : IEquatable<ServiceSku>
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

    // ✅ NUEVO: Igualdad por valor
    public override bool Equals(object? obj) => Equals(obj as ServiceSku);

    public bool Equals(ServiceSku? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    // ✅ NUEVO: Hash para colecciones
    public override int GetHashCode() => Value.GetHashCode();

    // ✅ NUEVO: Operadores de igualdad
    public static bool operator ==(ServiceSku? left, ServiceSku? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(ServiceSku? left, ServiceSku? right) => !(left == right);

    public override string ToString() => Value;
}
