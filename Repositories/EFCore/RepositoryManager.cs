using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseRankRepository _courseRankRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IStepRepository _stepRepository;

        public RepositoryManager(RepositoryContext context,
            ICourseRepository courseRepository,
            ICourseRankRepository courseRankRepository,
            IBlogRepository blogRepository,
            IStepRepository stepRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
            _courseRankRepository = courseRankRepository;
            _blogRepository = blogRepository;
            _stepRepository = stepRepository;
        }

        public ICourseRepository Course => _courseRepository;
        public ICourseRankRepository CourseRank => _courseRankRepository;
        public IBlogRepository Blog => _blogRepository;
        public IStepRepository Step => _stepRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
