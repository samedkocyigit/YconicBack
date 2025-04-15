using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.SharedLookServices
{
    public interface ISharedLookService
    {
        Task<ApiResult<List<SharedLookDto>>> GetAllSharedLookList();
        Task<ApiResult<List<SharedLookDto>>> GetAllSharedLooksByUserId(Guid userId);
        Task<ApiResult<SharedLookDetailDto>> GetSharedLookById(Guid id);
        Task<ApiResult<SharedLookDto>> CreateSharedLook(CreateSharedLookDto sharedLook);
        Task<ApiResult<SharedLookDto>> UpdateSharedLook(SharedLook sharedLook);
        Task<ApiResult<bool>> DeleteSharedLook(Guid id);
    }
}
