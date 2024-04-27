using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<ICourseRankRepository> _courseRankRepository;
        private readonly Lazy<IBlogRepository> _blogRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _courseRankRepository = new Lazy<ICourseRankRepository>(() => new CourseRankRepository(_context));
            _blogRepository = new Lazy<IBlogRepository>(() => new BlogRepository(_context));
        }

        public ICourseRepository Course => _courseRepository.Value;
        public ICourseRankRepository CourseRank => _courseRankRepository.Value;
        public IBlogRepository Blog => _blogRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
