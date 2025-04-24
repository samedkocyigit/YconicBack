using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models.UserModels;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResult<List<UserDto>>> GetAllUsers();
        Task<ApiResult<List<UserMiniDto>>> GetAllSimpleUsers();
        Task<ApiResult<UserPublicDto>> GetUserPublicProfile(Guid id);
        Task<ApiResult<UserDto>> GetUserById(Guid id);
        Task<ApiResult<UserDto>> CreateUser(User user);
        Task<ApiResult<UserDto>> UpdateUser(User user);
        Task<ApiResult<UserDto>> AddProfilePhoto(Guid id, AddProfilePhotoDto profilePhoto);
        Task<ApiResult<UserDto>> UpdateUserPersonal(Guid id, UserPersonalPatchDto user);
        Task<ApiResult<UserDto>> UpdateUserAccount(Guid id, UserAccountPatchDto user);
        Task<ApiResult<bool>> UpdateUserPassword(Guid id, ChangePasswordDto user);
        Task<ApiResult<UserDto>> UpdatePrivacy(Guid id);
        Task DeleteUser(Guid id);
    }
}
