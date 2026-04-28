using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.UseCases.Reconciliation;
using Microsoft.AspNetCore.Mvc;

namespace CleanDddHexagonal.Api.Controllers;

[ApiController]
[Route("api/reconciliation")]
public sealed class ReconciliationController : ControllerBase
{
    private readonly RunReconciliationUseCase _runReconciliation;
    private readonly GetOpenIssuesUseCase _getOpenIssues;
    private readonly ResolveIssueUseCase _resolveIssue;

    public ReconciliationController(
        RunReconciliationUseCase runReconciliation,
        GetOpenIssuesUseCase getOpenIssues,
        ResolveIssueUseCase resolveIssue)
    {
        _runReconciliation = runReconciliation;
        _getOpenIssues = getOpenIssues;
        _resolveIssue = resolveIssue;
    }

    [HttpPost("run")]
    public async Task<ActionResult<IReadOnlyList<ReconciliationIssueDto>>> Run(RunReconciliationRequest request)
    {
        var result = await _runReconciliation.ExecuteAsync(request);

        if (!result.Success)
            return NotFound(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet("issues/open")]
    public async Task<ActionResult<IReadOnlyList<ReconciliationIssueDto>>> GetOpenIssues()
    {
        var issues = await _getOpenIssues.ExecuteAsync();
        return Ok(issues);
    }

    [HttpPatch("issues/{issueId:guid}/resolve")]
    public async Task<ActionResult<ReconciliationIssueDto>> Resolve(Guid issueId, ResolveIssueRequest request)
    {
        var result = await _resolveIssue.ExecuteAsync(issueId, request);

        if (!result.Success)
            return NotFound(new { error = result.Error });

        return Ok(result.Value);
    }
}
