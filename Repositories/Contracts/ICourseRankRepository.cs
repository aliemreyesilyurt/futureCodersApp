using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface ICourseRankRepository : IRepositoryBase<CourseRank>
    {
        Task<PagedList<CourseRank>> GetAllCourseRanksAsync(CourseParameters courseParameters, bool trackCahnges);
        Task<CourseRank> GetOneCourseRankByIdAsync(int id, bool trackChanges);
        void CreateOneCourseRank(CourseRank courseRank);
        void DeleteOneCourseRank(CourseRank courseRank);
        void UpdateOneCourseRank(CourseRank courseRank);

    }
}
