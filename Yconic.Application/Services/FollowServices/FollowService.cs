using AutoMapper;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.FollowServices
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepo;
        private readonly IFollowRequestRepository _followRequestRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public FollowService(IFollowRepository followRepo, IFollowRequestRepository followRequestRepo, IUserRepository userRepo, IMapper mapper)
        {
            _followRepo = followRepo;
            _followRequestRepo = followRequestRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ApiResult<string>> FollowUser(Guid followerId, Guid followedId)
        {
            if (followerId == followedId)
                return ApiResult<string>.Fail("Cannot follow yourself.");

            if (await _followRepo.ExistsAsync(followerId, followedId))
            {
                var existing = await _followRepo.GetFollow(followerId, followedId);
                existing.IsFollowing = true;
                existing.UpdatedAt = DateTime.UtcNow;
                await _followRepo.Update(existing);
                return ApiResult<string>.Success("Followed successfully.");
            }

            var follower = await _userRepo.GetUserById(followerId);
            var followed = await _userRepo.GetUserById(followedId);

            if (follower == null || followed == null)
                return ApiResult<string>.Fail("User not found.");

            var follow = await _followRepo.Add(new Follow
            {
                FollowerId = follower.Id,
                FollowedId = followed.Id
            });

            follower.Following.Add(follow);
            followed.Followers.Add(follow);

            await _userRepo.Update(follower);
            await _userRepo.Update(followed);
            return ApiResult<string>.Success("Followed successfully.");
        }

        public async Task<ApiResult<string>> UnfollowUser(Guid followerId, Guid followedId)
        {
            var existing = await _followRepo
                .GetFollow(followerId, followedId);

            if (existing == null)
                return ApiResult<string>.Fail("Not following.");

            existing.IsFollowing = false;
            existing.UpdatedAt = DateTime.UtcNow;
            await _followRepo.Update(existing);

            var follower = await _userRepo.GetUserById(followerId);
            var followed = await _userRepo.GetUserById(followedId);

            follower.Following.Remove(existing);
            followed.Followers.Remove(existing);

            await _userRepo.Update(follower);
            await _userRepo.Update(followed);

            return ApiResult<string>.Success("Unfollowed successfully.");
        }

        public async Task<ApiResult<List<UserMiniDto>>> GetFollowers(Guid userId, Guid authUserId, int page, int pageSize)
        {
            // fetch followers
            var list = await _followRepo.GetFollowers(userId, page, pageSize); // returns List<Follow> (Follower navigasyon dolu olmalı)
            var followerUsers = list.Select(f => f.Follower!).ToList(); // Follower navigation kullanılıyor
            var targetUserIds = followerUsers.Select(u => u.Id).ToList();

            // fetchRelations (auth user <-> followers)
            var relations = await _followRepo.GetFollowRelationsAsync(authUserId, targetUserIds);

            // fetch auth user’s follow requests
            var followRequests = await _followRequestRepo.GetRequestsSentByUserAsync(authUserId, targetUserIds);

            //mapping + enrich
            var userMiniDtos = followerUsers.Select(user =>
            {
                var dto = _mapper.Map<UserMiniDto>(user);

                dto.isFollower = relations.Any(f => f.FollowerId == user.Id && f.FollowedId == authUserId && f.IsFollowing);
                dto.isFollowing = relations.Any(f => f.FollowerId == authUserId && f.FollowedId == user.Id && f.IsFollowing);
                dto.isRequested = followRequests.Any(r => r.TargetUserId == user.Id);

                return dto;
            }).ToList();

            return ApiResult<List<UserMiniDto>>.Success(userMiniDtos);
        }

        public async Task<ApiResult<List<UserMiniDto>>> GetFollowing(Guid userId, Guid authUserId, int page, int pageSize)
        {
            // fetch followers
            var list = await _followRepo.GetFollowing(userId, page, pageSize); // returns List<Follow> (Follower navigasyon dolu olmalı)
            var followerUsers = list.Select(f => f.Follower!).ToList(); // Follower navigation kullanılıyor
            var targetUserIds = followerUsers.Select(u => u.Id).ToList();

            // fetchRelations (auth user <-> followers)
            var relations = await _followRepo.GetFollowRelationsAsync(authUserId, targetUserIds);

            // fetch auth user’s follow requests
            var followRequests = await _followRequestRepo.GetRequestsSentByUserAsync(authUserId, targetUserIds);

            //mapping + enrich
            var userMiniDtos = followerUsers.Select(user =>
            {
                var dto = _mapper.Map<UserMiniDto>(user);

                dto.isFollower = relations.Any(f => f.FollowerId == user.Id && f.FollowedId == authUserId && f.IsFollowing);
                dto.isFollowing = relations.Any(f => f.FollowerId == authUserId && f.FollowedId == user.Id && f.IsFollowing);
                dto.isRequested = followRequests.Any(r => r.TargetUserId == user.Id);

                return dto;
            }).ToList();

            return ApiResult<List<UserMiniDto>>.Success(userMiniDtos);
        }
    }
}