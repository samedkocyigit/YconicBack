using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users.Include(x => x.UserGarderobe).ThenInclude(x => x.ClothesCategory).ThenInclude(x => x.Clothes).ThenInclude(x=>x.Photos).Include(x => x.UserPersona).ToListAsync();
        }
    }
}
