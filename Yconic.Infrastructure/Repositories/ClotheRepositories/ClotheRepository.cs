using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.ClotheRepositories
{
    public class ClotheRepository:GenericRepository<Clothe> ,IClotheRepository
    {
        protected readonly AppDbContext _context;
        
        public ClotheRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Clothe>> GetAllClothes()
        {
            return await _context.Clothes.Include(s=> s.Photos).ToListAsync();
        }
        public async Task<Clothe> GetClotheById(Guid id)
        {
            return await _context.Clothes.Include(s => s.Photos).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
