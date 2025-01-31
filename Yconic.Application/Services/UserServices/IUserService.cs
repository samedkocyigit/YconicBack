using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResult<List<UserDto>>> GetAllUsers();
        Task<ApiResult<UserDto>> GetUserById(Guid id);
        Task<ApiResult<UserDto>> CreateUser(User user);
        Task<ApiResult<UserDto>> UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}
