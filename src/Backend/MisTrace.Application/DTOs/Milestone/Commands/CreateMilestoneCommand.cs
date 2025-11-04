namespace MisTrace.Application.DTOs.Milestone.Commands;

public record CreateMilestoneCommand
{
    public required string Name { get; init; }
    public string? Description { get; init; } = null;
    public required bool ConcludesTrace { get; init; }
}

public record NewMilestoneResponse(int Id);
