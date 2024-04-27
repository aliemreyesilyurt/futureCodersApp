namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
