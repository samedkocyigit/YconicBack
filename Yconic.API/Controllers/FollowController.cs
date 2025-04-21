using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // follow
        [HttpPost]
        [Authorize]
        [Route("{followedId}/follow")]
        public async Task<IActionResult> Follow( Guid followedId)
        {
            var followerId = GetUserId();
            var result = await _followService.FollowUser(followerId, followedId);
            return Ok(result);
        }

        // unfollow
        [HttpDelete]
        [Authorize]
        [Route("{followedId}/unfollow")]
        public async Task<IActionResult> Unfollow( Guid followedId)
        {
            var followerId = GetUserId();
            var result = await _followService.UnfollowUser(followerId, followedId);
            return Ok(result);
        }

        // get users followers
        [HttpGet]
        [Authorize]
        [Route("{userId}/get-followers")]
        public async Task<IActionResult> GetFollowers(Guid userId,int page =1 ,int pageSize =100)
        {
            var result = await _followService.GetFollowers(userId,page,pageSize);
            return Ok(result);
        }

        // get users following
        [HttpGet]
        [Authorize]
        [Route("{userId}/get-following")]
        public async Task<IActionResult> GetFollowing(Guid userId,int page=1, int pageSize=100)
        {
            var result = await _followService.GetFollowing(userId,page,pageSize);
            return Ok(result);
        }
    }
}
