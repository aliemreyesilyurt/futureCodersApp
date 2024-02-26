using FutureCodersWebApi.Models;
using FutureCodersWebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FutureCodersWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public CoursesController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        // Get
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                var courses = _repositoryContext.Courses.ToList();
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
                var course = _repositoryContext
                .Courses
                .Where(c => c.Id.Equals(id))
                .SingleOrDefault();

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

                _repositoryContext.Courses.Add(course);
                _repositoryContext.SaveChanges();

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
                // check course?
                var entity = _repositoryContext
                    .Courses
                    .Where(c => c.Id.Equals(id))
                    .SingleOrDefault();

                if (entity == null)
                    return NotFound(); //404

                // check id?
                if (id != course.Id)
                    return BadRequest(); //400

                // mapping
                entity.CourseName = course.CourseName;
                entity.CourseDescription = course.CourseDescription;
                entity.CourseThumbnail = course.CourseThumbnail;
                entity.IsRequire = course.IsRequire;
                entity.Rank = course.Rank;

                _repositoryContext.SaveChanges();
                return Ok();

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
                var course = _repositoryContext
                .Courses
                .Where(c => c.Id.Equals(id))
                .SingleOrDefault();

                if (course == null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} could not found!"
                    });

                _repositoryContext.Courses.Remove(course);
                _repositoryContext.SaveChanges();
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
            try
            {
                //check entity
                var entity = _repositoryContext
                    .Courses
                    .Where(c => c.Id.Equals(id))
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound();

                coursePatch.ApplyTo(entity);
                _repositoryContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
