using System;
using MisTrace.Application.DTOs;
using MisTrace.Application.DTOs.Milestone;
using MisTrace.Application.DTOs.Milestone.Commands;

namespace MisTrace.Application.Interfaces;

public interface IMilestoneService
{
    public Task<IEnumerable<MilestoneDto>> GetMilestones(UserDto user);
    public Task<NewMilestoneResponse> CreateMilestone(CreateMilestoneCommand command, UserDto user);
}
