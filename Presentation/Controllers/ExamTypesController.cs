using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/exam-types")]
    public class ExamTypesController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ExamTypesController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllExamTypesAsync()
        {
            var examTypes = await _manager
                .ExamTypeService
                .GetAllStepsAsync(false);

            return Ok(examTypes);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOneExamTypeAsync([FromBody] ExamTypeDtoForInsertion examTypeDto)
        {
            var examType = await _manager
                .ExamTypeService
                .CreateOneStepAsync(examTypeDto);

            return StatusCode(201, examType);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneExamTypeAsync([FromRoute(Name = "id")] int id)
        {
            await _manager
                .ExamTypeService
                .DeleteOneStepAsync(id, false);

            return NoContent();
        }
    }
}
