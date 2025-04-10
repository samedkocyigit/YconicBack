using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        protected readonly AppDbContext _context;
        public UserRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                    .Include(x=> x.UserGarderobe)
                        .ThenInclude(x=> x.ClothesCategory)
                            .ThenInclude(x=> x.Clothes)
                                .ThenInclude(x=> x.Photos)
                    .Include(x=>x.UserPersona)
                    .Include(x=>x.Suggestions)
                        .ThenInclude(x=>x.SuggestedLook)
                            .ThenInclude(x=>x.Photos)
                    .Include(x => x.Followers)
                        .ThenInclude(x => x.Follower)
                    .Include(x => x.Following)
                        .ThenInclude(x => x.Followed)
                    .Include(x=> x.FollowRequestsReceived)
                        .ThenInclude(x => x.Requester)
                    .Include(x=> x.FollowRequestsSent)
                        .ThenInclude(x => x.TargetUser)
                    .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users
                    .Include(x => x.UserGarderobe)
                        .ThenInclude(x => x.ClothesCategory)
                            .ThenInclude(x => x.Clothes)
                                .ThenInclude(x=>x.Photos)
                    .Include(x => x.UserPersona)
                    .Include(x=> x.Suggestions)
                        .ThenInclude(x => x.SuggestedLook)
                            .ThenInclude(x=>x.Photos)
                    .Include(x => x.Followers)
                    .Include(x => x.Following)
                    .Include(x=> x.FollowRequestsReceived)
                    .Include(x=> x.FollowRequestsSent)
                    .ToListAsync();
        }
        
        public async Task<User> GetUserById(Guid id)
        {
           return await _context.Users
                    .Include(x => x.UserGarderobe)
                       .ThenInclude(x => x.ClothesCategory)
                           .ThenInclude(x => x.Clothes)
                               .ThenInclude(x=>x.Photos)
                    .Include(x => x.UserPersona)
                    .Include(x=> x.Suggestions)
                       .ThenInclude(x=> x.SuggestedLook)
                               .ThenInclude(x=>x.Photos)
                    .Include(x => x.Followers)
                        .ThenInclude(x => x.Follower)
                    .Include(x => x.Following)
                        .ThenInclude(x => x.Followed)
                    .Include(x=> x.FollowRequestsReceived)
                        .ThenInclude(x => x.Requester)
                    .Include(x=> x.FollowRequestsSent)
                        .ThenInclude(x => x.TargetUser)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

