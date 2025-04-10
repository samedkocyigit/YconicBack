using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    // [Authorize]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // get all users    
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        //get all user simple dto
        [HttpGet]
        [Route("simple")]
        public async Task<IActionResult> GetAllUsersSimple()
        {
            var users = await _userService.GetAllSimpleUsers();
            return Ok(users);
        }

        // get user public datas
        [HttpGet]
        [Route("{id}/public-profile")]
        public async Task<IActionResult> GetUserPublicProfile(Guid id)
        {
            var user = await _userService.GetUserPublicProfile(id);
            return Ok(user);
        }
        // get user by id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        // create user
        [HttpPost]
        public async Task<IActionResult> CreateUser( User user)
        {
            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }

        // update user
        [HttpPut]
        public async Task<IActionResult> UpdateUser( User user)
        {
            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }

        // update user
        [HttpPost]
        [Route("add-profile-photo/{id}")]
        public async Task<IActionResult> AddProfilePhoto(Guid id, [FromForm] AddProfilePhotoDto profilePhoto)
        {
            var updatedUser = await _userService.AddProfilePhoto(id,profilePhoto);
            return Ok(updatedUser);
        }
        //patch personal info
        [HttpPatch]
        [Route("personal-info/{id}")]
        public async Task<IActionResult> UserUpdatePersonalInfo(Guid id, [FromBody] UserPersonalPatchDto user)
        {
            var updatedUser = await _userService.UpdateUserPersonal(id,user);
            return Ok(updatedUser);
        }

        //patch account info
        [HttpPatch]
        [Route("account-info/{id}")]
        public async Task<IActionResult> UserUpdateAccountInfo(Guid id, [FromBody] UserAccountPatchDto user)
        {
            var updatedUser = await _userService.UpdateUserAccount(id,user);
            return Ok(updatedUser);
        }

        //patch user password
        [HttpPatch]
        [Route("change-password/{id}")]
        public async Task<IActionResult> UserUpdatePassword(Guid id, [FromBody] ChangePasswordDto user)
        {
            var updatedUser = await _userService.UpdateUserPassword(id, user);
            return Ok(updatedUser);
        }

        //patch user private or public 
        [HttpPatch]
        [Route("change-privacy/{id}")]
        public async Task<IActionResult> UserUpdatePrivacy(Guid id)
        {
            var updatedUser = await _userService.UpdatePrivacy(id);
            return Ok(updatedUser);
        }
        // delete user
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
