using Microsoft.EntityFrameworkCore;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Interfaces.Repos;
using MisTrace.Infrastructure.Data;

namespace MisTrace.Infrastructure.Repos;

public class MilestoneRepo : IMilestoneRepo
{
    private readonly MisTraceDbContext _dbContext;
    public MilestoneRepo(MisTraceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Milestone> CreateAsync(Milestone milestone)
    {
        await _dbContext.AddAsync(milestone);

        await _dbContext.SaveChangesAsync();

        return milestone;
    }

    public async Task<IEnumerable<Milestone>> GetByIdsAsync(int[] ids, int orgId)
    {
        if (orgId <= 0)
            throw new ArgumentException("Organization ID must be greater than zero.", nameof(orgId));

        if (ids == null || ids.Length == 0)
            throw new ArgumentException("At least one milestone ID must be provided.", nameof(ids));

        ids = ids.Distinct().ToArray();

        var milestones = await _dbContext.Milestones
            .AsNoTracking()
            .Where(m => ids.Contains(m.Id) && m.OrganizationId == orgId)
            .ToListAsync();

        if (milestones.Count != ids.Length)
        {
            var foundIds = milestones.Select(m => m.Id).ToHashSet();
            var missing = ids.Except(foundIds).ToArray();

            throw new InvalidOperationException(
                $"One or more milestone IDs are invalid or do not belong to organization {orgId}: {string.Join(", ", missing)}");
        }

        return milestones;
    }

    public async Task<IEnumerable<Milestone>> GetByOrg(int orgId, int page = 1, int top = 21)
    {
        if (orgId <= 0)
            throw new ArgumentException("Organization ID must be greater than zero.", nameof(orgId));

        if (page <= 0)
            throw new ArgumentException("Page number must be greater than zero.", nameof(page));

        if (top <= 0 || top > 100)
            throw new ArgumentException("Top must be between 1 and 100 to prevent large queries.", nameof(top));

        var query = _dbContext.Milestones
            .AsNoTracking()
            .Where(m => m.OrganizationId == orgId)
            .OrderBy(m => m.Id);

        int skip = (page - 1) * top;

        var milestones = await query
            .Skip(skip)
            .Take(top)
            .ToListAsync();

        if (milestones.Count == 0)
            return [];

        return milestones;
    }
}
