using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
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
        Task<ApiResult<UserDto>> AddProfilePhoto(Guid id, AddProfilePhotoDto profilePhoto);
        Task<ApiResult<bool>> UpdatePrivacy(Guid id);
        Task DeleteUser(Guid id);
    }
}
