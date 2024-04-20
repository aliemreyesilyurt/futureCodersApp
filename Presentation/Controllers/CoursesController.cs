using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contract;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public CoursesController(IServiceManager manager)
        {
            _manager = manager;
        }


        // Get
        [HttpHead]
        [HttpGet]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneCourseAsync([FromBody] CourseDtoForInsertion courseDto)
        {
            var course = await _manager.CourseService.CreateOneCourseAsync(courseDto);

            return StatusCode(201, course);
        }

        //Put
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCourseAsync([FromRoute(Name = "id")] int id, [FromBody] CourseDtoForUpdate courseDto)
        {
            await _manager.CourseService.UpdateOneCourseAsync(id, courseDto, false);

            return NoContent(); //204
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCourseByIdAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.CourseService.DeleteOneCourseAsync(id, false);
            //CourseManager uzerinde DeleteOneCourse metodunda entity kontrolu yapildigi icin burada tekrar yapmaya gerek yok!

            return NoContent();
        }

        //Patch
        [HttpPatch("{id:int}")]
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

        //Options
        [HttpOptions]
        public IActionResult GetBookOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
