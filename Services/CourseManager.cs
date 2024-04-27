using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

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
            var entity = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            _manager.Course.DeleteOneCourse(entity);
            await _manager.SaveAsync();

        }

        public async Task<(IEnumerable<CourseDto> courses, MetaData metaData)>
            GetAllCoursesAsync(CourseParameters courseParameters,
            bool trackChanges)
        {
            if (!courseParameters.ValidRank)
                throw new InvalidRankBadRequestException();

            var coursesWithRank = await _manager
                .CourseRank
                .GetAllCourseRanksAsync(courseParameters, trackChanges);

            var coursesWithMetaData = await _manager
                .Course
                .GetAllCoursesAsync(coursesWithRank, courseParameters, trackChanges);

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(coursesWithMetaData);

            return (coursesDto, coursesWithMetaData.MetaData);
        }

        public async Task<CourseDto> GetOneCourseByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            if (course is null)
                throw new CourseNotFoundException(id);
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<(CourseDtoForUpdate courseDtoForUpdate, Course course)>
            GetOneCourseForPatchAsync(int id, bool trackChanges)
        {
            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            var courseDtoForUpdate = _mapper.Map<CourseDtoForUpdate>(course);

            return (courseDtoForUpdate, course); // (Tuple)
            /*Bu metod hem bir CourseDtoForUpdate hem de Course return etmemizi istiyor
            Bu sebeple Map islemi yaparak ona hem bir Course hem de CourseDtoForUpdate vermis oluyoruz*/
        }

        public async Task UpdateOneCourseAsync(int id, CourseDtoForUpdate courseDto, bool trackChanges)
        {
            //check entity
            var entity = await GetOneCourseByIdAndCheckExists(id, trackChanges);

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

        private async Task<Course> GetOneCourseByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Course
                .GetOneCourseByIdAsync(id, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(id);

            return entity;
        }
    }
}
