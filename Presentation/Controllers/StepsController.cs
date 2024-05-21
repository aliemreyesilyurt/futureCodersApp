using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/steps")]
    public class StepsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private const string MediaFolderPath = "Media/Steps/";
        public StepsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStepsAsync([FromQuery] StepParameters stepParameters)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var _user = await _manager
                .AuthenticationService
                .GetOneUserByIdAsync(id);

            var steps = await _manager
                .StepService
                .GetAllStepsAsync(stepParameters, false);

            return Ok(steps);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneStepAsync([FromRoute] int id)
        {
            var step = await _manager
                .StepService
                .GetOneStepByIdAsync(id, false);

            if (step == null)
                return NotFound();

            return Ok(step);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneStepAsync([FromForm] StepDtoForInsertion stepDto, IFormFile file)
        {
            var result = await CheckAndPathingFile(stepDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid video file.");

            //check courseId
            var course = await _manager
                .CourseService
                .GetOneCourseByIdAsync(stepDto.CourseId, false);

            if (course is null)
                return NotFound();

            var step = await _manager
               .StepService
               .CreateOneStepAsync(result.stepDto);

            return StatusCode(201, step);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneStepAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.StepService.DeleteOneStepAsync(id, false);

            return NoContent();
        }

        // Put
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneStepAsync([FromRoute(Name = "id")] int id, [FromForm] StepDtoForUpdate stepDto, IFormFile file)
        {
            if (stepDto.Id != id || stepDto.Id == 0)
                return BadRequest("The step id in the json must match the id of the step to be updated on the route. And the id field in json must be full!");

            var result = await CheckAndPathingFile(stepDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid video file.");

            //check courseId
            var course = await _manager
                .CourseService
                .GetOneCourseByIdAsync(stepDto.CourseId, false);

            if (course is null)
                return NotFound();

            await _manager.StepService.UpdateOneStepAsync(id, result.stepDto, false);

            return NoContent();
        }

        // Patch-Video
        [HttpPatch("video/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneBlogVideoAsync([FromRoute(Name = "id")] int id, IFormFile file)
        {
            //check entity
            var entity = await _manager
                .StepService
                .GetOneStepByIdAsync(id, false);

            if (entity == null)
                return NotFound();

            //check file
            if (file is null || file.Length == 0 || !IsValidVideoFile(file))
                return BadRequest("Please provide a valid video file.");

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _manager.StepService.UpdateOneStepVideoAsync(id, uniqueFileName, false);

            return NoContent();
        }

        // Patch-Status
        [HttpPatch("status/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneBlogStatusAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.StepService.UpdateOneStepStatusAsync(id, false);

            return NoContent();
        }

        // Check File
        private async Task<(T stepDto, bool isValidate)> CheckAndPathingFile<T>(T stepDto, IFormFile file)
            where T : StepDtoForManipulation
        {
            var isValidate = true;

            if (file is null || file.Length == 0 || !IsValidVideoFile(file))
                isValidate = false;

            if (isValidate)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                stepDto.VideoPath = uniqueFileName;
            }

            return (stepDto, isValidate);
        }

        // Image Validation
        private bool IsValidVideoFile(IFormFile file)
        {
            if (file.ContentType.ToLower() != "video/mp4" &&
                file.ContentType.ToLower() != "video/mpeg" &&
                file.ContentType.ToLower() != "video/avi")
            {
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension == ".mp4" || extension == ".mpeg" || extension == ".avi";
        }


    }
}
