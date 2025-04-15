using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.LikeDtos;
using Yconic.Domain.Dtos.LikesDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.SharedLookLikeRepositories;
using Yconic.Infrastructure.Repositories.SharedLookRepositories;

namespace Yconic.Application.Services.SharedLookLikeServices
{
    public class SharedLookLikeService: ISharedLookLikeService
    {
        private readonly ISharedLookLikeRepository _sharedLookLikeRepository;
        private readonly ISharedLookRepository _sharedLookRepository;
        private readonly IMapper _mapper;

        public SharedLookLikeService(ISharedLookLikeRepository sharedLookLikeRepository,ISharedLookRepository sharedLookRepository, IMapper mapper)
        {
            _sharedLookLikeRepository = sharedLookLikeRepository;
            _sharedLookRepository = sharedLookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<bool>> LikeSharedLook(CreateSharedLookLikeDto dto)
        {
            var sharedLook = await _sharedLookRepository.GetById(dto.sharedLookId);
            
            if(sharedLook == null)
            {
                return ApiResult<bool>.Fail("Shared look not found");
            }
            
            var isExist = await _sharedLookLikeRepository.IsLikeExist(dto.sharedLookId, dto.userId);
            if (isExist)
            {
                var existingLike = await _sharedLookLikeRepository.GetExistingLike(dto.sharedLookId,dto.userId);
                existingLike.UpdatedAt = DateTime.UtcNow;
                existingLike.IsLiked = true;
                await _sharedLookLikeRepository.Update(existingLike);
                return ApiResult<bool>.Success(true);
            }

            var like = new SharedLookLike
            {
                LikedSharedLookId = dto.sharedLookId,
                LikedUserId = dto.userId,
            };
            
            var createdLike = await _sharedLookLikeRepository.Add(like);
            sharedLook.Likes.Add(createdLike);
            
            await _sharedLookRepository.Update(sharedLook);

            return ApiResult<bool>.Success(true);
        }
        public async Task<ApiResult<bool>> UnlikeSharedLook(Guid likeId)
        {
            var like = await _sharedLookLikeRepository.GetById(likeId);
            if (like == null)
            {
                return ApiResult<bool>.Fail("Like not found");
            }
            var sharedLook = await _sharedLookRepository.GetById(like.LikedUserId);
            
            like.UpdatedAt = DateTime.UtcNow;
            like.IsLiked = false;
            await _sharedLookLikeRepository.Update(like);


            Console.WriteLine($"update oldu canım");

            return ApiResult<bool>.Success(true);
        }
        public async Task<ApiResult<List<LikeDto>>> GetSharedLookLikesListBySharedLookId(Guid sharedLookId)
        {
            var likes = await _sharedLookLikeRepository.GetSharedLookLikesBySharedLookId(sharedLookId);
            var mappedLikes = _mapper.Map<List<LikeDto>>(likes);
            return ApiResult<List<LikeDto>>.Success(mappedLikes);
        }
        //public async Task<ApiResult<List<Guid>>> GetLikesByUserId(Guid userId)
        //{
        //    var likes = await _sharedLookLikeRepository.(userId);
        //    var mappedLikes = _mapper.Map<List<SharedLookLikeDto>>(likes);
        //    return ApiResult<List<SharedLookLikeDto>>.Success(mappedLikes);
        //}
    }
}
