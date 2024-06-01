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
    [Route("api/user-courses")]
    public class UserCoursesController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserCoursesController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getAllUserCoursesAsync([FromQuery] string userId)
        {
            var userCourses = await _manager
                .UserCourseService
                .GetAllUserCoursesAsync(userId, false);

            return Ok(userCourses);
        }

        // Create
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateOneUserStepAsync([FromQuery] int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCourse = await _manager
                .UserCourseService
                .CreateOneUserCourseAsync(userId, courseId);

            return StatusCode(201, userCourse);
        }
    }
}
