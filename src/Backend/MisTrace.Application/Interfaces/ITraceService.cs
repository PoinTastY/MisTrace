using MisTrace.Application.DTOs.Trace;

namespace MisTrace.Application.Interfaces;

public interface ITraceService
{
    Task<NewTraceResponse> AddNewTrace(NewTraceRequest request);
    Task<GetTraceResponse> GetById(int id);

    Task<IEnumerable<GetTraceResponse>> GetTracesByOrg(int orgId);
}
