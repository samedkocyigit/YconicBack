using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.FollowRepositories
{
    public interface IFollowRepository : IGenericRepository<Follow>
    {
        Task<bool> ExistsAsync(Guid followerId, Guid followedId);
        Task<List<Follow>> GetFollowers(Guid userId);
        Task<List<Follow>> GetFollowing(Guid userId);
        Task<Follow> GetFollow(Guid followerId, Guid followedId);
    }

}
