using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BlogManager : IBlogService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        private const string MediaFolderPath = "Media/Blogs/";

        public BlogManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<BlogDto> CreateOneBlogAsync(BlogDtoForInsertion blogDto)
        {
            var entity = _mapper.Map<Blog>(blogDto);
            _manager.Blog.CreateOneBlog(entity);
            await _manager.SaveAsync();

            return _mapper.Map<BlogDto>(entity);
            //ustteki kullanimda entity(Blog) BlogDto'ya donusturulur
        }

        // Delete
        public async Task DeleteOneBlogAsync(int id, bool trackChanges)
        {
            //check entity
            var blog = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, blog.BlogImage);

            _manager.Blog.DeleteOneBlog(blog);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<(IEnumerable<BlogDto> blogs, MetaData metaData)> GetAllBlogsAsync(BlogParameters blogParameters, bool trackChanges)
        {
            var blogsWithMetaData = await _manager
                .Blog
                .GetAllBlogsAsync(blogParameters, trackChanges);

            var blogsDto = _mapper.Map<IEnumerable<BlogDto>>(blogsWithMetaData);

            return (blogsDto, blogsWithMetaData.MetaData);
        }

        // Get-One
        public async Task<BlogDto> GetOneBlogByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var blog = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            var blogDto = _mapper.Map<BlogDto>(blog);

            return blogDto;
        }

        // Update
        public async Task UpdateOneBlogAsync(int id, BlogDtoForUpdate blogDto, bool trackChanges)
        {
            //check entity
            var entity = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            //mappig
            entity = _mapper.Map<Blog>(blogDto);

            _manager.Blog.Update(entity);
            await _manager.SaveAsync();
        }

        // Patch-Image
        public async Task UpdateOneBlogImageAsync(int id, string fileName, bool trackChanges)
        {
            //check entity
            var blog = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            var filePath = Path.Combine(MediaFolderPath, blog.BlogImage);

            blog.BlogImage = fileName;

            _manager.Blog.Update(blog);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await _manager.SaveAsync();
        }

        // Check
        private async Task<Blog> GetOneBlogByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Blog
                .GetOneBlogByIdAsync(id, trackChanges);

            if (entity is null)
                throw new BlogNotFoundException(id);

            return entity;
        }
    }
}
