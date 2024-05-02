using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICourseService _courseService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IBlogService _blogService;

        public ServiceManager(ICourseService courseService,
            IAuthenticationService authenticationService,
            IBlogService blogService)
        {
            _courseService = courseService;
            _authenticationService = authenticationService;
            _blogService = blogService;
        }

        public ICourseService CourseService => _courseService;
        public IBlogService BlogService => _blogService;
        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
