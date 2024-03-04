using Repositories.Contracts;
using Services.Contract;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICourseService> _courseService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerService logger)
        {
            _courseService = new Lazy<ICourseService>(() => new CourseManager(repositoryManager, logger));
        }

        public ICourseService CourseService => _courseService.Value;
    }
}
