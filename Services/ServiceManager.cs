using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICourseService> _courseService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerService logger,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _courseService = new Lazy<ICourseService>(() => new CourseManager(repositoryManager, logger, mapper));

            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager, configuration));
        }

        public ICourseService CourseService => _courseService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
