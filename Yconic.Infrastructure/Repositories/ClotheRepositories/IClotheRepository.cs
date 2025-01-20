using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.ClotheRepositories
{
    public interface IClotheRepository:IGenericRepository<Clothe>
    {
        Task<IEnumerable<Clothe>> GetAllClothes();
    }
}
