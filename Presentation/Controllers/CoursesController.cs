using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private const string MediaFolderPath = "Media/Courses/";

        public CoursesController(IServiceManager manager)
        {
            _manager = manager;
        }


        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCoursesAsync([FromQuery] CourseParameters courseParameters)
        {
            var pagedResult = await _manager
                .CourseService
                .GetAllCoursesAsync(courseParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.courses);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneCourseByIdAsync([FromRoute(Name = "id")] int id)
        {
            var course = await _manager
            .CourseService
            .GetOneCourseByIdAsync(id, false);


            if (course == null)
                return NotFound(); //400

            return Ok(course);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneCourseAsync([FromForm] CourseDtoForInsertion courseDto, IFormFile file)
        {
            var result = await CheckAndPathingFile(courseDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid image file.");

            var course = await _manager
                .CourseService
                .CreateOneCourseAsync(courseDto);

            return StatusCode(201, course);
        }

        // Put
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneCourseAsync([FromRoute(Name = "id")] int id, [FromForm] CourseDtoForUpdate courseDto, IFormFile file)
        {

            foreach (var rankId in courseDto.RankIds)
            {
                if (rankId != 1 && rankId != 2)
                    return BadRequest("Rank Ids should be 1(Alfa) or 2(Beta). " +
                        "If you want to create a course suitable for both ranks, you have to add them both with a comma(,)");
            }

            var result = await CheckAndPathingFile(courseDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid image file.");

            await _manager.CourseService.UpdateOneCourseAsync(id, result.courseDto, false);

            return NoContent();
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneCourseByIdAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.CourseService.DeleteOneCourseAsync(id, false);

            return NoContent();
        }

        // Patch
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PartiallyUpdateOneCourseAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<CourseDtoForUpdate> coursePatch)
        {
            //JsonPatchDocument<Course> : JSON verilerini bir varlık sinifi(orn. Course) uzerinde uygulamak icin kullanilir

            if (coursePatch is null)
                return BadRequest(); //400

            var result = await _manager.CourseService.GetOneCourseForPatchAsync(id, false); //(CourseDtoForUpdate, Course)

            coursePatch.ApplyTo(result.courseDtoForUpdate, ModelState); //gelen JSON yamalarini "entity" nesnesine uygular

            TryValidateModel(result.courseDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.CourseService.SaveChangesForUpdateAsync(result.courseDtoForUpdate, result.course);

            return NoContent();
        }

        // Patch
        [HttpPatch("image/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneCourseImageAsync([FromRoute(Name = "id")] int id, IFormFile file)
        {
            // check entity
            var entity = await _manager
                .CourseService
                .GetOneCourseByIdAsync(id, false);

            if (entity is null)
                return NotFound();

            // check file
            if (file is null || file.Length == 0 || !IsValidImageFile(file))
                return BadRequest("Please provide a valid image file.");

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _manager.CourseService.UpdateOneCourseImageAsync(id, uniqueFileName, false);

            return NoContent();
        }

        // Options
        [HttpOptions]
        [Authorize]
        public IActionResult GetCourseOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, OPTIONS");
            return Ok();
        }

        // Check File
        private async Task<(T courseDto, bool isValidate)> CheckAndPathingFile<T>(T courseDto, IFormFile file)
            where T : CourseDtoForManipulation
        {
            var isValidate = true;

            if (file is null || file.Length == 0 || !IsValidImageFile(file))
                isValidate = false;

            if (isValidate)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                courseDto.CourseThumbnail = uniqueFileName;
            }

            return (courseDto, isValidate);
        }

        // Image Validation
        private bool IsValidImageFile(IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpg" &&
                file.ContentType.ToLower() != "image/jpeg" &&
                file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".jpeg";
        }
    }
}
