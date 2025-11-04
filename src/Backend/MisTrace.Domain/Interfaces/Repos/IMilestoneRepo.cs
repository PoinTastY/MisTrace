using MisTrace.Domain.Entities;

namespace MisTrace.Domain.Interfaces.Repos;

public interface IMilestoneRepo
{
    /// <summary>
    /// Will throw if any id doesnt exist
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IEnumerable<Milestone>> GetByIdsAsync(int[] ids, int orgId);

    Task<IEnumerable<Milestone>> GetByOrg(int orgId, int page = 1, int top = 21);
    Task<Milestone> CreateAsync(Milestone milestone);
}
