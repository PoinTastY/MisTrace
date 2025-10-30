using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisTrace.Application.DTOs.TraceDtos;
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
        public async Task<IActionResult> CreateTrace([FromBody] NewTraceRequest request)
        {
            string? subValue = User.FindFirstValue("sub");
            if (subValue is null)
                throw new UnauthorizedAccessException("Missing subject claim");

            Guid subject = Guid.Parse(subValue);

            string? orgValue = User.FindFirstValue("org");
            if (orgValue is null)
                throw new UnauthorizedAccessException("Missing org claim");

            int orgId = int.Parse(orgValue);

            if (orgId == 0)
                throw new InvalidOperationException("Invalid org claim");

            try
            {
                NewTraceResponse response = await _traceService.AddNewTrace(request, subject, orgId);

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

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> GetActiveTraces()
        {
            if (!int.TryParse(User.FindFirst("org")?.Value, out int orgId))
                return UnprocessableEntity();

            return Ok(await _traceService.GetTracesByOrg(orgId));
        }

        [Authorize]
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
