using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class QuestionOptionManager : IQuestionOptionService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public QuestionOptionManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<QuestionOptionDto> CreateOneQuestionOptionAsync(QuestionOptionDtoForInsertion optionDto)
        {
            //check
            await CheckQuestionOptionDto(optionDto);

            var entity = _mapper.Map<QuestionOption>(optionDto);
            _manager.QuestionOption.Create(entity);
            await _manager.SaveAsync();

            return _mapper.Map<QuestionOptionDto>(entity);
        }

        // Delete
        public async Task DeleteOneQuestionOptionAsync(int id, bool trackChanges)
        {
            //check question option
            var questionOption = await GetOneQuestionOptionByIdAndCheckExist(id, trackChanges);

            _manager.QuestionOption.Delete(questionOption);
            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<IEnumerable<QuestionOptionDto>> GetAllQuestionOptionsAsync(QuestionOptionParameters optionParameters, bool trackChanges)
        {
            if (optionParameters.QuestionId != null)
            {
                var questionId = (int)optionParameters.QuestionId;
                await GetOneQuestionByIdAndCheckExist(questionId, trackChanges);
            }

            var questionOptionWithParams = await _manager
                .QuestionOption
                .GetAllQuestionOptionsAsync(optionParameters, trackChanges);

            var questionOptionsDto = _mapper.Map<IEnumerable<QuestionOptionDto>>(questionOptionWithParams);

            return questionOptionsDto;
        }

        // Get-One
        public async Task<QuestionOptionDto> GetOneQuestionOptionByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var questionOption = await GetOneQuestionOptionByIdAndCheckExist(id, trackChanges);

            var questionOptionDto = _mapper.Map<QuestionOptionDto>(questionOption);

            return questionOptionDto;
        }

        // Update
        public async Task UpdateOneQuestionOptionAsync(QuestionOptionDtoForUpdate optionDto, bool trackChanges)
        {
            //check entity
            var questionOption = await GetOneQuestionOptionByIdAndCheckExist(optionDto.Id, trackChanges);

            //mappig
            questionOption = _mapper.Map<QuestionOption>(optionDto);

            _manager.QuestionOption.Update(questionOption);
            await _manager.SaveAsync();
        }

        // Check
        private async Task<QuestionOption> GetOneQuestionOptionByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .QuestionOption
                .GetOneQuestionOptionByIdAsync(id, trackChanges);

            if (entity is null)
                throw new QuestionOptionNotFoundException(id);

            return entity;
        }

        private async Task GetOneQuestionByIdAndCheckExist(int questionId, bool trackChanges)
        {
            var question = await _manager
                .Question
                .GetOneQuestionByIdAsync(questionId, trackChanges);

            if (question == null)
                throw new QuestionNotFoundException(questionId);
        }

        private async Task CheckQuestionOptionDto(QuestionOptionDtoForInsertion optionDto)
        {
            var questionId = optionDto.QuestionId;

            var questionOptions = await _manager
                .QuestionOption
                .GetAllQuestionOptionsAsync(new QuestionOptionParameters { QuestionId = questionId }, false);

            if (questionOptions.Any(qo => qo.IsTrue) && optionDto.IsTrue)
            {
                throw new InvalidOperationException("There is already a correct answer for this question.");
            }

            if (questionOptions.Count() >= 5)
            {
                throw new InvalidOperationException("Cannot add more than 5 options for a question.");
            }

        }
    }
}
