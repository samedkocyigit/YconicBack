using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.ClothePhotoRepositories
{
    public class ClothePhotoRepository:GenericRepository<ClothePhoto>, IClothePhotoRepository
    {
        protected readonly AppDbContext _context;
        
        public ClothePhotoRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClothePhoto>> GetClothePhotosByClotheId(Guid clotheId)
        {
            return await _context.ClothePhotos.Where(cp => cp.ClotheId == clotheId).ToListAsync();
        }
    }
}
