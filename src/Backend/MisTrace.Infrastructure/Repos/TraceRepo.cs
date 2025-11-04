using Microsoft.EntityFrameworkCore;
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

    public async Task<Trace> GetByGlobalId(Guid guid)
    {
        return await _dbContext.Traces.SingleOrDefaultAsync(t => t.GlobalIdentifier == guid)
            ?? throw new KeyNotFoundException("No trace was found");
    }


    public IQueryable<Trace> GetTracesByOrgAsync(int orgId, int page = 1, int top = 21)
    {
        if (orgId <= 0)
            throw new ArgumentException("Organization ID must be greater than zero.", nameof(orgId));

        if (page <= 0)
            throw new ArgumentException("Page number must be greater than zero.", nameof(page));

        if (top <= 0 || top > 100)
            throw new ArgumentException("Top must be between 1 and 100 to prevent excessive load.", nameof(top));

        int skip = (page - 1) * top;

        return _dbContext.Traces
            .Include(t => t.Customer)
            .Include(t => t.TraceMilestones)
                .ThenInclude(tm => tm.Milestone)
            .AsNoTracking()
            .Where(t => t.OrganizationId == orgId)
            .OrderByDescending(t => t.Id) // or by CreatedDate if you have one
            .Skip(skip)
            .Take(top);
    }
}
