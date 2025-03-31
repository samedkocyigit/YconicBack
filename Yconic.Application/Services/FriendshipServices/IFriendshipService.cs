using Yconic.Domain.Dtos.FriendshipDtos;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.FriendshipServices
{
    public interface IFriendshipService
    {
        Task<ApiResult<FriendshipDto>> SendRequest(Guid requesterId, Guid addresseeId);
        Task<ApiResult<FriendshipDto>> AcceptRequest(Guid requesterId, Guid addresseeId);
        Task DeclineRequest(Guid requesterId, Guid addresseeId);
        Task CancelRequest(Guid requesterId, Guid addresseeId);
        Task RemoveFriend(Guid userId, Guid otherUserId);
        Task<bool> HasPendingRequest(Guid requesterId, Guid addresseeId);
        Task<bool> CanViewProfile(Guid viewerId, User targetUser);
        Task<ApiResult<List<UserDto>>> GetFriends(Guid userId);
        Task<ApiResult<List<FriendshipDto>>> GetPendingRequests(Guid userId);
    }
}
