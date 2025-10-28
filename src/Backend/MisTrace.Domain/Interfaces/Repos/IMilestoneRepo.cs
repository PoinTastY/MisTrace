using System;
using MisTrace.Domain.Entities;

namespace MisTrace.Domain.Interfaces.Repos;

public interface IMilestoneRepo
{
    /// <summary>
    /// Will throw if any id doesnt exist
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IEnumerable<Milestone>> GetByIdsAsync(int[] ids);
}
