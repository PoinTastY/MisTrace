using System;
using MisTrace.Application.DTOs.Milestone;

namespace MisTrace.Application.Interfaces;

public interface IMilestoneService
{
    public Task<IEnumerable<MilestoneDto>> GetMilestones(int orgId);
}
