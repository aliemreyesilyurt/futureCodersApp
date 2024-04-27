namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IBlogService BlogService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
