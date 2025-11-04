using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisTrace.ApiService.Extensions;
using MisTrace.Application.DTOs;
using MisTrace.Application.DTOs.Trace;
using MisTrace.Application.DTOs.Trace.Commands;
using MisTrace.Application.Interfaces;

namespace MisTrace.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraceController : ControllerBase
    {
        private readonly ITraceService _traceService;
        public TraceController(ITraceService traceService)
        {
            _traceService = traceService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTrace([FromBody] NewTraceCommand request)
        {
            UserDto user = ClaimsExtensions.BuildUserFromClaims(User);

            try
            {
                NewTraceResponse response = await _traceService.AddNewTrace(request, user.SubjectGuid, user.OrganizationId);

                return CreatedAtAction(
                    nameof(GetTraceById),
                    new { traceId = response.Id },
                    response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetActiveTraces()
        {
            UserDto user = ClaimsExtensions.BuildUserFromClaims(User);

            return Ok(await _traceService.GetTracesByOrg(user.OrganizationId));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTraceById([FromQuery] Guid traceId)
        {
            try
            {
                TraceDto response = await _traceService.GetByGlobalId(traceId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
