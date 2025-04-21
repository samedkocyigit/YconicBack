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
        Task<ApiResult<List<SharedLookDto>>> GetAllPublicSharedLookList();
        Task<ApiResult<List<SharedLookDetailDto>>> GetAllSharedLooksByUserId(Guid userId, int page, int pageSize);
        Task<ApiResult<List<SharedLookDetailDto>>> GetSharedLooksUserWhoFollowedPaginated(Guid userId, int page, int pageSize);
        Task<ApiResult<SharedLookDetailDto>> GetSharedLookById(Guid id);
        Task<ApiResult<SharedLookDetailDto>> CreateSharedLook(CreateSharedLookDto sharedLook);
        Task<ApiResult<SharedLookDto>> UpdateSharedLook(SharedLook sharedLook);
        Task<ApiResult<bool>> DeleteSharedLook(Guid id);
    }
}
