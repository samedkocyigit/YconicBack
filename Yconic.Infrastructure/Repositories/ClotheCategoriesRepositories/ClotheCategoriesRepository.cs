using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Microsoft.EntityFrameworkCore;


namespace Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories
{
    public class ClotheCategoriesRepository:GenericRepository<ClotheCategories>,IClotheCategoriesRepository
    {
        protected readonly AppDbContext _context;
        public ClotheCategoriesRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClotheCategories>> GetAllClotheCategories()
        {
           return await _context.ClotheCategories.Include(s=> s.Clothes).ToListAsync();
        }
    }
}
