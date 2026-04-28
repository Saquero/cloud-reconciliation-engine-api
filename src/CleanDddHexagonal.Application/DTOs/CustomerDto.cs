namespace CleanDddHexagonal.Application.DTOs;

public sealed record CustomerDto(
    Guid Id,
    string Name,
    DateTime CreatedAtUtc
);
