using CleanDddHexagonal.Domain.ValueObjects;
using CleanDddHexagonal.Domain.Exceptions;

namespace CleanDddHexagonal.Tests;

public class ValueObjectTests
{
    [Fact]
    public void CustomerName_ShouldTrimValue()
    {
        var name = CustomerName.Create("  Contoso Ltd  ");

        Assert.Equal("Contoso Ltd", name.Value);
    }

    [Fact]
    public void ServiceSku_ShouldNormalizeToUppercase()
    {
        var sku = ServiceSku.Create(" ms-365-business-premium ");

        Assert.Equal("MS-365-BUSINESS-PREMIUM", sku.Value);
    }

    [Fact]
    public void MoneyAmount_ShouldRejectNegativeValues()
    {
        Assert.Throws<InvalidDomainValueException>(() => MoneyAmount.Create(-1, "EUR"));
    }
}
