using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all shared look reviews
        [HttpGet]
        [Authorize]
        [Route("get-all-shared-looks-reviews/{sharedLookId}")]
        public async Task<IActionResult> GetAllSharedLooksReviews(Guid sharedLookId, int page=1 ,int pageSize=20)
        {
            var result = await _sharedLookReviewService.GetAllReviewsBySharedLookId(sharedLookId,page,pageSize);
            return Ok(result);
        }

        // create shared look review
        [HttpPost]
        [Authorize]
        [Route("create-shared-look-review")]
        public async Task<IActionResult> CreateSharedLookReview([FromBody] CreateSharedLookReviewDto dto)
        {
            var result = await _sharedLookReviewService.CreateReview(dto);
            return Ok(result);
        }

        //delete shared look review
        [HttpDelete]
        [Authorize]
        [Route("delete-shared-look-review/{id}")]
        public async Task<IActionResult> DeleteSharedLookReview(Guid id)
        {
            var result = await _sharedLookReviewService.DeleteReview(id);
            return Ok(result);
        }

        //update shared look review
        [HttpPatch]
        [Authorize]
        [Route("update-shared-look-review")]
        public async Task<IActionResult> UpdateSharedLookReview([FromBody] PatchSharedLookReviewDto dto)
        {
            var result = await _sharedLookReviewService.PatchReview(dto);
            return Ok(result);
        }
    }
}
