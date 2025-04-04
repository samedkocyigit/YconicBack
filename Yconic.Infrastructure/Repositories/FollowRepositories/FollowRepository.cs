using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.FollowRepositories
{
    public class FollowRepository : GenericRepository<Follow>, IFollowRepository
    {
        private readonly AppDbContext _context;

        public FollowRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Follow> GetFollow(Guid followerId, Guid followedId)
        {
            return await _context.Follows.FirstOrDefaultAsync(f =>
                f.FollowerId == followerId && f.FollowedId == followedId);
        }
        public async Task<bool> ExistsAsync(Guid followerId, Guid followedId)
        {
            return await _context.Follows.AnyAsync(f =>
                f.FollowerId == followerId && f.FollowedId == followedId);
        }

        public async Task<List<Follow>> GetFollowers(Guid userId)
        {
            return await _context.Follows
                .Include(f => f.Follower)
                .Where(f => f.FollowedId == userId)
                .ToListAsync();
        }

        public async Task<List<Follow>> GetFollowing(Guid userId)
        {
            return await _context.Follows
                .Include(f => f.Followed)
                .Where(f => f.FollowerId == userId)
                .ToListAsync();
        }
    }
}
