﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.GarderobeRepositories
{
    public class GarderobeRepository:GenericRepository<Garderobe>,IGarderobeRepository
    {
        protected readonly AppDbContext _context;
        public GarderobeRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Garderobe>> GetAllGarderobes()
        {
            return await _context.Garderobes.Include(x => x.ClothesCategory).ThenInclude(x=> x.Clothes).ToListAsync();
        }
    }
}
