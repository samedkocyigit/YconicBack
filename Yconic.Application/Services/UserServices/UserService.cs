using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.UserServices
{
    public class UserService:IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly ILogger<UserService> _logger;
        protected readonly IMapper _mapper;
        public UserService(IUserRepository userRepository , ILogger<UserService> logger,IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var listUser = _mapper.Map<List<UserDto>>(users);
            return ApiResult<List<UserDto>>.Success(listUser);
        }
        public async Task<ApiResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            var mappedUser = _mapper.Map<UserDto>(user);    
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserDto>> CreateUser(User user)
        {
            var newUser = await _userRepository.Add(user);
            var mappedUser = _mapper.Map<UserDto>(newUser);
            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task<ApiResult<UserDto>> UpdateUser(User user)
        {
            var updatedUser = await _userRepository.Update(user);
            var mappedUser = _mapper.Map<UserDto>(updatedUser);

            return ApiResult<UserDto>.Success(mappedUser);
        }
        public async Task DeleteUser(Guid id)
        {
            await _userRepository.Delete(id);
        }
    }
}
