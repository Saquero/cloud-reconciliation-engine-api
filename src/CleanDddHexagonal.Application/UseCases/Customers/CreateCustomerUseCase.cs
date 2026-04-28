using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Domain.Entities;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Domain.ValueObjects;
using CleanDddHexagonal.Application.UseCases;

namespace CleanDddHexagonal.Application.UseCases.Customers;

public sealed class CreateCustomerUseCase
{
    private readonly ICustomerAccountRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCustomerUseCase(ICustomerAccountRepository repository, IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<CustomerDto> ExecuteAsync(CreateCustomerRequest request)
    {
        var name = CustomerName.Create(request.Name);
        var customer = CustomerAccount.Create(name, _dateTimeProvider.UtcNow);

        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();

        return ReconciliationMapper.ToDto(customer);
    }
}
