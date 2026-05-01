namespace CleanDddHexagonal.Application.UseCases.CostAllocation;

using CleanDddHexagonal.Application.Common;
using CleanDddHexagonal.Application.DTOs.Providers;
using CleanDddHexagonal.Domain.Repositories;

public class CalculateCostAllocationUseCase
{
    private readonly ICostAllocationRepository _costRepository;
    private readonly ICustomerAccountRepository _customerRepository;

    public CalculateCostAllocationUseCase(
        ICostAllocationRepository costRepository,
        ICustomerAccountRepository customerRepository)
    {
        _costRepository = costRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<CostAllocationReportDto>> ExecuteAsync(
        int tenantId,
        DateTime startDate,
        DateTime endDate)
    {
        try
        {
            var records = await _costRepository.GetByTenantAsync(tenantId, startDate, endDate);

            if (!records.Any())
                return Result<CostAllocationReportDto>.Fail("No cost records found for this period");

            var totalActualCost = records.Sum(r => r.ActualCost);
            var totalBilledPrice = records.Sum(r => r.BilledPrice);
            var totalMargin = totalBilledPrice - totalActualCost;
            var marginPercentage = totalActualCost > 0 ? (totalMargin / totalActualCost * 100) : 0;

            var firstCustomerId = records.First().CustomerId;
            var customers = await _customerRepository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.Id.GetHashCode() == firstCustomerId);

            if (customer == null)
                return Result<CostAllocationReportDto>.Fail("Customer not found");

            return Result<CostAllocationReportDto>.Ok(new CostAllocationReportDto
            {
                CustomerId = customer.Id.GetHashCode(),
                CustomerName = customer.Name,
                TotalActualCost = totalActualCost,
                TotalBilledPrice = totalBilledPrice,
                TotalMargin = totalMargin,
                MarginPercentage = (decimal)marginPercentage
            });
        }
        catch (Exception ex)
        {
            return Result<CostAllocationReportDto>.Fail($"Calculation failed: {ex.Message}");
        }
    }
}
