using CleanDddHexagonal.Application.Ports;

namespace CleanDddHexagonal.Infrastructure.System;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
