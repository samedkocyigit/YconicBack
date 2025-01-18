using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.GarderobeCategoriesRepositories
{
    public class GarderobeCategoriesRepository:GenericRepository<GarderobeCategories>,IGarderobeCategoriesRepository
    {
        protected readonly AppDbContext _context;
        public GarderobeCategoriesRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
