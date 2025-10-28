using MisTrace.Domain.Entities;
using MisTrace.Domain.Entities.RelationDetails;

namespace MisTrace.Application.DTOs.TraceDtos;

public record NewTraceRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; } = null;
    public NewMilestoneRequest[]? Milestones { get; init; } = null;

    public Trace BuildTraceEntity(NewTraceRequest request, Guid subject, int orgId)
    {
        //TODO: ensure milestones provided exists before inserting trace
        Trace newTrace = new Trace
        {
            Name = this.Name,
            CreatedById = subject,
            OrganizationId = orgId,
        };

        if (this.Milestones != null)
            newTrace.TraceMilestones = this.Milestones.Select(m => new TraceMilestone { MilestoneId = m.MilestoneId }).ToList();

        return newTrace;
    }
}

