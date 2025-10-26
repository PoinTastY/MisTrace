using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisTrace.Application.DTOs.Trace;
using MisTrace.Application.Interfaces;

namespace MisTrace.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceController : ControllerBase
    {
        private readonly ITraceService _traceService;
        public TraceController(ITraceService traceService)
        {
            _traceService = traceService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTrace([FromBody] NewTraceRequest request)
        {
            try
            {
                NewTraceResponse response = await _traceService.AddNewTrace(request);

                return CreatedAtAction(
                    nameof(GetTraceById),
                    new { id = response.Id },
                    response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetActiveTraces()
        {
            if (!int.TryParse(User.FindFirst("org")?.Value, out int orgId))
                return UnprocessableEntity();

            return Ok(await _traceService.GetTracesByOrg(orgId));
        }

        [HttpGet]
        public async Task<IActionResult> GetTraceById([FromQuery] int traceId)
        {
            try
            {
                GetTraceResponse response = await _traceService.GetById(traceId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
