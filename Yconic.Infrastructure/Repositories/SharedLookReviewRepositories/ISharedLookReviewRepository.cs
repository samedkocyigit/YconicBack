using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookReviewRepositories
{
    public interface ISharedLookReviewRepository: IGenericRepository<SharedLookReview>
    {
        Task<IEnumerable<SharedLookReview>> GetSharedLookReviewsBySharedLookId(Guid sharedLookId);
        Task<IEnumerable<SharedLookReview>> GetByIdWithUser(Guid id);
        Task<IEnumerable<SharedLookReview>> GetUsersReviewsList(Guid userId);
        Task<SharedLookReview> GetById(Guid id);
    }
}
