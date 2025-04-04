using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.FollowRequestRepositories
{
    public interface IFollowRequestRepository : IGenericRepository<FollowRequest>
    {
        Task<bool> ExistsAsync(Guid requesterId, Guid targetUserId);
        Task<FollowRequest?> GetPendingRequest(Guid requesterId, Guid targetUserId);
        Task<List<FollowRequest>> GetPendingRequestsForUser(Guid targetUserId);
    }
}
