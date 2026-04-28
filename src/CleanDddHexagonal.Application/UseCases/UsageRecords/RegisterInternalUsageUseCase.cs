using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Application.UseCases;
using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Domain.ValueObjects;

namespace CleanDddHexagonal.Application.UseCases.UsageRecords;

public sealed class RegisterInternalUsageUseCase
{
    private readonly IUsageRecordRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterInternalUsageUseCase(IUsageRecordRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<UsageRecordDto> ExecuteAsync(RegisterInternalUsageRequest request)
    {
        var record = InternalUsageRecord.Create(
            request.CustomerId,
            request.Provider,
            ServiceSku.Create(request.ServiceSku),
            request.SeatCount,
            MoneyAmount.Create(request.MonthlyCost, request.Currency),
            _dateTimeProvider.UtcNow);

        await _repository.AddInternalAsync(record);
        await _repository.SaveChangesAsync();

        return ReconciliationMapper.ToDto(record);
    }
}
