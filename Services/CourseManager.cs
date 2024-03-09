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
        public async Task<CourseDto> CreateOneCourseAsync(CourseDtoForInsertion courseDto)
        {
            var entity = _mapper.Map<Course>(courseDto);
            _manager.Course.CreateOneCourse(entity);
            await _manager.SaveAsync();


            return _mapper.Map<CourseDto>(entity);
            //ustteki kullanimda entity(Course) CourseDto'ya donusturulur
        }

        public async Task DeleteOneCourseAsync(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Course
                .GetOneCourseByIdAsync(id, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(id);

            _manager.Course.DeleteOneCourse(entity);
            await _manager.SaveAsync();

        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(bool trackChanges)
        {
            var courses = await _manager.Course.GetAllCoursesAsync(trackChanges);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetOneCourseByIdAsync(int id, bool trackChanges)
        {
            var course = await _manager.Course.GetOneCourseByIdAsync(id, trackChanges);
            if (course is null)
                throw new CourseNotFoundException(id);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<(CourseDtoForUpdate courseDtoForUpdate, Course course)> GetOneCourseForPatchAsync(int id, bool trackChanges)
        {
            var course = await _manager.Course.GetOneCourseByIdAsync(id, trackChanges);

            if (course is null)
                throw new CourseNotFoundException(id);

            var courseDtoForUpdate = _mapper.Map<CourseDtoForUpdate>(course);

            return (courseDtoForUpdate, course); // (Tuple)
            /*Bu metod hem bir CourseDtoForUpdate hem de Course return etmemizi istiyor
            Bu sebeple Map islemi yaparak ona hem bir Course hem de CourseDtoForUpdate vermis oluyoruz*/
        }

        public async Task UpdateOneCourseAsync(int id, CourseDtoForUpdate courseDto, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Course
                .GetOneCourseByIdAsync(id, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(id);

            //mapping
            entity = _mapper.Map<Course>(courseDto);

            _manager.Course.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task SaveChangesForUpdateAsync(CourseDtoForUpdate courseDtoForUpdate, Course course)
        {
            _mapper.Map(courseDtoForUpdate, course);
            await _manager.SaveAsync();
        }
    }
}
