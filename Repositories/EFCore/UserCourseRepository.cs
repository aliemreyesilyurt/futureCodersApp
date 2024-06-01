using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class UserCourseRepository : RepositoryBase<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneUserCourse(UserCourse userCourse) => Create(userCourse);

        public async Task<UserCourseDto> GetAllUserCoursesAsync(string userId, bool trackChanges)
        {
            var courseIds = await FindAll(trackChanges)
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToListAsync();

            var userCourses = new UserCourseDto
            {
                UserId = userId,
                CourseIds = courseIds
            };

            return userCourses;
        }

        public async Task<UserCourse> GetOneUserCourseAsync(int courseId, bool trackChanges) =>
            await FindByCondition(uc => uc.CourseId == courseId, trackChanges)
                .SingleOrDefaultAsync();
    }
}
