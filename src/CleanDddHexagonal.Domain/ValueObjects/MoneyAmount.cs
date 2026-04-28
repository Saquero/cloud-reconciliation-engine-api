using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Domain.ValueObjects;

public sealed class MoneyAmount
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

    public override string ToString() => $"{Value:0.00} {Currency}";
}
