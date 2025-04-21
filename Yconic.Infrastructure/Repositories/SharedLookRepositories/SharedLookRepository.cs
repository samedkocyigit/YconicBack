using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SharedLookRepositories
{
    public class SharedLookRepository: GenericRepository<SharedLook>, ISharedLookRepository
    {
        private readonly AppDbContext _context;
        public SharedLookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SharedLook>> GetAllListsPublicUsers()
        {
            return await _context.SharedLooks
                .Include(sl => sl.Suggestion)
                    .ThenInclude(s => s.SuggestedLook)
                .Include(sl=> sl.User)
                    
                .Include(sl => sl.Likes)
                    .ThenInclude(l => l.LikedUser)
                .Include(sl => sl.Reviews)
                    .ThenInclude(r => r.ReviewerUser)
                .Where(sl=> sl.User.IsPrivate == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<SharedLook>> GetSharedLooksUserWhoFollowedPaginated(Guid userId, int page, int pageSize)
        {
            var followedUserIds = await _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FollowedId)
                .ToListAsync();

            return await _context.SharedLooks
                .Include(sl => sl.Suggestion).ThenInclude(s => s.SuggestedLook)
                .Include(sl => sl.User)
                .Include(sl => sl.Likes).ThenInclude(l => l.LikedUser)
                .Include(sl => sl.Reviews).ThenInclude(r => r.ReviewerUser)
                .Where(sl => followedUserIds.Contains(sl.UserId))
                .OrderByDescending(sl => sl.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<SharedLook>> GetSharedLooksByUserId(Guid userId, int page, int pageSize)
        {
            return await _context.SharedLooks
                .Include(sl => sl.Suggestion)
                    .ThenInclude(s => s.SuggestedLook)
                .Include(sl => sl.User)
                .Include(sl => sl.Likes)
                    .ThenInclude(l => l.LikedUser)
                .Include(sl => sl.Reviews)
                    .ThenInclude(r => r.ReviewerUser)
                .Where(sl => sl.UserId == userId)
                .OrderByDescending(sl => sl.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<SharedLook> GetById(Guid id)
        {
            return await _context.SharedLooks
                .Include(sl => sl.Suggestion)
                    .ThenInclude(s => s.SuggestedLook)
                .Include(sl => sl.User)
                .Include(sl => sl.Likes)
                    .ThenInclude(l => l.LikedUser)
                .Include(sl => sl.Reviews)
                    .ThenInclude(r => r.ReviewerUser)
                .FirstOrDefaultAsync(sl => sl.Id == id);
        }
    }
}
