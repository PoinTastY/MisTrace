using System;

namespace MisTrace.Application.DTOs.Trace;

public record GetTraceResponse
{
    public required string Name { get; init; }
}
