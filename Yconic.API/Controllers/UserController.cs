using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.UserServices;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
