using MisTrace.Domain.Entities.RelationDetails;

namespace MisTrace.Application.DTOs.Trace.Commands;

public record NewTraceCommand
{
    public required string Name { get; init; }
    public string? Description { get; init; } = null;
    public int? CustomerId { get; init; }
    public int[]? Milestones { get; init; } = null;

    public Domain.Entities.Trace BuildTraceEntity(Guid subject, int orgId)
    {
        //TODO: ensure milestones provided exists before inserting trace
        Domain.Entities.Trace newTrace = new()
        {
            Name = this.Name,
            CreatedById = subject,
            OrganizationId = orgId,
            CustomerId = this.CustomerId
        };

        if (this.Milestones != null)
            newTrace.TraceMilestones = this.Milestones.Select(m => new TraceMilestone { MilestoneId = m }).ToList();

        return newTrace;
    }
}

public record NewTraceResponse(Guid Id);
