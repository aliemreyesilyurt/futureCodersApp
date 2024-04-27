namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICourseRepository Course { get; }
        ICourseRankRepository CourseRank { get; }
        IBlogRepository Blog { get; }
        Task SaveAsync();
    }
}
