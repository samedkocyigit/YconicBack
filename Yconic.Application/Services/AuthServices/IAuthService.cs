using Yconic.Domain.Dtos.Auth;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResult<LoginResponse>> Login(LoginDto loginDto);

        Task<ApiResult<UserDto>> Register(RegisterDto registerModel);

        Task ForgotPassword(string email);

        Task ResetPassword(string email, string token, string newPassword);
    }
}