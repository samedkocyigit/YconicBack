using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.FollowServices
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepo;
        private readonly IUserRepository _userRepo;

        public FollowService(IFollowRepository followRepo, IUserRepository userRepo)
        {
            _followRepo = followRepo;
            _userRepo = userRepo;
        }

        public async Task<ApiResult<string>> FollowUser(Guid followerId, Guid followedId)
        {
            if (followerId == followedId)
                return ApiResult<string>.Fail("Cannot follow yourself.");

            if (await _followRepo.ExistsAsync(followerId, followedId))
                return ApiResult<string>.Fail("Already following.");

            var follower = await _userRepo.GetUserById(followerId);
            var followed = await _userRepo.GetUserById(followedId);

            if (follower == null || followed == null)
                return ApiResult<string>.Fail("User not found.");

           var follow =  await _followRepo.Add(new Follow
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
                .GetFollow(followerId ,followedId);

            if (existing == null)
                return ApiResult<string>.Fail("Not following.");

            await _followRepo.Delete(existing.Id);
            return ApiResult<string>.Success("Unfollowed successfully.");
        }

        public async Task<ApiResult<List<Guid>>> GetFollowers(Guid userId)
        {
            var list = await _followRepo.GetFollowers(userId);
            var ids = list.Select(f => f.FollowerId).ToList();
            return ApiResult<List<Guid>>.Success(ids);
        }

        public async Task<ApiResult<List<Guid>>> GetFollowing(Guid userId)
        {
            var list = await _followRepo.GetFollowing(userId);
            var ids = list.Select(f => f.FollowedId).ToList();
            return ApiResult<List<Guid>>.Success(ids);
        }
    }
}
