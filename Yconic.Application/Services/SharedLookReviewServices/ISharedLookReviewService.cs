using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ReviewDtos;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.SharedLookReviewServices
{
    public interface ISharedLookReviewService
    {
        Task<ApiResult<List<ReviewDto>>> GetAllReviewsByUserId(Guid userId, int page, int pageSize);
        Task<ApiResult<List<ReviewDto>>> GetAllReviewsBySharedLookId(Guid sharedLookId, int page, int pageSize);
        Task<ApiResult<ReviewDto>> CreateReview(CreateSharedLookReviewDto dto);
        Task<ApiResult<ReviewDto>> PatchReview(PatchSharedLookReviewDto review);
        Task<ApiResult<bool>> DeleteReview(Guid id);
    }
}
