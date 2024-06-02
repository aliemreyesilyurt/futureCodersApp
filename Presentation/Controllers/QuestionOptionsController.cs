using Entities.DataTransferObjects;
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
    [Route("api/question-options")]
    public class QuestionOptionsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public QuestionOptionsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllQuestionOptionsAsync([FromQuery] QuestionOptionParameters optionParameters)
        {
            var questionOptions = await _manager
                .QuestionOptionService
                .GetAllQuestionOptionsAsync(optionParameters, false);

            return Ok(questionOptions);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneQuestionOptionAsync([FromRoute] int id)
        {
            var questionOption = await _manager
                .QuestionOptionService
                .GetOneQuestionOptionByIdAsync(id, false);

            if (questionOption == null)
                return NotFound();

            return Ok(questionOption);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneQuestionOpitonAsync([FromBody] QuestionOptionDtoForInsertion optionDto)
        {
            //check questionId
            await _manager
                .QuestionService
                .GetOneQuestionByIdAsync(optionDto.QuestionId, false);

            var questionOption = await _manager
                .QuestionOptionService
                .CreateOneQuestionOptionAsync(optionDto);

            return StatusCode(201, questionOption);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneQuestionOpitonAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.QuestionOptionService.DeleteOneQuestionOptionAsync(id, false);

            return NoContent();
        }

        // Put
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneQuestionOptionAsync([FromBody] QuestionOptionDtoForUpdate optionDto)
        {
            await _manager.QuestionOptionService.UpdateOneQuestionOptionAsync(optionDto, false);

            return NoContent();
        }
    }
}
