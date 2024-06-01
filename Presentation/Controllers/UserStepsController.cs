using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/user-steps")]
    public class UserStepsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserStepsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUserStepsAsync([FromQuery] string userId)
        {
            var userSteps = await _manager
                .UserStepService
                .GetAllUserStepsAsync(userId, false);

            return Ok(userSteps);
        }

        // Create
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateOneUserStepAsync([FromQuery] int stepId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userStep = await _manager
                .UserStepService
                .CreateOneUserStepAsync(userId, stepId);

            return StatusCode(201, userStep);
        }
    }
}
