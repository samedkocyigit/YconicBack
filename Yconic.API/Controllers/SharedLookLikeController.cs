using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.SharedLookLikeServices;
using Yconic.Domain.Dtos.LikeDtos;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SharedLookLikeController:ControllerBase
    {
        private readonly ISharedLookLikeService _sharedLookLikeService;
        public SharedLookLikeController(ISharedLookLikeService sharedLookLikeService)
        {
            _sharedLookLikeService = sharedLookLikeService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all shared look likes
        [HttpGet]
        [Authorize]
        [Route("get-all-shared-looks-likes/{sharedLookId}")]
        public async Task<IActionResult> GetAllSharedLooksLikes(Guid sharedLookId,int page=1,int pageSize=100)
        {
            var result = await _sharedLookLikeService.GetSharedLookLikesListBySharedLookId(sharedLookId, page,pageSize);
            return Ok(result);
        }

        // like shared look
        [HttpPost]
        [Authorize]
        [Route("like-shared-look/{sharedLookId}")]
        public async Task<IActionResult> GetSharedLooksLikesByUserId(Guid sharedLookId)
        {
            var userId = GetUserId();
            var result = await _sharedLookLikeService.LikeSharedLook(sharedLookId,userId);
            return Ok(result);
        }

        // unlike shared look
        [HttpDelete]
        [Authorize]
        [Route("unlike-shared-look/{id}")]
        public async Task<IActionResult> UnlikeSharedLook(Guid id)
        {
            var result = await _sharedLookLikeService.UnlikeSharedLook(id);
            return Ok(result);
        }
    }
}
