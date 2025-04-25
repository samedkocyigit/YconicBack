using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookLikeRepositories
{
    public class SharedLookLikeRepository : GenericRepository<SharedLookLike>, ISharedLookLikeRepository
    {
        private readonly AppDbContext _context;

        public SharedLookLikeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SharedLookLike>> GetSharedLookLikesBySharedLookId(Guid sharedLookId, int page, int pageSize)
        {
            return await _context.SharedLookLikes
                .Include(x => x.LikedUser)
                    .ThenInclude(x => x.UserAccount)
                .Where(x => x.LikedSharedLookId == sharedLookId && x.IsLiked == true)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<SharedLookLike>> GetByIdWithUser(Guid userId)
        {
            return await _context.SharedLookLikes
                .Include(x => x.LikedUser)
                .Where(x => x.LikedUserId == userId)
                .ToListAsync();
        }

        public async Task<bool> IsLikeExist(Guid sharedLookId, Guid userId)
        {
            return await _context.SharedLookLikes
                .AnyAsync(x => x.LikedSharedLookId == sharedLookId && x.LikedUserId == userId);
        }

        public async Task<SharedLookLike> GetExistingLike(Guid sharedLookId, Guid userId)
        {
            return await _context.SharedLookLikes
                .FirstOrDefaultAsync(x => x.LikedSharedLookId == sharedLookId && x.LikedUserId == userId);
        }
    }
}