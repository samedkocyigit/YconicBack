using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookReviewRepositories
{
    public class SharedLookReviewRepository: GenericRepository<SharedLookReview>,ISharedLookReviewRepository
    {
        private readonly AppDbContext _context;
        public SharedLookReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SharedLookReview>> GetSharedLookReviewsBySharedLookId(Guid sharedLookId)
        {
            return await _context.SharedLookReviews
                .Include(x => x.ReviewerUser)
                .Where(x => x.ReviewedSharedLookId == sharedLookId && x.IsDeleted ==false)
                .ToListAsync();
        }
        public async Task<IEnumerable<SharedLookReview>> GetByIdWithUser(Guid id)
        {
            return await _context.SharedLookReviews
                .Include(x => x.ReviewerUser)
                .Where(x => x.Id == id)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<SharedLookReview>> GetUsersReviewsList(Guid userId)
        {
            return await _context.SharedLookReviews.Include(r => r.ReviewerUser).Where(r => r.ReviewerUserId == userId).ToListAsync();
        }
        public async Task<SharedLookReview> GetById(Guid id)
        {
            return await _context.SharedLookReviews
                .Where(x => x.Id == id && x.IsDeleted ==false)
                .Include(x => x.ReviewerUser)
                .FirstOrDefaultAsync();
        }
    }
}
