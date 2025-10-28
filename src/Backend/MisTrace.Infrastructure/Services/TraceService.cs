using MisTrace.Application.DTOs.TraceDtos;
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
    public async Task<NewTraceResponse> AddNewTrace(NewTraceRequest request, Guid subject, int orgId)
    {
        List<Milestone> milestones = [];
        //Ensure upcomming milestones does exists, else throw invalid
        if (request.Milestones != null && request.Milestones.Count() > 0)
            milestones = (await _milestoneRepo.GetByIdsAsync(request.Milestones.Select(m => m.MilestoneId).ToArray())).ToList();

        Trace newTrace = await _traceRepo.CreateAsync(request.BuildTraceEntity(request, subject, orgId));

        return new NewTraceResponse
        {
            Id = newTrace.Id
        };
    }
    public Task<GetTraceResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<GetTraceResponse>> GetTracesByOrg(int orgId)
    {
        throw new NotImplementedException();
    }
}
