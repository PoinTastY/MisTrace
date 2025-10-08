using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MisTrace.ApiService.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {

            return Ok();
        }
    }
}
