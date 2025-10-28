using System;

namespace MisTrace.Application.DTOs;

public class NewMilestoneRequest
{
    public int MilestoneId { get; set; }
    public string? Comments { get; set; }
}
