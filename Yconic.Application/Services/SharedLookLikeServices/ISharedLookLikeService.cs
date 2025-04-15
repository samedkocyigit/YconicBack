using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.LikeDtos;
using Yconic.Domain.Dtos.LikesDtos;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.SharedLookLikeServices
{
    public interface ISharedLookLikeService
    {
        Task<ApiResult<List<LikeDto>>> GetSharedLookLikesListBySharedLookId(Guid sharedLookId);
        //Task<ApiResult<List<Guid>>> GetSharedLookLikesListByUserId(Guid userId);
        Task<ApiResult<bool>> LikeSharedLook(CreateSharedLookLikeDto dto);
        Task<ApiResult<bool>> UnlikeSharedLook(Guid id);
    }
}
