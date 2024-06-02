using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public QuestionsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllQuestionsAsync([FromQuery] QuestionParameters questionParameters)
        {
            var questions = await _manager
                .QuestionService
                .GetAllQuestionsAsync(questionParameters, false);

            foreach (var question in questions)
            {
                // QuestionOptions koleksiyonunun null olmadığından emin olun
                if (question.QuestionOptions == null)
                {
                    question.QuestionOptions = new List<QuestionOption>();
                }

                // İlgili QuestionOptions'ı alıyoruz
                var questionOptions = await _manager.QuestionOptionService.GetAllQuestionOptionsAsync(new QuestionOptionParameters { QuestionId = question.Id }, false);
                //var questionOptions = await _manager.QuestionOptionService.GetOneQuestionOptionByIdAsync(question.Id, false);

                foreach (var option in questionOptions)
                {
                    // Yeni QuestionOption'u koleksiyona ekliyoruz
                    question.QuestionOptions.Add(new QuestionOption
                    {
                        Id = option.Id,
                        Answer = option.Answer,
                        IsTrue = option.IsTrue,
                        QuestionId = option.QuestionId,
                        Question = null // Gerekirse null bırakabilirsiniz veya uygun şekilde ayarlayabilirsiniz
                    });
                }
            }

            return Ok(questions);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneQuestionAsync([FromRoute] int id)
        {
            var question = await _manager
                .QuestionService
                .GetOneQuestionByIdAsync(id, false);

            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneQuestionAsync([FromBody] QuestionDtoForInsertion questionDto)
        {
            //check examId
            var exam = await _manager
                .ExamTypeService
                .GetAllStepsAsync(false);

            var examIds = exam.Select(x => x.Id).ToList();

            if (!examIds.Contains(questionDto.ExamTypeId))
                return NotFound($"The exam type with id: {questionDto.ExamTypeId} could not found!");


            var question = await _manager
                .QuestionService
                .CreateOneQuestionAsync(questionDto);

            return StatusCode(201, question);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneQuestionAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.QuestionService.DeleteOneQuestionAsync(id, false);

            return NoContent();
        }

        // Put
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneQuestionTextAsync([FromBody] QuestionDtoForUpdate questionDto)
        {
            await _manager.QuestionService.UpdateOneQuestionTextAsync(questionDto, false);

            return NoContent();
        }
    }
}
