using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.UseCases;
using CleanDddHexagonal.Domain.Repositories;

namespace CleanDddHexagonal.Application.UseCases.Reconciliation;

public sealed class GetOpenIssuesUseCase
{
    private readonly IReconciliationIssueRepository _repository;

    public GetOpenIssuesUseCase(IReconciliationIssueRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReconciliationIssueDto>> ExecuteAsync()
    {
        var issues = await _repository.GetOpenAsync();
        return issues.Select(ReconciliationMapper.ToDto).ToList();
    }
}
