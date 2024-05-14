using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ReviewManager : IReviewService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ReviewManager(IRepositoryManager mananger, ILoggerService logger, IMapper mapper)
        {
            _manager = mananger;
            _logger = logger;
            _mapper = mapper;
        }

        // Create
        public async Task<ReviewDto> CreateOneReviewAsync(ReviewDtoForInsertion reviewDto)
        {
            var entity = _mapper.Map<Review>(reviewDto);
            _manager.Review.CreateOneReview(entity);
            await _manager.SaveAsync();

            return _mapper.Map<ReviewDto>(entity);
        }

        // Delete
        public async Task DeleteOneReviewAsync(int id, bool trackChanges)
        {
            //check entity
            var review = await GetOneReviewByIdAndCheckExist(id, trackChanges);

            _manager.Review.DeleteOneReview(review);
            await _manager.SaveAsync();
        }

        // Get-All
        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync(ReviewParameters reviewParameters, bool trackChanges)
        {
            var reviewsWithParams = await _manager
                .Review
                .GetAllReviewsAsync(reviewParameters, trackChanges);

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviewsWithParams);

            return reviewsDto;
        }

        // Get-One
        public async Task<ReviewDto> GetOneReviewByIdAsync(int id, bool trackChanges)
        {
            //check entity
            var review = await GetOneReviewByIdAndCheckExist(id, trackChanges);

            var reviewDto = _mapper.Map<ReviewDto>(review);

            return reviewDto;
        }

        // Update
        public async Task UpdateOneReviewAsync(int id, ReviewDtoForUpdate reviewDto, bool trackChanges)
        {
            //check entity
            var review = await GetOneReviewByIdAndCheckExist(id, trackChanges);

            //mapping
            review = _mapper.Map<Review>(reviewDto);

            _manager.Review.UpdateOneReview(review);
            await _manager.SaveAsync();
        }

        // Patch-Content
        public async Task UpdateOneReviewContentAsync(int id, string content, bool trackChanges)
        {
            //check entity
            var review = await GetOneReviewByIdAndCheckExist(id, trackChanges);

            review.Content = content;

            _manager.Review.Update(review);
            await _manager.SaveAsync();
        }

        // Check
        private async Task<Review> GetOneReviewByIdAndCheckExist(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager
                .Review
                .GetOneReviewByIdAsync(id, trackChanges);

            if (entity is null)
                throw new ReviewNotFoundException(id);

            return entity;
        }
    }
}
