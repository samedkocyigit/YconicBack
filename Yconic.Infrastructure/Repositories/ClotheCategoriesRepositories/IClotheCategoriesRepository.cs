using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories
{
    public interface IClotheCategoriesRepository:IGenericRepository<ClotheCategory>
    {
        Task<IEnumerable<ClotheCategory>> GetAllClotheCategories();
    }
}
