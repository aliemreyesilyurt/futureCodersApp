namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IBlogService BlogService { get; }
        IStepService StepService { get; }
        IReviewService ReviewService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
