using CleanDddHexagonal.Domain.Enums;

namespace CleanDddHexagonal.Application.DTOs;

public sealed record UsageRecordDto(
    Guid Id,
    Guid CustomerId,
    CloudProvider Provider,
    string ServiceSku,
    int SeatCount,
    decimal MonthlyCost,
    string Currency,
    DateTime TimestampUtc
);
