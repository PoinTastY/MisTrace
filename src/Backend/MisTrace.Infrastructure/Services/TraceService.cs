using Microsoft.EntityFrameworkCore;
using MisTrace.Application.DTOs.Trace;
using MisTrace.Application.DTOs.Trace.Commands;
using MisTrace.Application.Interfaces;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Interfaces.Repos;

namespace MisTrace.Infrastructure.Services;

public class TraceService : ITraceService
{
    private readonly ITraceRepo _traceRepo;
    private readonly IMilestoneRepo _milestoneRepo;
    public TraceService(ITraceRepo traceRepo, IMilestoneRepo milestoneRepo)
    {
        _traceRepo = traceRepo;
        _milestoneRepo = milestoneRepo;
    }
    public async Task<NewTraceResponse> AddNewTrace(NewTraceCommand request, Guid subject, int orgId)
    {
        //Ensure upcomming milestones does exists, else throw invalid
        if (request.Milestones != null && request.Milestones.Count() > 0)
            if ((await _milestoneRepo.GetByIdsAsync(request.Milestones.Select(m => m).ToArray(), orgId)).Count() != request.Milestones.Count())
                throw new InvalidOperationException("Provided milestones seems to not exist");

        Trace newTrace = await _traceRepo.CreateAsync(request.BuildTraceEntity(subject, orgId));

        return new NewTraceResponse(newTrace.GlobalIdentifier);
    }

    public async Task<TraceDto> GetByGlobalId(Guid id)
    {
        Trace trace = await _traceRepo.GetByGlobalId(id);

        return new TraceDto(trace);
    }

    public async Task<IEnumerable<TraceDto>> GetTracesByOrg(int orgId)
    {
        return await _traceRepo.GetTracesByOrgAsync(orgId)
            .Select(t => new TraceDto
            {
                Id = t.Id,
                Name = t.Name,
                CustomerId = t.CustomerId,
                CustomerName = t.Customer != null ? t.Customer.Name : null,
                IsComplete = t.TraceMilestones.Any(m => m.Milestone.ConcludesService)
            })
            .ToListAsync();
    }
}
