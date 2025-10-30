using System;
using MisTrace.Domain.Entities;
using MisTrace.Domain.Interfaces.Repos;

namespace MisTrace.Infrastructure.Services;

public class MilestoneRepo : IMilestoneRepo
{
    public Task<IEnumerable<Milestone>> GetByIdsAsync(int[] ids)
    {
        throw new NotImplementedException();
    }
}
