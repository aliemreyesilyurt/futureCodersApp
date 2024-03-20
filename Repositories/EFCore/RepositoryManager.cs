using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<ICourseRankRepository> _courseRankRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _courseRankRepository = new Lazy<ICourseRankRepository>(() => new CourseRankRepository(_context));
        }

        public ICourseRepository Course => _courseRepository.Value;
        public ICourseRankRepository CourseRank => _courseRankRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
