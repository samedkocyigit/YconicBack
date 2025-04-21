using AutoMapper;
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

        public async Task<ApiResult<bool>> LikeSharedLook(Guid sharedLookId,Guid userId)
        {
            var sharedLook = await _sharedLookRepository.GetById(sharedLookId);
            
            if(sharedLook == null)
            {
                return ApiResult<bool>.Fail("Shared look not found");
            }
            
            var isExist = await _sharedLookLikeRepository.IsLikeExist(sharedLookId, userId);
            if (isExist)
            {
                var existingLike = await _sharedLookLikeRepository.GetExistingLike(sharedLookId,userId);
                existingLike.UpdatedAt = DateTime.UtcNow;
                existingLike.IsLiked = true;
                await _sharedLookLikeRepository.Update(existingLike);
                return ApiResult<bool>.Success(true);
            }

            var like = new SharedLookLike
            {
                LikedSharedLookId = sharedLookId,
                LikedUserId = userId,
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
        public async Task<ApiResult<List<LikeDto>>> GetSharedLookLikesListBySharedLookId(Guid sharedLookId, int page, int pageSize)
        {
            var likes = await _sharedLookLikeRepository.GetSharedLookLikesBySharedLookId(sharedLookId,page,pageSize);
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
