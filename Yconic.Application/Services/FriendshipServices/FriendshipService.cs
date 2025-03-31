using AutoMapper;
using Microsoft.Extensions.Logging;
using Yconic.Domain.Dtos.FriendshipDtos;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FriendshipRepositories;

namespace Yconic.Application.Services.FriendshipServices
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly ILogger<FriendshipService> _logger;
        private readonly IMapper _mapper;

        public FriendshipService(IFriendshipRepository friendshipRepository, ILogger<FriendshipService> logger,IMapper mapper)
        {
            _friendshipRepository = friendshipRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResult<FriendshipDto>> SendRequest(Guid requesterId, Guid addresseeId)
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

            var resp =  await _friendshipRepository.SendFriendRequest(newRequest);
            var mapped = _mapper.Map<FriendshipDto>(resp);
            return ApiResult<FriendshipDto>.Success(mapped);
        }

        public async Task<ApiResult<FriendshipDto>> AcceptRequest(Guid requesterId, Guid addresseeId)
        {
            var friendship = await _friendshipRepository.GetFriendship(requesterId, addresseeId);
            if (friendship == null || friendship.Status != FriendshipStatus.Pending)
                throw new Exception("No pending request found.");

            friendship.Status = FriendshipStatus.Accepted;
            var res = await _friendshipRepository.UpdateFriendship(friendship);
            var mapped = _mapper.Map<FriendshipDto>(res);
            return ApiResult<FriendshipDto>.Success(mapped);
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

        public async Task<ApiResult<List<UserDto>>> GetFriends(Guid userId)
        {
            var friendships = await _friendshipRepository.GetFriends(userId);
            var friends = friendships.Select(f =>
                f.RequesterId == userId ? f.Addressee : f.Requester
            ).ToList();
            var mapped = _mapper.Map<List<UserDto>>(friends);
            return ApiResult<List<UserDto>>.Success(mapped);
        }

        public async Task<ApiResult<List<FriendshipDto>>> GetPendingRequests(Guid userId)
        {
            var frienship = await _friendshipRepository.GetPendingRequests(userId);
            var mapped = _mapper.Map<List<FriendshipDto>>(frienship);
            return ApiResult<List<FriendshipDto>>.Success(mapped);
        }
    }
}
