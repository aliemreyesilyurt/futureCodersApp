namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICourseRepository Course { get; }
        ICourseRankRepository CourseRank { get; }
        IBlogRepository Blog { get; }
        IStepRepository Step { get; }
        IUserStepRepository UserStep { get; }
        IReviewRepository Review { get; }
        Task SaveAsync();
    }
}
