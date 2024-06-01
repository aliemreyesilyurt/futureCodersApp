﻿using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICourseService _courseService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IBlogService _blogService;
        private readonly IStepService _stepService;
        private readonly IReviewService _reviewService;
        private readonly IUserStepService _userStepService;
        private readonly IUserCourseService _userCourseService;

        public ServiceManager(ICourseService courseService,
            IAuthenticationService authenticationService,
            IBlogService blogService,
            IStepService stepService,
            IReviewService reviewService,
            IUserStepService userStepService,
            IUserCourseService userCourseService)
        {
            _courseService = courseService;
            _authenticationService = authenticationService;
            _blogService = blogService;
            _stepService = stepService;
            _reviewService = reviewService;
            _userStepService = userStepService;
            _userCourseService = userCourseService;
        }

        public ICourseService CourseService => _courseService;
        public IBlogService BlogService => _blogService;
        public IStepService StepService => _stepService;
        public IUserStepService UserStepService => _userStepService;
        public IUserCourseService UserCourseService => _userCourseService;
        public IReviewService ReviewService => _reviewService;
        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
