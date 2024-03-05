using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contract;

namespace Services
{
    public class CourseManager : ICourseService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public CourseManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public Course CreateOneCourse(Course course)
        {
            _manager.Course.CreateOneCourse(course);
            _manager.Save();

            return course;
        }

        public void DeleteOneCourse(int id, bool trackChanges)
        {
            //check entity
            var entity = _manager
                .Course
                .GetOneCourseById(id, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(id);

            _manager.Course.DeleteOneCourse(entity);
            _manager.Save();

        }

        public IEnumerable<Course> GetAllCourses(bool trackChanges)
        {
            return _manager.Course.GetAllCourses(trackChanges);
        }

        public Course GetOneCourseById(int id, bool trackChanges)
        {
            var course = _manager.Course.GetOneCourseById(id, trackChanges);
            if (course is null)
                throw new CourseNotFoundException(id);
            return course;
        }

        public void UpdateOneCourse(int id, CourseDtoForUpdate courseDto, bool trackChanges)
        {
            //check entity
            var entity = _manager
                .Course
                .GetOneCourseById(id, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(id);

            //mapping
            entity = _mapper.Map<Course>(courseDto);

            _manager.Course.Update(entity);
            _manager.Save();

        }
    }
}
