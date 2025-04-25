using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace Yconic.Infrastructure.Repositories.ClotheCategoryRepositories
{
    public class ClotheCategoryRepository : GenericRepository<ClotheCategory>, IClotheCategoryRepository
    {
        protected readonly AppDbContext _context;

        public ClotheCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClotheCategory>> GetAllClotheCategories()
        {
            return await _context.ClotheCategories.Include(s => s.Clothes).ToListAsync();
        }

        public async Task<ClotheCategory> GetClotheCategoryById(Guid id)
        {
            return await _context.ClotheCategories.Include(s => s.Clothes).ThenInclude(s => s.Photos).FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}