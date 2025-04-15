using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.SharedLookRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.SharedLookServices
{
    public class SharedLookService:ISharedLookService
    {
        private readonly ISharedLookRepository _sharedLookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SharedLookService(ISharedLookRepository sharedLookRepository,IUserRepository userRepository, IMapper mapper)
        {
            _sharedLookRepository = sharedLookRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<SharedLookDto>>> GetAllSharedLookList()
        {
            var sharedLooks = await _sharedLookRepository.GetAllList();
            var mappedLooks=    _mapper.Map<List<SharedLookDto>>(sharedLooks);
            return ApiResult<List<SharedLookDto>>.Success(mappedLooks);
        }
        public async Task<ApiResult<List<SharedLookDto>>> GetAllSharedLooksByUserId(Guid userId)
        {
            var sharedLooks = await _sharedLookRepository.GetSharedLooksByUserId(userId);
            var mappedLook=    _mapper.Map<List<SharedLookDto>>(sharedLooks);
            return ApiResult<List<SharedLookDto>>.Success(mappedLook);
        }
        public async Task<ApiResult<SharedLookDetailDto>> GetSharedLookById(Guid id)
        {
            var sharedLook = await _sharedLookRepository.GetById(id);
            var mapped =    _mapper.Map<SharedLookDetailDto>(sharedLook);
            return ApiResult<SharedLookDetailDto>.Success(mapped);
        }
        public async Task<ApiResult<SharedLookDto>> CreateSharedLook(CreateSharedLookDto sharedLookDto)
        {
            var user = await _userRepository.GetById(sharedLookDto.UserId);
            var mappedSharedLook = _mapper.Map<SharedLook>(sharedLookDto);

            var createdSharedLook = await _sharedLookRepository.Add(mappedSharedLook);
            
            user.SharedLooks.Add(createdSharedLook);
            await _userRepository.Update(user);
            
            var mappedLook = _mapper.Map<SharedLookDto>(createdSharedLook);
            return ApiResult<SharedLookDto>.Success(mappedLook);
        }
        public async Task<ApiResult<SharedLookDto>> UpdateSharedLook(SharedLook sharedLook)
        {
            var updatedSharedLook = await _sharedLookRepository.Update(sharedLook);
            var mappedLook= _mapper.Map<SharedLookDto>(updatedSharedLook);
            return ApiResult<SharedLookDto>.Success(mappedLook);
        }
        public async Task<ApiResult<bool>> DeleteSharedLook(Guid id)
        {
            try
            {
                var sharedLook = await _sharedLookRepository.GetById(id);
                var user = await _userRepository.GetById(sharedLook.UserId);

                await _sharedLookRepository.Delete(id);
                user.SharedLooks.Remove(sharedLook);
                await _userRepository.Update(user);
                return ApiResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Fail(ex.Message);
            }
        }

    }
}
