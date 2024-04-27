using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BlogManager : IBlogService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BlogManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        // CREATE
        public async Task<BlogDto> CreateOneBlogAsync(BlogDtoForInsertion blogDto)
        {
            var entity = _mapper.Map<Blog>(blogDto);
            _manager.Blog.CreateOneBlog(entity);
            await _manager.SaveAsync();

            return _mapper.Map<BlogDto>(entity);
            //ustteki kullanimda entity(Blog) BlogDto'ya donusturulur
        }

        // DELETE
        public async Task DeleteOneBlogAsync(int id, bool trackChanges)
        {
            //check entity
            var entity = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            _manager.Blog.DeleteOneBlog(entity);
            await _manager.SaveAsync();
        }

        // GET-ALL
        public async Task<IEnumerable<Blog>> GetAllBlogsAsync(bool trackChanges)
        {
            return await _manager
                .Blog
                .GetAllBlogsAsync(trackChanges);
        }

        // GET-ONE
        public async Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var blog = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            return blog;
        }

        // PATCH
        public async Task<(BlogDtoForUpdate blodDtoForUpdate, Blog blog)> GetOneBlogForPatchAsync(int id, bool trackChanges)
        {
            //check entity
            var blog = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            var blogDtoForUpdate = _mapper.Map<BlogDtoForUpdate>(blog);

            return (blogDtoForUpdate, blog); // (Tuple)
        }

        // UPDATE
        public async Task UpdateOneBlogAsync(int id, BlogDtoForUpdate blogDto, bool trackChanges)
        {
            //check entity
            var entity = await GetOneBlogByIdAndCheckExist(id, trackChanges);

            //mappig
            entity = _mapper.Map<Blog>(blogDto);

            _manager.Blog.Update(entity);
            await _manager.SaveAsync();
        }

        // SAVE
        public async Task SaveChangesForUpdateAsync(BlogDtoForUpdate blogDtoForUpdate, Blog blog)
        {
            _mapper.Map(blogDtoForUpdate, blog);
            await _manager.SaveAsync();
        }

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
