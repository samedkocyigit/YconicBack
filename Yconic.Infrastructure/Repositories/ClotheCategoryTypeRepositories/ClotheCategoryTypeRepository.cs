using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.ClotheCategoryTypeRepositories
{
    public class ClotheCategoryTypeRepository : GenericRepository<ClotheCategoryType>, IClotheCategoryTypeRepository
    {
        protected readonly AppDbContext _context;

        public ClotheCategoryTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}