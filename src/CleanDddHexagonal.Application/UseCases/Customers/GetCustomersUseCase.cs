using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Application.UseCases;

namespace CleanDddHexagonal.Application.UseCases.Customers;

public sealed class GetCustomersUseCase
{
    private readonly ICustomerAccountRepository _repository;

    public GetCustomersUseCase(ICustomerAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CustomerDto>> ExecuteAsync()
    {
        var customers = await _repository.GetAllAsync();
        return customers.Select(ReconciliationMapper.ToDto).ToList();
    }
}
