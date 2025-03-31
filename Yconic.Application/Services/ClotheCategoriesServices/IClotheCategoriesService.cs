using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.ClotheCategoriesServices
{
    public interface IClotheCategoriesService
    {
        Task<ApiResult<List<ClotheCategoryDto>>> GetAllClotheCategories();
        Task<ApiResult<ClotheCategoryDto>> GetClotheCategoriesById(Guid id);
        Task<ApiResult<ClotheCategoryDto>> CreateClotheCategories(ClotheCategory clotheCategories);
        Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategories(ClotheCategory clotheCategories);
        Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategoryWithPatch(Guid id,UpdateClotheCategoryDto clotheCategoryDto);
        Task DeleteClotheCategories(Guid id);
    }
}
