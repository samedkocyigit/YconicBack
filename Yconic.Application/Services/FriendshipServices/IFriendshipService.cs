using Yconic.Domain.Models;

namespace Yconic.Application.Services.FriendshipServices
{
    public interface IFriendshipService
    {
        Task<Friendship> SendRequest(Guid requesterId, Guid addresseeId);
        Task<Friendship> AcceptRequest(Guid requesterId, Guid addresseeId);
        Task DeclineRequest(Guid requesterId, Guid addresseeId);
        Task CancelRequest(Guid requesterId, Guid addresseeId);
        Task RemoveFriend(Guid userId, Guid otherUserId);
        Task<bool> HasPendingRequest(Guid requesterId, Guid addresseeId);
        Task<bool> CanViewProfile(Guid viewerId, User targetUser);
        Task<List<User>> GetFriends(Guid userId);
        Task<List<Friendship>> GetPendingRequests(Guid userId);
    }
}
