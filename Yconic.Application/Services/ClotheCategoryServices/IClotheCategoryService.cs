using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.ClotheCategoryServices
{
    public interface IClotheCategoryService
    {
        Task<ApiResult<List<ClotheCategoryDto>>> GetAllClotheCategories();

        Task<ApiResult<ClotheCategoryDto>> GetClotheCategoryById(Guid id);

        Task<ApiResult<ClotheCategoryDto>> CreateClotheCategory(CreateClotheCategoryDto clotheCategories);

        Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategory(ClotheCategory clotheCategories);

        Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategoryWithPatch(Guid id, UpdateClotheCategoryDto clotheCategoryDto);

        Task DeleteClotheCategory(Guid id);
    }
}