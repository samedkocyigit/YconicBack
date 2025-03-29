using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Domain.Enums;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.FriendshipRepositories;

namespace Yconic.Infrastructure.Repositories.FriendshipRepositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly AppDbContext _context;

        public FriendshipRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Friendship?> GetFriendship(Guid requesterId, Guid addresseeId)
        {
            return await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.RequesterId == requesterId && f.AddresseeId == addresseeId) ||
                    (f.RequesterId == addresseeId && f.AddresseeId == requesterId));
        }

        public async Task<Friendship> SendFriendRequest(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }

        public async Task<Friendship> UpdateFriendship(Friendship friendship)
        {
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
            return friendship;
        }

        public async Task DeleteFriendship(Guid id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship != null)
            {
                _context.Friendships.Remove(friendship);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Friendship>> GetFriends(Guid userId)
        {
            return await _context.Friendships
                .Include(f => f.Requester)
                .Include(f => f.Addressee)
                .Where(f =>
                    (f.RequesterId == userId || f.AddresseeId == userId) &&
                    f.Status == FriendshipStatus.Accepted)
                .ToListAsync();
        }

        public async Task<List<Friendship>> GetPendingRequests(Guid userId)
        {
            return await _context.Friendships
                .Include(f => f.Requester)
                .Where(f => f.AddresseeId == userId && f.Status == FriendshipStatus.Pending)
                .ToListAsync();
        }
    }
}
