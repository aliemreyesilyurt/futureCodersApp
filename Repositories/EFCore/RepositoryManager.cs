using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
        }

        public ICourseRepository Course => _courseRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
