using MisTrace.Application.DTOs.Trace;
using MisTrace.Application.DTOs.Trace.Commands;

namespace MisTrace.Application.Interfaces;

public interface ITraceService
{
    Task<NewTraceResponse> AddNewTrace(NewTraceCommand request, Guid subject, int orgId);
    Task<TraceDto> GetByGlobalId(Guid id);
    Task<IEnumerable<TraceDto>> GetTracesByOrg(int orgId);
}
