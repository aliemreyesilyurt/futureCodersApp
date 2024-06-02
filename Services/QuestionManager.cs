using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class QuestionManager : IQuestionService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public QuestionManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<QuestionDto> CreateOneQuestionAsync(QuestionDtoForInsertion questionDto)
        {
            var entity = _mapper.Map<Question>(questionDto);
            _manager.Question.Create(entity);
            await _manager.SaveAsync();

            return _mapper.Map<QuestionDto>(entity);
        }

        // Delete
        public async Task DeleteOneQuestionAsync(int id, bool trackChanges)
        {
            //check exam type
            var question = await GetOneQuestionByIdAndCheckExist(id, trackChanges);

            _manager.Question.Delete(question);
            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync(QuestionParameters questionParameters, bool trackChanges)
        {
            if (questionParameters.ExamTypeId != null)
            {
                var examTypeId = (int)questionParameters.ExamTypeId;
                await GetOneExamTypeByIdAndCheckExist(examTypeId, trackChanges);
            }

            var questionWithParams = await _manager
                .Question
                .GetAllQuestionsAsync(questionParameters, trackChanges);

            var questionsDto = _mapper.Map<IEnumerable<QuestionDto>>(questionWithParams);

            return questionsDto;
        }

        // Get-One
        public async Task<QuestionDto> GetOneQuestionByIdAsync(int id, bool trackChanges)
        {
            //chech entity
            var question = await GetOneQuestionByIdAndCheckExist(id, trackChanges);

            var questionDto = _mapper.Map<QuestionDto>(question);

            return questionDto;
        }

        // Update
        public async Task UpdateOneQuestionTextAsync(QuestionDtoForUpdate questionDto, bool trackChanges)
        {
            //check entity
            var question = await GetOneQuestionByIdAndCheckExist(questionDto.Id, trackChanges);

            //mappig
            question.QuestionText = questionDto.QuestionText;

            _manager.Question.Update(question);
            await _manager.SaveAsync();
        }

        // Check
        private async Task<Question> GetOneQuestionByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Question
                .GetOneQuestionByIdAsync(id, trackChanges);

            if (entity is null)
                throw new QuestionNotFoundException(id);

            return entity;
        }

        private async Task GetOneExamTypeByIdAndCheckExist(int questionId, bool trackChanges)
        {
            var examType = await _manager
                .ExamType
                .GetOneExamTypeByIdAsync(questionId, trackChanges);

            if (examType == null)
                throw new ExamTypeNotFoundException(questionId);
        }
    }
}
