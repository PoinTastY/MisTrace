using MisTrace.Application.DTOs.TraceDtos;

namespace MisTrace.Application.Interfaces;

public interface ITraceService
{
    Task<NewTraceResponse> AddNewTrace(NewTraceRequest request, Guid subject, int orgId);
    Task<GetTraceResponse> GetById(int id);
    Task<IEnumerable<GetTraceResponse>> GetTracesByOrg(int orgId);
}
