using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserCourseManager : IUserCourseService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public UserCourseManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<UserCourse> CreateOneUserCourseAsync(string userId, int courseId)
        {
            //check-course
            var isCorretct = await CheckCourseByIdAndSteps(userId, courseId, false);

            if (isCorretct)
            {
                var userCourseDto = new UserCourseDtoForInsertion()
                {
                    UserId = userId,
                    CourseId = courseId
                };

                var entity = _mapper.Map<UserCourse>(userCourseDto);
                _manager.UserCourse.Create(entity);
                await _manager.SaveAsync();

                return entity;
            }
            else
            {
                throw new InvalidUserCourseBadRequestException(userId, courseId);
            }

        }

        // Get-All
        public async Task<UserCourseDto> GetAllUserCoursesAsync(string userId, bool trackChanges)
        {
            var userCourses = await _manager
                .UserCourse
                .GetAllUserCoursesAsync(userId, trackChanges);

            return userCourses;
        }

        // Check-Course
        private async Task<bool> CheckCourseByIdAndSteps(string userId, int courseId, bool trackChanges)
        {
            // Check entity
            var entity = await _manager
                .Course
                .GetOneCourseByIdAsync(courseId, trackChanges);

            if (entity is null)
                throw new CourseNotFoundException(courseId);

            courseId = entity.Id;
            var parameters = new StepParameters() { CourseId = courseId };

            var steps = await _manager
                .Step
                .GetAllStepsAsync(parameters, new List<int>(), trackChanges);

            var userSteps = await _manager
                .UserStep
                .GetAllUserStepsAsync(userId, trackChanges);

            // steps içindeki her bir id'yi userSteps.StepIds listesinde kontrol et
            foreach (int stepId in steps.Select(s => s.Id))
            {
                if (!userSteps.StepIds.Contains(stepId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
