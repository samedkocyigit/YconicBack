using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.FollowServices;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("{followerId}/follow/{followedId}")]
        public async Task<IActionResult> Follow(Guid followerId, Guid followedId)
        {
            var result = await _followService.FollowUser(followerId, followedId);
            return Ok(result);
        }

        [HttpDelete("{followerId}/unfollow/{followedId}")]
        public async Task<IActionResult> Unfollow(Guid followerId, Guid followedId)
        {
            var result = await _followService.UnfollowUser(followerId, followedId);
            return Ok(result);
        }

        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(Guid userId)
        {
            var result = await _followService.GetFollowers(userId);
            return Ok(result);
        }

        [HttpGet("{userId}/following")]
        public async Task<IActionResult> GetFollowing(Guid userId)
        {
            var result = await _followService.GetFollowing(userId);
            return Ok(result);
        }
    }
}
