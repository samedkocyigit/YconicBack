using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheCategoryTypeDtos;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.ClotheCategoryTypeServices
{
    public interface IClotheCategoryTypeService
    {
        Task<ApiResult<List<ClotheCategoryTypeDto>>> GetAllCategoryTypes();

        Task<ApiResult<ClotheCategoryTypeDto>> GetCategoryTypeById(Guid id);

        Task<ApiResult<ClotheCategoryTypeDto>> CreateCategoryType(CreateClotheCategoryTypeDto clotheCategoryTypeDto);

        Task<ApiResult<ClotheCategoryTypeDto>> UpdateCategoryType(Guid id, ClotheCategoryTypeDto clotheCategoryTypeDto);

        Task<bool> DeleteCategoryType(Guid id);
    }
}