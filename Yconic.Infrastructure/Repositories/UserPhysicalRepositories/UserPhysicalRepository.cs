using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models.UserModels;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.UserPhysicalRepositories
{
    public class UserPhysicalRepository : GenericRepository<UserPhysical>, IUserPhysicalRepository
    {
        protected readonly AppDbContext _context;

        public UserPhysicalRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}