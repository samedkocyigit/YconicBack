using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.ClotheCategoriesServices
{
    public interface IClotheCategoriesService
    {
        Task<List<ClotheCategories>> GetAllClotheCategories();
        Task<ClotheCategories> GetClotheCategoriesById(Guid id);
        Task<ClotheCategories> CreateClotheCategories(ClotheCategories clotheCategories);
        Task<ClotheCategories> UpdateClotheCategories(ClotheCategories clotheCategories);
        Task<ClotheCategories> UpdateClotheCategoryWithPatch(Guid id,UpdateClotheCategoryDto clotheCategoryDto);
        Task DeleteClotheCategories(Guid id);
    }
}
