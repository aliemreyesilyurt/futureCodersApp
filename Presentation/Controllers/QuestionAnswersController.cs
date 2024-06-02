using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/question-answer")]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public QuestionAnswersController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllQuestionAnswersAsync([FromQuery] string userId)
        {
            var questionAnswers = await _manager
                .QuestionAnswerService
                .GetAllQuestionAnswersAsync(userId, false);

            return Ok(questionAnswers);
        }

        // Create
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateOneQuestionAnswerAsync([FromBody] int questionOptionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var questionAnswer = await _manager
                .QuestionAnswerService
                .CreateOneQuestionAnswerAsync(userId, questionOptionId);

            return StatusCode(201, questionAnswer);
        }
    }
}
