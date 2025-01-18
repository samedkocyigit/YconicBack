using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.AuthServices;
using Yconic.Domain.Dtos;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController:ControllerBase
    {
        protected readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginModel)
        {
            var token = await _authService.Login(loginModel);
            return Ok(token);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var token = await _authService.Register(registerModel);
            return Ok(token);
        }
    }
}
