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
        private readonly IUserStepService _userStepService;
        private readonly IUserCourseService _userCourseService;
        private readonly IExamTypeService _examTypeService;
        private readonly IQuestionOptionService _questionOptionService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionAnswerService _questionAnswerService;

        public ServiceManager(ICourseService courseService,
            IAuthenticationService authenticationService,
            IBlogService blogService,
            IStepService stepService,
            IReviewService reviewService,
            IUserStepService userStepService,
            IUserCourseService userCourseService,
            IExamTypeService examTypeService,
            IQuestionOptionService questionOptionService,
            IQuestionService questionService,
            IQuestionAnswerService questionAnswerService)
        {
            _courseService = courseService;
            _authenticationService = authenticationService;
            _blogService = blogService;
            _stepService = stepService;
            _reviewService = reviewService;
            _userStepService = userStepService;
            _userCourseService = userCourseService;
            _examTypeService = examTypeService;
            _questionOptionService = questionOptionService;
            _questionService = questionService;
            _questionAnswerService = questionAnswerService;
        }

        public ICourseService CourseService => _courseService;
        public IBlogService BlogService => _blogService;
        public IStepService StepService => _stepService;
        public IUserStepService UserStepService => _userStepService;
        public IUserCourseService UserCourseService => _userCourseService;
        public IReviewService ReviewService => _reviewService;
        public IExamTypeService ExamTypeService => _examTypeService;
        public IQuestionOptionService QuestionOptionService => _questionOptionService;
        public IQuestionService QuestionService => _questionService;
        public IQuestionAnswerService QuestionAnswerService => _questionAnswerService;
        public IAuthenticationService AuthenticationService => _authenticationService;
    }
}
