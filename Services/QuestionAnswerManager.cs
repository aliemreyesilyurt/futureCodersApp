using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public QuestionAnswerManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<QuestionAnswerDto> CreateOneQuestionAnswerAsync(string userId, int questionOptionId)
        {
            //check-question
            await CheckQuestionOptionById(questionOptionId, false);

            var questionAnswerDto = new QuestionAnswerDtoForInsertion()
            {
                UserId = userId,
                QuestionOptionId = questionOptionId
            };

            var entity = _mapper.Map<QuestionAnswer>(questionAnswerDto);
            _manager.QuestionAnswer.Create(entity);
            await _manager.SaveAsync();

            return _mapper.Map<QuestionAnswerDto>(entity);
        }

        // Get-All
        public async Task<QuestionAnswerDto> GetAllQuestionAnswersAsync(string userId, bool trackChanges)
        {
            var questionAnswers = await _manager
                .QuestionAnswer
                .GetAllQuestionAnswersAsync(userId, trackChanges);

            return questionAnswers;
        }

        // Check-Step
        private async Task CheckQuestionOptionById(int questionOptionId, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .QuestionOption
                .GetOneQuestionOptionByIdAsync(questionOptionId, trackChanges);

            if (entity is null)
                throw new QuestionOptionNotFoundException(questionOptionId);
        }
    }
}
