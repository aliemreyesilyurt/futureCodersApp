using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface ICourseRankRepository : IRepositoryBase<CourseRank>
    {
        Task<List<CourseRank>> GetAllCourseRanksWithParamsAsync(CourseParameters courseParameters, bool trackCahnges);
        Task<List<CourseRank>> GetOneCourseRankByIdAsync(int id, bool trackChanges);
        void CreateOneCourseRank(CourseRank courseRank);
        void DeleteOneCourseRank(CourseRank courseRank);
        void UpdateOneCourseRank(CourseRank courseRank);

    }
}
