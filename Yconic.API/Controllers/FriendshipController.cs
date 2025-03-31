using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.FriendshipServices;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IUserRepository _userRepository;

        public FriendshipController(IFriendshipService friendshipService , IUserRepository userRepository)
        {
            _friendshipService = friendshipService;
            _userRepository = userRepository;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequest(Guid requesterId, Guid addresseeId)
        {
            var result = await _friendshipService.SendRequest(requesterId, addresseeId);
            return Ok(result);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriendRequest(Guid requesterId, Guid addresseeId)
        {
            var result = await _friendshipService.AcceptRequest(requesterId, addresseeId);
            return Ok(result);
        }

        [HttpPost("decline")]
        public async Task<IActionResult> DeclineFriendRequest(Guid requesterId, Guid addresseeId)
        {
            await _friendshipService.DeclineRequest(requesterId, addresseeId);
            return Ok(ApiResult<string>.Success("Request declined"));
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelFriendRequest(Guid requesterId, Guid addresseeId)
        {
            await _friendshipService.CancelRequest(requesterId, addresseeId);
            return Ok(ApiResult<string>.Success("Request canceled"));
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriend(Guid userId, Guid otherUserId)
        {
            await _friendshipService.RemoveFriend(userId, otherUserId);
            return Ok(ApiResult<string>.Success("Friend removed"));
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends(Guid userId)
        {
            var friends = await _friendshipService.GetFriends(userId);
            return Ok(friends);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests(Guid userId)
        {
            var requests = await _friendshipService.GetPendingRequests(userId);
            return Ok(requests);
        }

        [HttpGet("can-view-profile")]
        public async Task<IActionResult> CanViewProfile(Guid viewerId, Guid targetUserId)
        {
            var targetUser = await _userRepository.GetUserById(targetUserId);
            if (targetUser == null)
                return NotFound(ApiResult<string>.Fail("Target user not found"));

            var result = await _friendshipService.CanViewProfile(viewerId, targetUser);
            return Ok(result);
        }

    }
}
