using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOneUserStepAsync([FromQuery] UserStepDtoForInsertion userStepDto)
        {
            var userStep = await _manager
                .UserStepService
                .CreateOneUserStepAsync(userStepDto);

            return StatusCode(201, userStep);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneUserStepAsync([FromQuery(Name = "id")] int stepId)
        {
            await _manager
                .UserStepService
                .DeleteOneUserStepAsync(stepId, false);

            return NoContent();
        }
    }
}
