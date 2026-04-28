using CleanDddHexagonal.Application.Common;
using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Application.UseCases;
using CleanDddHexagonal.Domain.Repositories;
using CleanDddHexagonal.Domain.Services;

namespace CleanDddHexagonal.Application.UseCases.Reconciliation;

public sealed class RunReconciliationUseCase
{
    private readonly ICustomerAccountRepository _customerRepository;
    private readonly IUsageRecordRepository _usageRepository;
    private readonly IReconciliationIssueRepository _issueRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public RunReconciliationUseCase(
        ICustomerAccountRepository customerRepository,
        IUsageRecordRepository usageRepository,
        IReconciliationIssueRepository issueRepository,
        IDateTimeProvider dateTimeProvider,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _customerRepository = customerRepository;
        _usageRepository = usageRepository;
        _issueRepository = issueRepository;
        _dateTimeProvider = dateTimeProvider;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<Result<IReadOnlyList<ReconciliationIssueDto>>> ExecuteAsync(RunReconciliationRequest request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
            return Result<IReadOnlyList<ReconciliationIssueDto>>.Fail("Customer not found.");

        var internalRecords = await _usageRepository.GetInternalByCustomerAsync(request.CustomerId);
        var externalSnapshots = await _usageRepository.GetExternalByCustomerAsync(request.CustomerId);

        var service = new UsageReconciliationService();
        var issues = service.Compare(internalRecords, externalSnapshots, _dateTimeProvider.UtcNow);

        await _issueRepository.AddRangeAsync(issues);
        await _issueRepository.SaveChangesAsync();

        var events = issues.SelectMany(issue => issue.DomainEvents).ToList();
        await _domainEventDispatcher.DispatchAsync(events);

        foreach (var issue in issues)
            issue.ClearDomainEvents();

        return Result<IReadOnlyList<ReconciliationIssueDto>>.Ok(
            issues.Select(ReconciliationMapper.ToDto).ToList());
    }
}
