using CleanDddHexagonal.Application.DTOs;
using CleanDddHexagonal.Application.UseCases.UsageRecords;
using Microsoft.AspNetCore.Mvc;

namespace CleanDddHexagonal.Api.Controllers;

[ApiController]
[Route("api/usage")]
public sealed class UsageRecordsController : ControllerBase
{
    private readonly RegisterInternalUsageUseCase _registerInternalUsage;
    private readonly ImportExternalUsageSnapshotUseCase _importExternalUsageSnapshot;

    public UsageRecordsController(
        RegisterInternalUsageUseCase registerInternalUsage,
        ImportExternalUsageSnapshotUseCase importExternalUsageSnapshot)
    {
        _registerInternalUsage = registerInternalUsage;
        _importExternalUsageSnapshot = importExternalUsageSnapshot;
    }

    [HttpPost("internal")]
    public async Task<ActionResult<UsageRecordDto>> RegisterInternal(RegisterInternalUsageRequest request)
    {
        var record = await _registerInternalUsage.ExecuteAsync(request);
        return Created($"/api/usage/internal/{record.Id}", record);
    }

    [HttpPost("external-snapshots")]
    public async Task<ActionResult<UsageRecordDto>> ImportExternalSnapshot(ImportExternalUsageSnapshotRequest request)
    {
        var snapshot = await _importExternalUsageSnapshot.ExecuteAsync(request);
        return Created($"/api/usage/external-snapshots/{snapshot.Id}", snapshot);
    }
}
