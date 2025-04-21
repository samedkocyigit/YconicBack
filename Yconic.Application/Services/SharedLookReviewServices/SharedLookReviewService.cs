using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ReviewDtos;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.SharedLookRepositories;
using Yconic.Infrastructure.Repositories.SharedLookReviewRepositories;

namespace Yconic.Application.Services.SharedLookReviewServices
{
    public class SharedLookReviewService : ISharedLookReviewService
    {
        private readonly ISharedLookReviewRepository _sharedLookReviewRepository;
        private readonly ISharedLookRepository _sharedLookRepository;
        private readonly IMapper _mapper;

        public SharedLookReviewService(ISharedLookReviewRepository sharedLookReviewRepository,ISharedLookRepository sharedLookRepository, IMapper mapper)
        {
            _sharedLookReviewRepository = sharedLookReviewRepository;
            _sharedLookRepository = sharedLookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ReviewDto>>> GetAllReviewsByUserId(Guid userId, int page, int pageSize)
        {
            var reviews = await _sharedLookReviewRepository.GetUsersReviewsList(userId, page, pageSize);
            var mappedReviews = _mapper.Map<List<ReviewDto>>(reviews);
            return ApiResult<List<ReviewDto>>.Success(mappedReviews);
        }
        public async Task<ApiResult<List<ReviewDto>>> GetAllReviewsBySharedLookId(Guid sharedLookId, int page, int pageSize)
        {
            var reviews = await _sharedLookReviewRepository.GetSharedLookReviewsBySharedLookId(sharedLookId, page, pageSize);
            var mappedReviews = _mapper.Map<List<ReviewDto>>(reviews);
            return ApiResult<List<ReviewDto>>.Success(mappedReviews);
        }

        public async Task<ApiResult<ReviewDto>> CreateReview(CreateSharedLookReviewDto dto)
        {
            var sharedLook = await _sharedLookRepository.GetById(dto.ReviewedSharedLookId);
            var review = _mapper.Map<SharedLookReview>(dto);

            var createdReview = await _sharedLookReviewRepository.Add(review);
            
            sharedLook.Reviews.Add(createdReview);
            await _sharedLookRepository.Update(sharedLook);

            var mappedReview = _mapper.Map<ReviewDto>(createdReview);
            return ApiResult<ReviewDto>.Success(mappedReview);
        }

        public async Task<ApiResult<ReviewDto>> PatchReview(PatchSharedLookReviewDto review)
        {
            var existingReview = await _sharedLookReviewRepository.GetById(review.id);
            if (existingReview == null)
            {
                return ApiResult<ReviewDto>.Fail("Review not found");
            }
            existingReview.Review = review.newReview;
            existingReview.UpdatedAt = DateTime.UtcNow;
            var updatedReview = await _sharedLookReviewRepository.Update(existingReview);
            var mappedReview = _mapper.Map<ReviewDto>(updatedReview);
            return ApiResult<ReviewDto>.Success(mappedReview);
        }

        public async Task<ApiResult<bool>> DeleteReview(Guid id)
        {
            try
            {
                var sharedLook = await _sharedLookRepository.GetById(id);
                var review = await _sharedLookReviewRepository.GetById(id);
                if (review == null)
                {
                    return ApiResult<bool>.Fail("Review not found");
                }

                review.IsDeleted = true;
                await _sharedLookReviewRepository.Update(review);
                return ApiResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Fail(ex.Message);
            }
        }
    }
}
