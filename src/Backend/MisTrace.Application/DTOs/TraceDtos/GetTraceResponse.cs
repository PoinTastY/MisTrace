namespace MisTrace.Application.DTOs.TraceDtos;

public record GetTraceResponse
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public GetMilestoneResponse[]? Milestones { get; init; }
}
