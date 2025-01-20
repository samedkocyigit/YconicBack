using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
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

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            var listUser =users.ToList();
            return listUser;
        }
        public async Task<User> GetUserById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            return user;
        }
        public async Task<User> CreateUser(User user)
        {
            var newUser = await _userRepository.Add(user);
            return newUser;
        }
        public async Task<User> UpdateUser(User user)
        {
            var updatedUser = await _userRepository.Update(user);
            return updatedUser;
        }
        public async Task DeleteUser(Guid id)
        {
            await _userRepository.Delete(id);
        }
    }
}
