using Entities.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contract;

namespace Presentation.Controllers
{
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
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = _manager.CourseService.GetAllCourses(false);
            return Ok(courses);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneCourseById([FromRoute(Name = "id")] int id)
        {
            var course = _manager
            .CourseService
            .GetOneCourseById(id, false);


            if (course == null)
                return NotFound(); //400

            return Ok(course);
        }

        // Post
        [HttpPost]
        public IActionResult CreateOneCourse([FromBody] CourseDtoForInsertion courseDto)
        {
            if (courseDto is null)
                return BadRequest(); //400

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); //422

            var course = _manager.CourseService.CreateOneCourse(courseDto);

            return StatusCode(201, course);
        }

        //Put
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCourse([FromRoute(Name = "id")] int id, [FromBody] CourseDtoForUpdate courseDto)
        {
            if (courseDto == null)
                return BadRequest(); //400

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); //422

            _manager.CourseService.UpdateOneCourse(id, courseDto, false);

            return NoContent(); //204
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneCourseById([FromRoute(Name = "id")] int id)
        {
            _manager.CourseService.DeleteOneCourse(id, false);
            //CourseManager uzerinde DeleteOneCourse metodunda entity kontrolu yapidligi icin burada tekrar yapmaya gerek yok!

            return NoContent();
        }

        //Patch
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneCourse([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<CourseDtoForUpdate> coursePatch)
        {
            //JsonPatchDocument<Course> : JSON verilerini bir varlık sinifi(orn. Course) uzerinde uygulamak icin kullanilir

            if (coursePatch is null)
                return BadRequest(); //400

            var result = _manager.CourseService.GetOneCourseForPatch(id, false); //(CourseDtoForUpdate, Course)

            coursePatch.ApplyTo(result.courseDtoForUpdate, ModelState); //gelen JSON yamalarini "entity" nesnesine uygular

            TryValidateModel(result.courseDtoForUpdate);

            _manager.CourseService.SaveChangesForUpdate(result.courseDtoForUpdate, result.course);

            return NoContent();
        }
    }
}
