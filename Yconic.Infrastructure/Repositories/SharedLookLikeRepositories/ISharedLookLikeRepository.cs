using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookLikeRepositories
{
    public interface ISharedLookLikeRepository:IGenericRepository<SharedLookLike>
    {
        Task<IEnumerable<SharedLookLike>> GetSharedLookLikesBySharedLookId(Guid sharedLookId);
        Task<IEnumerable<SharedLookLike>> GetByIdWithUser(Guid userId);
        Task<bool> IsLikeExist(Guid sharedLookId, Guid userId);
        Task<SharedLookLike> GetExistingLike(Guid sharedLookId, Guid userId);
    }
}
