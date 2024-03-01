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
            try
            {
                var courses = _manager.CourseService.GetAllCourses(false);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneCourseById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var course = _manager
                .CourseService
                .GetOneCourseById(id, false);


                if (course == null)
                    return NotFound(); //400

                return Ok(course);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Post
        [HttpPost]
        public IActionResult CreateOneCourse([FromBody] Course course)
        {
            try
            {
                if (course == null)
                    return BadRequest(); //400

                _manager.CourseService.CreateOneCourse(course);

                return StatusCode(201, course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Put
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCourse([FromRoute(Name = "id")] int id, [FromBody] Course course)
        {
            try
            {
                if (course == null)
                    return BadRequest(); //400

                _manager.CourseService.UpdateOneCourse(id, course, true);

                return NoContent(); //204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneCourseById([FromRoute(Name = "id")] int id)
        {
            try
            {
                _manager.CourseService.DeleteOneCourse(id, false);
                //CourseManager uzerinde DeleteOneCourse metodunda entity kontrolu yapidligi icin burada tekrar yapmaya gerek yok!

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Patch
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Course> coursePatch)
        {
            //JsonPatchDocument<Course> : JSON verilerini bir varlık sinifi(orn. Course) uzerinde uygulamak icin kullanilir

            try
            {
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
