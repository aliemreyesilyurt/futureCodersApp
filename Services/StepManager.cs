using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class StepManager : IStepService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        private const string MediaFolderPath = "Media/Steps/";

        public StepManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<StepDto> CreateOneStepAsync(StepDtoForInsertion stepDto)
        {
            var entity = _mapper.Map<Step>(stepDto);
            _manager.Step.CreateOneStep(entity);
            await _manager.SaveAsync();

            return _mapper.Map<StepDto>(entity);
        }

        // Delete
        public async Task DeleteOneStepAsync(int id, bool trackChanges)
        {
            //check entity
            var step = await GetOneStepByIdAndCheckExist(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, step.VideoPath);

            _manager.Step.DeleteOneStep(step);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<IEnumerable<StepDto>> GetAllStepsAsync(StepParameters stepParameters, bool trackChanges)
        {
            var stepsWithParams = await _manager
                .Step
                .GetAllStepsAsync(stepParameters, trackChanges);

            var stepsDto = _mapper.Map<IEnumerable<StepDto>>(stepsWithParams);

            return stepsDto;
        }

        // Get-One
        public async Task<StepDto> GetOneStepByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var step = await GetOneStepByIdAndCheckExist(id, trackChanges);

            var stepDto = _mapper.Map<StepDto>(step);

            return stepDto;
        }

        // Update
        public async Task UpdateOneStepAsync(int id, StepDtoForUpdate stepDto, bool trackChanges)
        {
            //check entity
            var step = await GetOneStepByIdAndCheckExist(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, step.VideoPath);

            //mapping
            step = _mapper.Map<Step>(stepDto);

            if (File.Exists(filePath))
                File.Delete(filePath);

            _manager.Step.Update(step);
            await _manager.SaveAsync();
        }

        // Patch-Status
        public async Task UpdateOneStepStatusAsync(int id, bool trackChanges)
        {
            //check entity
            var step = await GetOneStepByIdAndCheckExist(id, trackChanges);

            step.Status = true;

            _manager.Step.Update(step);
            await _manager.SaveAsync();
        }

        // Patch-Video
        public async Task UpdateOneStepVideoAsync(int id, string fileName, bool trackChanges)
        {
            //check entity
            var step = await GetOneStepByIdAndCheckExist(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, step.VideoPath);

            step.VideoPath = fileName;

            _manager.Step.Update(step);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await _manager.SaveAsync();
        }

        // Check
        private async Task<Step> GetOneStepByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Step
                .GetOneStepByIdAsync(id, trackChanges);

            if (entity is null)
                throw new StepNotFoundException(id);

            return entity;
        }


    }
}
