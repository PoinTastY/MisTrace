using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisTrace.Application.DTOs.Notify;
using MisTrace.Application.Interfaces;

namespace MisTrace.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly ITwillioService _twilioService;
        public NotifyController(ITwillioService twillioService)
        {
            _twilioService = twillioService;
        }

        [Authorize]
        [HttpPost("Whatsapp")]
        public IActionResult SendWhatsapMessage([FromBody] WhatsAppMessageRequest messageRequest)
        {
            try
            {
                _twilioService.SendWhatsAppMessage(messageRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
