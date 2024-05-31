using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class UserStepManager : IUserStepService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public UserStepManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<UserStep> CreateOneUserStepAsync(UserStepDtoForInsertion userStepDto)
        {
            var entity = _mapper.Map<UserStep>(userStepDto);
            _manager.UserStep.Create(entity);
            await _manager.SaveAsync();

            return entity;
        }

        // Get-All
        public async Task<UserStepDto> GetAllUserStepsAsync(string userId, bool trackChanges)
        {
            var userSteps = await _manager
                .UserStep
                .GetAllUserStepsAsync(userId, trackChanges);

            return userSteps;

        }

        // Delete
        public async Task DeleteOneUserStepAsync(int stepId, bool trackChanges)
        {
            //check entity
            var userStep = await GetOneUserStepByIdAndCheckExist(stepId, trackChanges);

            _manager.UserStep.DeleteOneUserStep(userStep);
            await _manager.SaveAsync();
        }

        // Check
        private async Task<UserStep> GetOneUserStepByIdAndCheckExist(int stepId, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .UserStep
                .GetOneUserStepAsync(stepId, trackChanges);

            if (entity is null)
                throw new UserStepNotFoundException(stepId);

            return entity;
        }
    }
}
