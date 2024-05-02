using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseRankRepository _courseRankRepository;
        private readonly IBlogRepository _blogRepository;

        public RepositoryManager(RepositoryContext context
            , ICourseRepository courseRepository,
            ICourseRankRepository courseRankRepository,
            IBlogRepository blogRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
            _courseRankRepository = courseRankRepository;
            _blogRepository = blogRepository;
        }

        public ICourseRepository Course => _courseRepository;
        public ICourseRankRepository CourseRank => _courseRankRepository;
        public IBlogRepository Blog => _blogRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
