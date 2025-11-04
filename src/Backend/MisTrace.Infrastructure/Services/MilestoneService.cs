using MisTrace.Application.DTOs;
using MisTrace.Application.DTOs.Milestone;
using MisTrace.Application.DTOs.Milestone.Commands;
using MisTrace.Application.Interfaces;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Interfaces.Repos;

namespace MisTrace.Infrastructure.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IMilestoneRepo _milestoneRepo;
    public MilestoneService(IMilestoneRepo milestoneRepo)
    {
        _milestoneRepo = milestoneRepo;
    }

    public async Task<NewMilestoneResponse> CreateMilestone(CreateMilestoneCommand command, UserDto user)
    {
        Milestone newMilestone = new Milestone
        {
            Name = command.Name,
            CreatedById = user.SubjectGuid,
            OrganizationId = user.OrganizationId
        };

        return new NewMilestoneResponse((await _milestoneRepo.CreateAsync(newMilestone)).Id);
    }

    public async Task<IEnumerable<MilestoneDto>> GetMilestones(UserDto user)
    {
        return (await _milestoneRepo.GetByOrg(user.OrganizationId))
            .Select(m => new MilestoneDto
            {
                MilestoneId = m.Id,
                Name = m.Name
            });
    }
}
