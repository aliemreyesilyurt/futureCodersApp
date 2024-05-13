using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private const string MediaFolderPath = "Media/Blogs/";

        public BlogsController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Get
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBlogsAsync([FromQuery] BlogParameters blogParameters)
        {
            var pagedResult = await _manager
                .BlogService
                .GetAllBlogsAsync(blogParameters, true);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.blogs);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOneBlogAsync([FromRoute] int id)
        {
            var blog = await _manager
                .BlogService
                .GetOneBlogByIdAsync(id, false);

            if (blog == null)
                return NotFound();

            return Ok(blog);
        }

        // Post
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOneBlogAsync([FromForm] BlogDtoForInsertion blogDto, IFormFile file)
        {
            var result = await CheckAndPathingFile(blogDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid image file.");

            var blog = await _manager
                .BlogService
                .CreateOneBlogAsync(blogDto);


            return StatusCode(201, blog);
        }

        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneBlogAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BlogService.DeleteOneBlogAsync(id, false);

            return NoContent();
        }

        // Put
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateOneBlogAsync([FromRoute(Name = "id")] int id, [FromForm] BlogDtoForUpdate blogDto, IFormFile file)
        {
            if (blogDto.Id != id || blogDto.Id == 0)
                return BadRequest("The blog id in the json must match the id of the blog to be updated on the route. And the id field in json must be full!");

            var result = await CheckAndPathingFile(blogDto, file);

            if (result.isValidate == false)
                return BadRequest("Please provide a valid image file.");

            await _manager.BlogService.UpdateOneBlogAsync(id, result.blogDto, false);

            return NoContent();
        }

        // Patch
        [HttpPatch("image/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneBlogImageAsync([FromRoute(Name = "id")] int id, IFormFile file)
        {
            // check entity
            var entity = await _manager
                .BlogService
                .GetOneBlogByIdAsync(id, false);

            if (entity == null)
                return NotFound();


            // check file
            if (file is null || file.Length == 0 || !IsValidImageFile(file))
                return BadRequest("Please provide a valid image file.");

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _manager.BlogService.UpdateOneBlogImageAsync(id, uniqueFileName, false);

            return NoContent();
        }

        // Check File
        private async Task<(T blogDto, bool isValidate)> CheckAndPathingFile<T>(T blogDto, IFormFile file)
            where T : BlogDtoForManipulation
        {
            var isValidate = true;

            if (file is null || file.Length == 0 || !IsValidImageFile(file))
                isValidate = false;

            if (isValidate)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(MediaFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                blogDto.BlogImage = uniqueFileName;
            }

            return (blogDto, isValidate);
        }

        // Image Validation
        private bool IsValidImageFile(IFormFile file)
        {
            if (file.ContentType.ToLower() != "image/jpg" &&
                file.ContentType.ToLower() != "image/jpeg" &&
                file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".jpeg";
        }
    }
}
