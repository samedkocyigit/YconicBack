using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.ClotheServices
{
    public interface IClotheService
    {
        Task<ApiResult<List<ClotheDto>>> GetAllClothes();
        Task<ApiResult<ClotheDto>> GetClotheById(Guid id);
        Task CreateClothe(AddClotheRequestDto clothe);
        Task<Clothe> UpdateClothe(Clothe clothe);
        Task<ApiResult<ClotheDto>> PatchClothe(Guid id, PatchClotheRequestDto dto);
        Task DeleteClothe(Guid id);
    }
}
