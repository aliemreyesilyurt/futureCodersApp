namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICourseRepository Course { get; }
        ICourseRankRepository CourseRank { get; }
        IBlogRepository Blog { get; }
        IStepRepository Step { get; }
        IUserStepRepository UserStep { get; }
        IUserCourseRepository UserCourse { get; }
        IReviewRepository Review { get; }
        IExamTypeRepository ExamType { get; }
        IQuestionOptionRepository QuestionOption { get; }
        IQuestionRepository Question { get; }
        IQuestionAnswerRepository QuestionAnswer { get; }
        Task SaveAsync();
    }
}
