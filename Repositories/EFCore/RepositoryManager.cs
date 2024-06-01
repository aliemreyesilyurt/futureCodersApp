using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseRankRepository _courseRankRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserStepRepository _userStepRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IStepRepository _stepRepository;
        private readonly IReviewRepository _reviewRepository;

        public RepositoryManager(RepositoryContext context,
            ICourseRepository courseRepository,
            ICourseRankRepository courseRankRepository,
            IBlogRepository blogRepository,
            IStepRepository stepRepository,
            IReviewRepository reviewRepository,
            IUserStepRepository userStepRepository,
            IUserCourseRepository userCourseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
            _courseRankRepository = courseRankRepository;
            _blogRepository = blogRepository;
            _stepRepository = stepRepository;
            _reviewRepository = reviewRepository;
            _userStepRepository = userStepRepository;
            _userCourseRepository = userCourseRepository;
        }

        public ICourseRepository Course => _courseRepository;
        public ICourseRankRepository CourseRank => _courseRankRepository;
        public IBlogRepository Blog => _blogRepository;
        public IStepRepository Step => _stepRepository;
        public IUserStepRepository UserStep => _userStepRepository;
        public IUserCourseRepository UserCourse => _userCourseRepository;
        public IReviewRepository Review => _reviewRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
