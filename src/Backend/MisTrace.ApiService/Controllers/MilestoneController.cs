using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MisTrace.ApiService.Extensions;
using MisTrace.Application.DTOs;
using MisTrace.Application.DTOs.Milestone.Commands;
using MisTrace.Application.Interfaces;

namespace MisTrace.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;
        public MilestoneController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMilestones()
        {
            UserDto user = ClaimsExtensions.BuildUserFromClaims(User);

            return Ok(await _milestoneService.GetMilestones(user));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMilestone([FromBody] CreateMilestoneCommand request)
        {
            UserDto user = ClaimsExtensions.BuildUserFromClaims(User);

            return Ok(await _milestoneService.CreateMilestone(request, user));
        }
    }
}
