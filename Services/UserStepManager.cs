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
        public async Task<UserStep> CreateOneUserStepAsync(string userId, int stepId)
        {
            //check-step
            await CheckStepById(stepId, false);

            var userStepDto = new UserStepDtoForInsertion()
            {
                UserId = userId,
                StepId = stepId
            };

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

        // Check-Step
        private async Task CheckStepById(int stepId, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Step
                .GetOneStepByIdAsync(stepId, trackChanges);

            if (entity is null)
                throw new StepNotFoundException(stepId);
        }
    }
}
