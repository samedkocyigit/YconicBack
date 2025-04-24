using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.LikeDtos;
using Yconic.Domain.Dtos.LikesDtos;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.SharedLookLikeServices
{
    public interface ISharedLookLikeService
    {
        Task<ApiResult<List<UserMiniDto>>> GetSharedLookLikesListBySharedLookId(Guid sharedLookId, int page, int pageSize);

        //Task<ApiResult<List<Guid>>> GetSharedLookLikesListByUserId(Guid userId);
        Task<ApiResult<bool>> LikeSharedLook(Guid sharedLookId, Guid userId);

        Task<ApiResult<bool>> UnlikeSharedLook(Guid id);
    }
}