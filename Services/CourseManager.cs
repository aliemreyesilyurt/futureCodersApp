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
        public CourseDto CreateOneCourse(CourseDtoForInsertion courseDto)
        {
            var entity = _mapper.Map<Course>(courseDto);
            _manager.Course.CreateOneCourse(entity);
            _manager.Save();


            return _mapper.Map<CourseDto>(entity);
            //ustteki kullanimda entity(Course) CourseDto'ya donusturulur
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

        public IEnumerable<CourseDto> GetAllCourses(bool trackChanges)
        {
            var courses = _manager.Course.GetAllCourses(trackChanges);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public CourseDto GetOneCourseById(int id, bool trackChanges)
        {
            var course = _manager.Course.GetOneCourseById(id, trackChanges);
            if (course is null)
                throw new CourseNotFoundException(id);
            return _mapper.Map<CourseDto>(course);
        }

        public (CourseDtoForUpdate courseDtoForUpdate, Course course) GetOneCourseForPatch(int id, bool trackChanges)
        {
            var course = _manager.Course.GetOneCourseById(id, trackChanges);

            if (course is null)
                throw new CourseNotFoundException(id);

            var courseDtoForUpdate = _mapper.Map<CourseDtoForUpdate>(course);

            return (courseDtoForUpdate, course); // (Tuple)
            /*Bu metod hem bir CourseDtoForUpdate hem de Course return etmemizi istiyor
            Bu sebeple Map islemi yaparak ona hem bir Course hem de CourseDtoForUpdate vermis oluyoruz*/
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

        public void SaveChangesForUpdate(CourseDtoForUpdate courseDtoForUpdate, Course course)
        {
            _mapper.Map(courseDtoForUpdate, course);
            _manager.Save();
        }
    }
}
