using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserCourseService
    {
        Task<UserCourseDto> GetAllUserCoursesAsync(string userId, bool trackChanges);
        Task<UserCourse> CreateOneUserCourseAsync(string userId, int courseId);
    }
}
