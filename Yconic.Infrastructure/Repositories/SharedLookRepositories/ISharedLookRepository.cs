using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookRepositories
{
    public interface ISharedLookRepository:IGenericRepository<SharedLook>
    {
        Task<IEnumerable<SharedLook>> GetAllListsPublicUsers();
        Task<IEnumerable<SharedLook>> GetSharedLooksByUserId(Guid userId,int page,int pageSize);
        Task<IEnumerable<SharedLook>> GetSharedLooksUserWhoFollowedPaginated(Guid userId, int page, int pageSize);
        Task<SharedLook> GetById(Guid id);
    }
}
