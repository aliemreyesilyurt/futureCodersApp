using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IUserCourseRepository : IRepositoryBase<UserCourse>
    {
        Task<UserCourseDto> GetAllUserCoursesAsync(string userId, bool trackChanges);
        Task<UserCourse> GetOneUserCourseAsync(int courseId, bool trackChanges);
        void CreateOneUserCourse(UserCourse userCourse);
    }
}
