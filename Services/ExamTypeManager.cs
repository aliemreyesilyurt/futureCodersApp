using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ExamTypeManager : IExamTypeService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ExamTypeManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<ExamTypeDto> CreateOneStepAsync(ExamTypeDtoForInsertion examTypeDto)
        {
            var entity = _mapper.Map<ExamType>(examTypeDto);
            _manager.ExamType.Create(entity);
            await _manager.SaveAsync();

            return _mapper.Map<ExamTypeDto>(entity);
        }

        // Delete
        public async Task DeleteOneStepAsync(int id, bool trackChanges)
        {
            //check entity
            var examType = await GetOneExamTypeByIdAndCheckExist(id, trackChanges);

            _manager.ExamType.Delete(examType);
            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<IEnumerable<ExamTypeDto>> GetAllStepsAsync(bool trackChanges)
        {
            var examTypes = await _manager
                .ExamType
                .GetAllExamTypesAsync(trackChanges);

            var examTypeDto = _mapper.Map<IEnumerable<ExamTypeDto>>(examTypes);

            return examTypeDto;
        }

        // Check
        private async Task<ExamType> GetOneExamTypeByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .ExamType
                .GetOneExamTypeByIdAsync(id, trackChanges);

            if (entity is null)
                throw new ExamTypeNotFoundException(id);

            return entity;
        }
    }
}
