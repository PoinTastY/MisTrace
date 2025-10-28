using MisTrace.Domain.Entities;
using MisTrace.Domain.Interfaces.Repos;
using MisTrace.Infrastructure.Data;

namespace MisTrace.Infrastructure.Repos;

public class TraceRepo : ITraceRepo
{
    private readonly MisTraceDbContext _dbContext;
    public TraceRepo(MisTraceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Trace> CreateAsync(Trace trace)
    {
        //TODO: Validate before creating

        await _dbContext.Traces.AddAsync(trace);

        await _dbContext.SaveChangesAsync();

        return trace;
    }

    public async Task UpdateAsync(Trace trace)
    {
        //TODO: Validate entity
        _dbContext.Update(trace);//this maybe gives error lol

        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<Trace> GetTracesAsync(int top = 21)
    {
        return _dbContext.Traces.Take(top).AsQueryable();
    }
}
