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

        private const string MediaFolderPath = "Media/Courses/";
        public CourseManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Get-All
        public async Task<(IEnumerable<CourseDto> courses, MetaData metaData)> GetAllCoursesAsync(CourseParameters courseParameters,
            bool trackChanges)
        {
            if (!courseParameters.ValidRank)
                throw new InvalidRankBadRequestException();

            var coursesWithRank = await _manager
                .CourseRank
                .GetAllCourseRanksWithParamsAsync(courseParameters, trackChanges);

            var coursesWithMetaData = await _manager
                .Course
                .GetAllCoursesAsync(coursesWithRank, courseParameters, trackChanges);

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(coursesWithMetaData);

            // CourseDto'ya RankIds bilgisini ekleyelim
            foreach (var courseDto in coursesDto)
            {
                var rankIds = coursesWithRank
                    .Where(c => c.CourseId == courseDto.Id)
                    .Select(cr => cr.RankId)
                    .ToList();

                courseDto.RankIds = rankIds;
            }

            return (coursesDto, coursesWithMetaData.MetaData);
        }

        // Get-One
        public async Task<CourseDto> GetOneCourseByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            var courseDto = _mapper.Map<CourseDto>(course);

            var courseRank = await _manager
                .CourseRank
                .GetOneCourseRankByIdAsync(course.Id, trackChanges);

            var rankIds = courseRank
                .Select(cr => cr.RankId)
                .ToList();

            courseDto.RankIds = rankIds;

            return courseDto;
        }

        // Create
        public async Task<CourseDtoForInsertion> CreateOneCourseAsync(CourseDtoForInsertion courseDto)
        {
            var entity = _mapper.Map<Course>(courseDto);
            _manager.Course.CreateOneCourse(entity);
            await _manager.SaveAsync();

            var courseId = entity.Id;

            foreach (var id in courseDto.RankIds)
            {
                var tmpObj = new CourseRank()
                {
                    CourseId = courseId,
                    RankId = id
                };
                _manager.CourseRank.Create(tmpObj);
                await _manager.SaveAsync();
            }

            return courseDto;
        }

        // Delete
        public async Task DeleteOneCourseAsync(int id, bool trackChanges)
        {
            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, course.CourseThumbnail);

            _manager.Course.DeleteOneCourse(course);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await _manager.SaveAsync();
        }

        // Patch
        public async Task<(CourseDtoForUpdate courseDtoForUpdate, Course course)>
            GetOneCourseForPatchAsync(int id, bool trackChanges)
        {
            var rankIds = new List<int>();

            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            var courseRanks = await _manager.CourseRank.GetOneCourseRankByIdAsync(id, trackChanges);

            var courseDtoForUpdate = _mapper.Map<CourseDtoForUpdate>(course);

            foreach (var cr in courseRanks)
            {
                rankIds.Add(cr.RankId);
            }

            courseDtoForUpdate.RankIds = rankIds;

            return (courseDtoForUpdate, course); // (Tuple)
            /*Bu metod hem bir CourseDtoForUpdate hem de Course return etmemizi istiyor
            Bu sebeple Map islemi yaparak ona hem bir Course hem de CourseDtoForUpdate vermis oluyoruz*/
        }

        // Update
        public async Task UpdateOneCourseAsync(int id, CourseDtoForUpdate courseDto, bool trackChanges)
        {
            //check
            var entity = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            //check rank Ids
            var courseRanks = await _manager.CourseRank.GetOneCourseRankByIdAsync(id, trackChanges);

            foreach (var cr in courseRanks)
            {
                _manager.CourseRank.Delete(cr);
            }

            //mapping
            entity = _mapper.Map<Course>(courseDto);

            _manager.Course.Update(entity);

            foreach (var rankIds in courseDto.RankIds)
            {
                _manager.CourseRank.Create(new CourseRank
                {
                    RankId = rankIds,
                    CourseId = id
                });
            }

            await _manager.SaveAsync();
        }

        // Save
        public async Task SaveChangesForUpdateAsync(CourseDtoForUpdate courseDtoForUpdate, Course course)
        {
            var entity = _mapper.Map<Course>(courseDtoForUpdate);
            _manager.Course.Update(entity);

            var courseRanks = await _manager
                .CourseRank
                .GetOneCourseRankByIdAsync(course.Id, true);

            foreach (var cr in courseRanks)
            {
                _manager.CourseRank.Delete(cr);
            }

            foreach (var rankIds in courseDtoForUpdate.RankIds)
            {
                _manager.CourseRank.Create(new CourseRank
                {
                    RankId = rankIds,
                    CourseId = course.Id
                });
            }

            await _manager.SaveAsync();
        }

        // Update Course Status
        public async Task UpdateOneCourseStatusAsync(int courseId, bool trackChanges)
        {
            var course = await _manager
                .Course
                .GetOneCourseByIdAsync(courseId, trackChanges);

            course.IsOver = true;
        }

        // Patch-Image
        public async Task UpdateOneCourseImageAsync(int id, string fileName, bool trackChanges)
        {
            //check entity
            var course = await GetOneCourseByIdAndCheckExists(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, course.CourseThumbnail);

            course.CourseThumbnail = fileName;

            _manager.Course.Update(course);

            if (File.Exists(filePath))
                File.Delete(filePath);

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
