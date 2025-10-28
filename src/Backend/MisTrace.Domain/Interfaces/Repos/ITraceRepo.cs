using MisTrace.Domain.Entities;

namespace MisTrace.Domain.Interfaces.Repos;

public interface ITraceRepo
{
    Task<Trace> CreateAsync(Trace trace);
    Task UpdateAsync(Trace trace);
    IQueryable<Trace> GetTracesAsync(int top = 21);
}
