using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResult<List<User>>> GetAllUsers();
        Task<ApiResult<User>> GetUserById(Guid id);
        Task<ApiResult<User>> CreateUser(User user);
        Task<ApiResult<User>> UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}
