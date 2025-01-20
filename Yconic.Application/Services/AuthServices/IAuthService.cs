using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);
        Task<User> Register(RegisterDto registerModel);
        Task ForgotPassword(string email);
        Task ResetPassword(string email, string token, string newPassword);
    }
}
