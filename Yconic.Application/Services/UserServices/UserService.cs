using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.UserServices
{
    public class UserService:IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository , ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ApiResult<List<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var listUser =users.ToList();
            return ApiResult<List<User>>.Success(listUser);
        }
        public async Task<ApiResult<User>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            return ApiResult<User>.Success(user);
        }
        public async Task<ApiResult<User>> CreateUser(User user)
        {
            var newUser = await _userRepository.Add(user);
            return ApiResult<User>.Success(newUser);
        }
        public async Task<ApiResult<User>> UpdateUser(User user)
        {
            var updatedUser = await _userRepository.Update(user);
            return ApiResult<User>.Success(updatedUser);
        }
        public async Task DeleteUser(Guid id)
        {
            await _userRepository.Delete(id);
        }
    }
}
