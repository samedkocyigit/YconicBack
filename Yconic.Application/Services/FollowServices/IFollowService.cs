using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.FollowServices
{
    public interface IFollowService
    {
        Task<ApiResult<string>> FollowUser(Guid followerId, Guid followedId);
        Task<ApiResult<string>> UnfollowUser(Guid followerId, Guid followedId);
        Task<ApiResult<List<Guid>>> GetFollowers(Guid userId, int page, int pageSize);
        Task<ApiResult<List<Guid>>> GetFollowing(Guid userId, int page, int pageSize);
    }
}
