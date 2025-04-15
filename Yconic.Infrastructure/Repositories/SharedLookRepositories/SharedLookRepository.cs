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
        public async Task<IEnumerable<SharedLook>> GetAllList()
        {
            return await _context.SharedLooks
                .Include(sl => sl.Suggestion)
                    .ThenInclude(s => s.SuggestedLook)
                .Include(sl=> sl.User)
                .Include(sl => sl.Likes)
                    .ThenInclude(l => l.LikedUser)
                .Include(sl => sl.Reviews)
                    .ThenInclude(r => r.ReviewerUser)
                .ToListAsync();
        }
        public async Task<IEnumerable<SharedLook>> GetSharedLooksByUserId(Guid userId)
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
