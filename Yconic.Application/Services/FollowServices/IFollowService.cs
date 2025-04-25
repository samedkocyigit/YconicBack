using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.FollowServices
{
    public interface IFollowService
    {
        Task<ApiResult<string>> FollowUser(Guid followerId, Guid followedId);

        Task<ApiResult<string>> UnfollowUser(Guid followerId, Guid followedId);

        Task<ApiResult<List<UserMiniDto>>> GetFollowers(Guid userId, Guid authUser, int page, int pageSize);

        Task<ApiResult<List<UserMiniDto>>> GetFollowing(Guid userId, Guid authUser, int page, int pageSize);
    }
}