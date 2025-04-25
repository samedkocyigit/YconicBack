using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.PersonaTypeRopositories
{
    public class PersonaTypeRepository : GenericRepository<PersonaType>, IPersonaTypeRepository
    {
        protected readonly AppDbContext _context;

        public PersonaTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}