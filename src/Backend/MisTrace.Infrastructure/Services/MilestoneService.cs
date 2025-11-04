using MisTrace.Application.DTOs.Milestone;
using MisTrace.Application.Interfaces;
using MisTrace.Domain.Interfaces.Repos;

namespace MisTrace.Infrastructure.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IMilestoneRepo _milestoneRepo;
    public MilestoneService(IMilestoneRepo milestoneRepo)
    {
        _milestoneRepo = milestoneRepo;
    }
    public async Task<IEnumerable<MilestoneDto>> GetMilestones(int orgId)
    {
        return (await _milestoneRepo.GetByOrg(orgId))
            .Select(m => new MilestoneDto
            {
                MilestoneId = m.Id,
                Name = m.Name
            });
    }
}
