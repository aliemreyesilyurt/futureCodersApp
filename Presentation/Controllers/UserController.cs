using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Security.Claims;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserParameters userParameters)
        {
            var pagedResult = await _manager
                .AuthenticationService
                .GetAllUsersAsync(userParameters);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOneUserAsync(string id)
        {
            var user = await _manager
                .AuthenticationService
                .GetOneUserByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetOneUserByUserNameAsync()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _manager
                .AuthenticationService
                .GetOneUserByIdAsync(id);

            return Ok(user);
        }

        // Post-Forget Password
        [HttpPost("forget-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> FortgotPasswordAsync([FromQuery] UserForgetPasswordDto userDto)
        {
            var result = await _manager
                .AuthenticationService
                .NewRandomPasswordAsync(userDto);

            if (!result)
                return BadRequest("An error occurred while changing the password!");

            return Ok("Your new password was sent to your email!");
        }

        // Put-User
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneUserAsync([FromBody] UserDtoForUpdate userDto)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _manager
                .AuthenticationService
                .UpdateOneUserAsync(id, userDto);

            if (!result)
                return BadRequest("An error occurred while updating the user!");

            return NoContent();
        }

        // Patch-Current Password
        [HttpPatch("change-password")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneUserPasswordAsync([FromQuery] UserChangePasswordDto userDto)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _manager
                .AuthenticationService
                .UpdateOneUserPasswordAsync(id, userDto);

            if (!result)
                return BadRequest("An error occurred while updating the user password!");

            return Ok("Updated your password!");
        }

        // Patch-Email
        [Authorize]
        [HttpPatch("change-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneUserEmailAsync([FromQuery] UserChangeEmailDto userDto)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _manager
                .AuthenticationService
                .UpdateOneUserEmailAsync(id, userDto);

            if (!result)
                return BadRequest("An error occurred while changing the update!");

            return NoContent();
        }

        // Delete
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneUserAsync(string id)
        {
            var result = await _manager
                .AuthenticationService
                .DeleteOneUserAsync(id);

            if (!result)
                return BadRequest("An error occurred while deleting the user!");

            return NoContent();
        }

    }
}
