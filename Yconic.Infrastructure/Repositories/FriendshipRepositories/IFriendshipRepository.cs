using Yconic.Domain.Models;

namespace Yconic.Infrastructure.Repositories.FriendshipRepositories
{
    public interface IFriendshipRepository
    {
        Task<Friendship?> GetFriendship(Guid requesterId, Guid addresseeId);
        Task<Friendship> SendFriendRequest(Friendship friendship);
        Task<Friendship> UpdateFriendship(Friendship friendship);
        Task DeleteFriendship(Guid id);
        Task<List<Friendship>> GetFriends(Guid userId);
        Task<List<Friendship>> GetPendingRequests(Guid userId);
    }
}
