using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.SharedLookReviewServices;
using Yconic.Domain.Dtos.ReviewDtos;
using Yconic.Domain.Dtos.SharedLookDtos;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SharedLookReviewController:ControllerBase
    {
        private readonly ISharedLookReviewService _sharedLookReviewService;
        public SharedLookReviewController(ISharedLookReviewService sharedLookReviewService)
        {
            _sharedLookReviewService = sharedLookReviewService;
        }

        [HttpGet("GetAllSharedLooksReviews/{sharedLookId}")]
        public async Task<IActionResult> GetAllSharedLooksReviews(Guid sharedLookId)
        {
            var result = await _sharedLookReviewService.GetAllReviewsBySharedLookId(sharedLookId);
            return Ok(result);
        }

        [HttpPost("CreateSharedLookReview")]
        public async Task<IActionResult> CreateSharedLookReview([FromBody] CreateSharedLookReviewDto dto)
        {
            var result = await _sharedLookReviewService.CreateReview(dto);
            return Ok(result);
        }

        [HttpDelete("DeleteSharedLookReview/{id}")]
        public async Task<IActionResult> DeleteSharedLookReview(Guid id)
        {
            var result = await _sharedLookReviewService.DeleteReview(id);
            return Ok(result);
        }
        [HttpPatch("UpdateSharedLookReview")]
        public async Task<IActionResult> UpdateSharedLookReview([FromBody] PatchSharedLookReviewDto dto)
        {
            var result = await _sharedLookReviewService.PatchReview(dto);
            return Ok(result);
        }
    }
}
