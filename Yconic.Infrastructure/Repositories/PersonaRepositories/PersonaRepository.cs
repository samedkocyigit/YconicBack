using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.PersonaRepositories
{
    public class PersonaRepository :GenericRepository<Persona>, IPersonaRepository
    {
        protected readonly AppDbContext _context;
        public PersonaRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
