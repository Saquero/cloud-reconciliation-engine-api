using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Application.UseCases;
using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Domain.ValueObjects;

namespace CleanDddHexagonal.Application.UseCases.UsageRecords;

public sealed class ImportExternalUsageSnapshotUseCase
{
    private readonly IUsageRecordRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ImportExternalUsageSnapshotUseCase(IUsageRecordRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<UsageRecordDto> ExecuteAsync(ImportExternalUsageSnapshotRequest request)
    {
        var snapshot = ExternalUsageSnapshot.Create(
            request.CustomerId,
            request.Provider,
            ServiceSku.Create(request.ServiceSku),
            request.SeatCount,
            MoneyAmount.Create(request.MonthlyCost, request.Currency),
            _dateTimeProvider.UtcNow);

        await _repository.AddExternalAsync(snapshot);
        await _repository.SaveChangesAsync();

        return ReconciliationMapper.ToDto(snapshot);
    }
}
