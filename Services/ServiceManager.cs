using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICourseService _courseService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IBlogService _blogService;
        private readonly IStepService _stepService;
        private readonly IReviewService _reviewService;

        public ServiceManager(ICourseService courseService,
            IAuthenticationService authenticationService,
            IBlogService blogService,
            IStepService stepService,
            IReviewService reviewService)
        {
            _courseService = courseService;
            _authenticationService = authenticationService;
            _blogService = blogService;
            _stepService = stepService;
            _reviewService = reviewService;
        }

        public ICourseService CourseService => _courseService;
        public IBlogService BlogService => _blogService;
        public IStepService StepService => _stepService;
        public IReviewService ReviewService => _reviewService;
        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
