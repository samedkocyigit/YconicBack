using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserAccountRepositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        protected readonly AppDbContext _context;

        public UserAccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<UserAccount> GetUserAccountByUserId(Guid userId)
        {
            return _context.UserAccounts
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<UserAccount> GetUserAccountById(Guid id)
        {
            return _context.UserAccounts
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}