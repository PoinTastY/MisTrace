using System;

namespace MisTrace.Application.DTOs.TraceDtos;

public record GetMilestoneResponse
{
    public required string Name { get; init; }
    public string? Details { get; init; }
    public bool Notified { get; init; }
}
