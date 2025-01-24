using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.Auth;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResult<LoginResponse>> Login(LoginDto loginDto);
        Task<ApiResult<User>> Register(RegisterDto registerModel);
        Task ForgotPassword(string email);
        Task ResetPassword(string email, string token, string newPassword);
    }
}
