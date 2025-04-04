using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.FollowRequestServices
{
    public interface IFollowRequestService
    {
        Task<ApiResult<string>> SendRequest(Guid requesterId, Guid targetUserId);
        Task<ApiResult<List<FollowRequest>>> GetPendingRequests(Guid targetUserId);
        Task<ApiResult<string>> ApproveRequest(Guid targetUserId, Guid requesterId);
        Task<ApiResult<string>> RejectRequest(Guid targetUserId, Guid requesterId);
        Task<ApiResult<string>> CancelRequest(Guid requesterId, Guid targetUserId);
    }
}
