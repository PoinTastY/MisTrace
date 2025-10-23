using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MisTrace.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public IActionResult CreateTrace([FromBody] LoginRequest loginRequest)
        {
            return StatusCode(501, "This endpoint is not implemented yet lol");
        }
    }
}
