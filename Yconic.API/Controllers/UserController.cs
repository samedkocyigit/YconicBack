using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models;

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

        private Guid GetUserId() =>

            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        

        // get all users    
        [HttpGet]
        [Authorize]
        [Route("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        //get all user simple dto
        [HttpGet]
        [Authorize]
        [Route("get-all-simple-users")]
        public async Task<IActionResult> GetAllUsersSimple()
        {
            var users = await _userService.GetAllSimpleUsers();
            return Ok(users);
        }

        // get user public datas
        [HttpGet]
        [Authorize]
        [Route("{id}/public-profile")]
        public async Task<IActionResult> GetUserPublicProfile(Guid id)
        {
            var user = await _userService.GetUserPublicProfile(id);
            return Ok(user);
        }

        // get user by id
        [HttpGet]
        [Authorize]
        [Route("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        // create user
        [HttpPost]
        [Authorize]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser( User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }

        // update user
        [HttpPut]
        [Authorize]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser( User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }

        // update user profile photo
        [HttpPost]
        [Authorize]
        [Route("add-profile-photo")]
        public async Task<IActionResult> AddProfilePhoto([FromForm] AddProfilePhotoDto profilePhoto)
        {
            var userId = GetUserId();
            var updatedUser = await _userService.AddProfilePhoto(userId,profilePhoto);
            return Ok(updatedUser);
        }

        //patch personal info
        [HttpPatch]
        [Authorize]
        [Route("personal-info")]
        public async Task<IActionResult> UserUpdatePersonalInfo([FromBody] UserPersonalPatchDto user)
        {
            var id = GetUserId();
            var updatedUser = await _userService.UpdateUserPersonal(id,user);
            return Ok(updatedUser);
        }

        //patch account info
        [HttpPatch]
        [Authorize]
        [Route("account-info")]
        public async Task<IActionResult> UserUpdateAccountInfo([FromBody] UserAccountPatchDto user)
        {
            var userId = GetUserId();
            var updatedUser = await _userService.UpdateUserAccount(userId,user);
            return Ok(updatedUser);
        }

        //patch user password
        [HttpPatch]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> UserUpdatePassword([FromBody] ChangePasswordDto user)
        {
            var userId = GetUserId();
            var updatedUser = await _userService.UpdateUserPassword(userId, user);
            return Ok(updatedUser);
        }

        //patch user private or public 
        [HttpPatch]
        [Authorize]
        [Route("change-privacy")]
        public async Task<IActionResult> UserUpdatePrivacy()
        {
            var userId = GetUserId();
            var updatedUser = await _userService.UpdatePrivacy(userId);
            return Ok(updatedUser);
        }

        // delete user
        [HttpDelete]
        [Authorize]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
