using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class MoneyAmount : IEquatable<MoneyAmount>
{
    public decimal Value { get; }
    public string Currency { get; }

    private MoneyAmount(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }

    public static MoneyAmount Create(decimal value, string currency)
    {
        if (value < 0)
            throw new InvalidDomainValueException("Money amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new InvalidDomainValueException("Currency is required.");

        if (currency.Trim().Length != 3)
            throw new InvalidDomainValueException("Currency must use ISO 4217 format, for example EUR or USD.");

        return new MoneyAmount(decimal.Round(value, 2), currency.Trim().ToUpperInvariant());
    }

    // ✅ NUEVO: Igualdad por valor
    public override bool Equals(object? obj) => Equals(obj as MoneyAmount);

    public bool Equals(MoneyAmount? other)
    {
        if (other is null) return false;
        return Value == other.Value && Currency == other.Currency;
    }

    // ✅ NUEVO: Hash para colecciones
    public override int GetHashCode() => HashCode.Combine(Value, Currency);

    // ✅ NUEVO: Operadores de igualdad
    public static bool operator ==(MoneyAmount? left, MoneyAmount? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(MoneyAmount? left, MoneyAmount? right) => !(left == right);

    public override string ToString() => $"{Value:0.00} {Currency}";
}
