using CleanDddHexagonal.Application.Common;
using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.Ports;
using CleanDddHexagonal.Application.UseCases;
using CleanDddHexagonal.Domain.Repositories;

namespace CleanDddHexagonal.Application.UseCases.Reconciliation;

public sealed class ResolveIssueUseCase
{
    private readonly IReconciliationIssueRepository _repository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public ResolveIssueUseCase(
        IReconciliationIssueRepository repository,
        IDateTimeProvider dateTimeProvider,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _repository = repository;
        _dateTimeProvider = dateTimeProvider;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<Result<ReconciliationIssueDto>> ExecuteAsync(Guid issueId, ResolveIssueRequest request)
    {
        var issue = await _repository.GetByIdAsync(issueId);

        if (issue is null)
            return Result<ReconciliationIssueDto>.Fail("Reconciliation issue not found.");

        issue.Resolve(request.ResolutionNote, _dateTimeProvider.UtcNow);

        var events = issue.DomainEvents.ToList();

        await _repository.SaveChangesAsync();
        await _domainEventDispatcher.DispatchAsync(events);

        issue.ClearDomainEvents();

        return Result<ReconciliationIssueDto>.Ok(ReconciliationMapper.ToDto(issue));
    }
}
