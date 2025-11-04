using System.Diagnostics.CodeAnalysis;
using MisTrace.Application.DTOs.Milestone;

namespace MisTrace.Application.DTOs.Trace;

public record TraceDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public int? CustomerId { get; init; }
    public string? CustomerName { get; init; }
    public string? Description { get; init; }
    public required bool IsComplete { get; init; }
    public MilestoneDto[] Milestones { get; init; } = [];
    public TraceDto() { }

    [SetsRequiredMembers]
    public TraceDto(Domain.Entities.Trace trace)
    {
        Id = trace.Id;
        Name = trace.Name;
        CustomerId = trace.CustomerId;
        if (trace.Customer is not null)
            CustomerName = trace.Customer.Name;

        Description = trace.Description;

        if (trace.TraceMilestones.Count == 0)
            IsComplete = false;
        else
            IsComplete = trace.TraceMilestones.Any(m => m.Milestone.ConcludesService);

        Milestones = [..  trace.TraceMilestones.Select(tm =>
            new MilestoneDto
            {
                Name = tm.Milestone.Name,
                MilestoneId = tm.MilestoneId
            })];
    }
}
