using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BlogsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync()
        {
            return Ok(await _manager
                .BlogService
                .GetAllBlogsAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBlogAsync([FromRoute] int id)
        {
            return Ok(await _manager
                .BlogService
                .GetOneBlogByIdAsync(id, false));
        }
    }
}
