using Microsoft.Extensions.Logging;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.FriendshipRepositories;

namespace Yconic.Application.Services.FriendshipServices
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly ILogger<FriendshipService> _logger;

        public FriendshipService(IFriendshipRepository friendshipRepository, ILogger<FriendshipService> logger)
        {
            _friendshipRepository = friendshipRepository;
            _logger = logger;
        }

        public async Task<Friendship> SendRequest(Guid requesterId, Guid addresseeId)
        {
            var existing = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            if (existing != null)
                throw new Exception("Friend request already exists or you are already friends.");

            var newRequest = new Friendship
            {
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                Status = FriendshipStatus.Pending
            };

            return await _friendshipRepository.SendFriendRequest(newRequest);
        }

        public async Task<Friendship> AcceptRequest(Guid requesterId, Guid addresseeId)
        {
            var friendship = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                throw new Exception("No pending request found.");

            friendship.Status = FriendshipStatus.Accepted;
            return await _friendshipRepository.UpdateFriendship(friendship);
        }

        public async Task DeclineRequest(Guid requesterId, Guid addresseeId)
        {
            var friendship = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                throw new Exception("No pending request found.");

            friendship.Status = FriendshipStatus.Declined;
            await _friendshipRepository.UpdateFriendship(friendship);
        }

        public async Task CancelRequest(Guid requesterId, Guid addresseeId)
        {
            var friendship = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                throw new Exception("No request to cancel.");

            await _friendshipRepository.DeleteFriendship(friendship.Id);
        }

        public async Task RemoveFriend(Guid userId, Guid otherUserId)
        {
            var friendship = await _friendshipRepository.GetFriendship(userId, otherUserId);
            if (friendship == null || friendship.Status != FriendshipStatus.Accepted)
                throw new Exception("You are not friends.");

            await _friendshipRepository.DeleteFriendship(friendship.Id);
        }

        public async Task<bool> HasPendingRequest(Guid requesterId, Guid addresseeId)
        {
            var friendship = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            return friendship != null && friendship.Status == FriendshipStatus.Pending;
        }

        public async Task<bool> CanViewProfile(Guid viewerId, User targetUser)
        {
            if (!targetUser.IsPrivate) return true;

            var friendship = await _friendshipRepository.GetFriendship(viewerId, targetUser.Id);
            return friendship != null && friendship.Status == FriendshipStatus.Accepted;
        }

        public async Task<List<User>> GetFriends(Guid userId)
        {
            var friendships = await _friendshipRepository.GetFriends(userId);
            var friends = friendships.Select(f =>
                f.RequesterId == userId ? f.Addressee : f.Requester
            ).ToList();

            return friends;
        }

        public async Task<List<Friendship>> GetPendingRequests(Guid userId)
        {
            return await _friendshipRepository.GetPendingRequests(userId);
        }
    }
}
