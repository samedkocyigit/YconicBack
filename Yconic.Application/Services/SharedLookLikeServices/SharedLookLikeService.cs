using AutoMapper;
using Yconic.Domain.Dtos.LikesDtos;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;
using Yconic.Infrastructure.Repositories.SharedLookLikeRepositories;
using Yconic.Infrastructure.Repositories.SharedLookRepositories;

namespace Yconic.Application.Services.SharedLookLikeServices
{
    public class SharedLookLikeService : ISharedLookLikeService
    {
        private readonly ISharedLookLikeRepository _sharedLookLikeRepo;
        private readonly ISharedLookRepository _sharedLookRepo;
        private readonly IFollowRepository _followRepo;
        private readonly IFollowRequestRepository _followRequestRepo;
        private readonly IMapper _mapper;

        public SharedLookLikeService(ISharedLookLikeRepository sharedLookLikeRepo, ISharedLookRepository sharedLookRepo, IFollowRepository followRepo, IFollowRequestRepository followRequestRepo, IMapper mapper)
        {
            _sharedLookLikeRepo = sharedLookLikeRepo;
            _sharedLookRepo = sharedLookRepo;
            _followRepo = followRepo;
            _followRequestRepo = followRequestRepo;
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> LikeSharedLook(Guid sharedLookId, Guid userId)
        {
            var sharedLook = await _sharedLookRepo.GetById(sharedLookId);

            if (sharedLook == null)
            {
                return ApiResult<bool>.Fail("Shared look not found");
            }

            var isExist = await _sharedLookLikeRepo.IsLikeExist(sharedLookId, userId);
            if (isExist)
            {
                var existingLike = await _sharedLookLikeRepo.GetExistingLike(sharedLookId, userId);
                existingLike.UpdatedAt = DateTime.UtcNow;
                existingLike.IsLiked = true;
                await _sharedLookLikeRepo.Update(existingLike);
                return ApiResult<bool>.Success(true);
            }

            var like = new SharedLookLike
            {
                LikedSharedLookId = sharedLookId,
                LikedUserId = userId,
            };

            var createdLike = await _sharedLookLikeRepo.Add(like);
            sharedLook.Likes.Add(createdLike);

            await _sharedLookRepo.Update(sharedLook);

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<bool>> UnlikeSharedLook(Guid likeId)
        {
            var like = await _sharedLookLikeRepo.GetById(likeId);
            if (like == null)
            {
                return ApiResult<bool>.Fail("Like not found");
            }
            var sharedLook = await _sharedLookRepo.GetById(like.LikedUserId);

            like.UpdatedAt = DateTime.UtcNow;
            like.IsLiked = false;
            await _sharedLookLikeRepo.Update(like);

            Console.WriteLine($"update oldu canım");

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<UserMiniDto>>> GetSharedLookLikesListBySharedLookId(Guid sharedLookId, Guid authUserId, int page, int pageSize)
        {
            var likes = await _sharedLookLikeRepo.GetSharedLookLikesBySharedLookId(sharedLookId, page, pageSize);
            var likedUsers = likes.Select(l => l.LikedUser!).ToList();
            var targetUserIds = likedUsers.Select(u => u.Id).ToList();

            var relations = await _followRepo.GetFollowRelationsAsync(authUserId, targetUserIds);

            // fetch auth user’s follow requests
            var followRequests = await _followRequestRepo.GetRequestsSentByUserAsync(authUserId, targetUserIds);
            var userMiniDtos = likedUsers.Select(user =>
            {
                var dto = _mapper.Map<UserMiniDto>(user);

                dto.isFollower = relations.Any(f => f.FollowerId == user.Id && f.FollowedId == authUserId && f.IsFollowing);
                dto.isFollowing = relations.Any(f => f.FollowerId == authUserId && f.FollowedId == user.Id && f.IsFollowing);
                dto.isRequested = followRequests.Any(r => r.TargetUserId == user.Id);

                return dto;
            }).ToList();

            return ApiResult<List<UserMiniDto>>.Success(userMiniDtos);
        }

        //public async Task<ApiResult<List<Guid>>> GetLikesByUserId(Guid userId)
        //{
        //    var likes = await _sharedLookLikeRepository.(userId);
        //    var mappedLikes = _mapper.Map<List<SharedLookLikeDto>>(likes);
        //    return ApiResult<List<SharedLookLikeDto>>.Success(mappedLikes);
        //}
    }
}