using MisTrace.Domain.Entities;

namespace MisTrace.Domain.Interfaces.Repos;

public interface ITraceRepo
{
    Task<Trace> CreateAsync(Trace trace);
    Task UpdateAsync(Trace trace);
    IQueryable<Trace> GetTracesByOrgAsync(int orgId, int page = 1, int top = 21);
    Task<Trace> GetByGlobalId(Guid guid);
}
