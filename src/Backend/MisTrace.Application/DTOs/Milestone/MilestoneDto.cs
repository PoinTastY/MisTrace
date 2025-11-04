namespace MisTrace.Application.DTOs.Milestone;

public record MilestoneDto
{
    public required int MilestoneId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; } = null;
    public string? Notes { get; init; } = null;
    public bool? IsNotified { get; init; } = null;
}
