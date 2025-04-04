using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{targetUserId}/pending")]
        public async Task<IActionResult> GetPendingRequests(Guid targetUserId)
        {
            var result = await _followRequestService.GetPendingRequests(targetUserId);
            return Ok(result);
        }

        [HttpPost("{targetUserId}/approve/{requesterId}")]
        public async Task<IActionResult> Approve(Guid targetUserId, Guid requesterId)
        {
            var result = await _followRequestService.ApproveRequest(targetUserId, requesterId);
            return Ok(result);
        }

        [HttpPost("{targetUserId}/reject/{requesterId}")]
        public async Task<IActionResult> Reject(Guid targetUserId, Guid requesterId)
        {
            var result = await _followRequestService.RejectRequest(targetUserId, requesterId);
            return Ok(result);
        }

        [HttpDelete("{requesterId}/cancel/{targetUserId}")]
        public async Task<IActionResult> Cancel(Guid requesterId, Guid targetUserId)
        {
            var result = await _followRequestService.CancelRequest(requesterId, targetUserId);
            return Ok(result);
        }
    }
}
