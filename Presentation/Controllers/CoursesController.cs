using Entities.Models;
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
        public IActionResult CreateOneCourse([FromBody] Course course)
        {
            if (course == null)
                return BadRequest(); //400

            _manager.CourseService.CreateOneCourse(course);

            return StatusCode(201, course);
        }

        //Put
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCourse([FromRoute(Name = "id")] int id, [FromBody] Course course)
        {
            if (course == null)
                return BadRequest(); //400

            _manager.CourseService.UpdateOneCourse(id, course, true);

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
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Course> coursePatch)
        {
            //JsonPatchDocument<Course> : JSON verilerini bir varlık sinifi(orn. Course) uzerinde uygulamak icin kullanilir

            //check entity
            var entity = _manager
                .CourseService
                .GetOneCourseById(id, true);

            if (entity is null)
                return NotFound(); //404

            coursePatch.ApplyTo(entity); //gelen JSON yamalarini "entity" nesnesine uygular

            _manager.CourseService.UpdateOneCourse(id, entity, true);

            return NoContent();
        }
    }
}
