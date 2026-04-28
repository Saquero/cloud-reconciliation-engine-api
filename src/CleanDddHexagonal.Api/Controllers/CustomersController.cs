using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CleanDddHexagonal.Api.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController : ControllerBase
{
    private readonly CreateCustomerUseCase _createCustomer;
    private readonly GetCustomersUseCase _getCustomers;

    public CustomersController(CreateCustomerUseCase createCustomer, GetCustomersUseCase getCustomers)
    {
        _createCustomer = createCustomer;
        _getCustomers = getCustomers;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll()
    {
        var customers = await _getCustomers.ExecuteAsync();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create(CreateCustomerRequest request)
    {
        var customer = await _createCustomer.ExecuteAsync(request);
        return Created($"/api/customers/{customer.Id}", customer);
    }
}
