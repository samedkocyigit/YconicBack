using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserPersonalRepositories
{
    public class UserPersonalRepository : GenericRepository<UserPersonal>, IUserPersonalRepository
    {
        protected readonly AppDbContext _context;

        public UserPersonalRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserPersonal> GetUserPersonalByUserId(Guid userId)
        {
            return await _context.UserPersonals
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}