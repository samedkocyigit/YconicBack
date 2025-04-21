using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.FollowRequestServices;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowRequestController : ControllerBase
    {
        private readonly IFollowRequestService _followRequestService;

        public FollowRequestController(IFollowRequestService followRequestService)
        {
            _followRequestService = followRequestService;
        }

        Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // send follow request
        [HttpPost]
        [Authorize]
        [Route("{targetUserId}/send")]
        public async Task<IActionResult> SendFollowRequest(Guid targetUserId)
        {
            var requesterId = GetUserId();
            var response = await _followRequestService.SendRequest(requesterId,targetUserId);
            return Ok(response);
        }

        // get all pending requests
        [HttpGet]
        [Authorize]
        [Route("{targetUserId}/pending")]
        public async Task<IActionResult> GetPendingRequests(Guid targetUserId)
        {
            var result = await _followRequestService.GetPendingRequests(targetUserId);
            return Ok(result);
        }

        // accept follow request
        [HttpPost]
        [Authorize]
        [Route("{requesterId}/approve")]
        public async Task<IActionResult> Approve(Guid requesterId)
        {
            var currentUserId = GetUserId();
            var result = await _followRequestService.ApproveRequest(currentUserId, requesterId);
            return Ok(result);
        }

        // reject follow request
        [HttpPost]
        [Authorize]
        [Route("{requesterId}/reject")]
        public async Task<IActionResult> Reject( Guid requesterId)
        {
            var targetUserId = GetUserId();
            var result = await _followRequestService.RejectRequest(targetUserId, requesterId);
            return Ok(result);
        }

        // cancel follow request
        [HttpDelete]
        [Authorize]
        [Route("{requesterId}/cancel")]
        public async Task<IActionResult> Cancel(Guid requesterId)
        {
            var currentUserId = GetUserId();
            var result = await _followRequestService.CancelRequest(requesterId, currentUserId);
            return Ok(result);
        }
    }
}
