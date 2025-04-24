using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.AuthServices;
using Yconic.Domain.Dtos.Auth;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginModel)
        {
            var response = await _authService.Login(loginModel);
            return Ok(response);
        }

        // register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerModel)
        {
            var token = await _authService.Register(registerModel);
            return Ok(token);
        }

        // forgot password
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            try
            {
                await _authService.ForgotPassword(email);
                return Ok(new { success = true, message = "Password reset email sent." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // reset password
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordModel)
        {
            try
            {
                await _authService.ResetPassword(resetPasswordModel.Email, resetPasswordModel.Token, resetPasswordModel.NewPassword);
                return Ok(new { success = true, message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}