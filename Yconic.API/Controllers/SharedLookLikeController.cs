using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetAllSharedLooksLikes/{sharedLookId}")]
        public async Task<IActionResult> GetAllSharedLooksLikes(Guid sharedLookId)
        {
            var result = await _sharedLookLikeService.GetSharedLookLikesListBySharedLookId(sharedLookId);
            return Ok(result);
        }

        [HttpPost("LikeSharedLook")]
        public async Task<IActionResult> GetSharedLooksLikesByUserId(CreateSharedLookLikeDto dto)
        {
            var result = await _sharedLookLikeService.LikeSharedLook(dto);
            return Ok(result);
        }

        [HttpDelete("UnlikeSharedLook/{id}")]
        public async Task<IActionResult> UnlikeSharedLook(Guid id)
        {
            var result = await _sharedLookLikeService.UnlikeSharedLook(id);
            return Ok(result);
        }
    }
}
