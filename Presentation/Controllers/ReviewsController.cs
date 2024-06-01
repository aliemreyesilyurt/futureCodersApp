using Entities.DataTransferObjects;
using Entities.RequestFeatures;
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
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ReviewsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllReviewsAsync([FromQuery] ReviewParameters reviewParameters)
        {
            var reviews = await _manager
                .ReviewService
                .GetAllReviewsAsync(reviewParameters, false);

            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneReviewAsync([FromRoute] int id)
        {
            var review = await _manager
                .ReviewService
                .GetOneReviewByIdAsync(id, false);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "User")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneReviewAsync([FromBody] ReviewDtoForInsertion reviewDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //check courseId
            var course = await _manager
                .CourseService
                .GetOneCourseByIdAsync(reviewDto.CourseId, false);

            //check userId
            var user = await _manager
                .AuthenticationService
                .GetOneUserByIdAsync(userId);

            if (course is null || user is null)
                return NotFound();

            var review = await _manager
                .ReviewService
                .CreateOneReviewAsync(reviewDto, userId);

            return StatusCode(201, review);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteOneReviewAsync([FromRoute(Name = "id")] int id)
        {
            var role = User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await _manager.ReviewService.GetOneReviewByIdAsync(id, false);

            if (review.UserId != userId && role == "User")
                return BadRequest();

            await _manager.ReviewService.DeleteOneReviewAsync(id, false);

            return NoContent();
        }

        // Put
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneReviewAsync([FromRoute(Name = "id")] int id, [FromBody] ReviewDtoForUpdate reviewDto)
        {
            if (reviewDto.Id != id || reviewDto.Id == 0)
                return BadRequest("The review id in the json must match the id of the review to be updated on the route. And the id field in json must be full!");

            //check courseId
            var course = await _manager
                .CourseService
                .GetOneCourseByIdAsync(reviewDto.CourseId, false);

            //check userId
            var user = await _manager
                .AuthenticationService
                .GetOneUserByIdAsync(reviewDto.UserId);

            if (course is null || user is null)
                return NotFound();

            await _manager.ReviewService.UpdateOneReviewAsync(id, reviewDto, false);

            return NoContent();
        }

        // Patch
        [HttpPatch("content/{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateOneReviewContentAsync([FromRoute(Name = "id")] int id, [FromBody] string content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var review = await _manager
                .ReviewService
                .GetOneReviewByIdAsync(id, false);

            if (review.UserId != userId)
                return BadRequest();

            await _manager.ReviewService.UpdateOneReviewContentAsync(id, content, false);

            return NoContent();
        }
    }
}
