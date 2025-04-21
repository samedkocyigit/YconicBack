using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.SharedLookServices;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SharedLookController : ControllerBase
    {
        private readonly ISharedLookService _sharedLookService;

        public SharedLookController(ISharedLookService sharedLookService)
        {
            _sharedLookService = sharedLookService;
        }

        Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all public shared looks
        [HttpGet]
        [Authorize]
        [Route("get-all-public-shared-looks")]
        public async Task<IActionResult> GetAllSharedLooks()
        {
            var result = await _sharedLookService.GetAllPublicSharedLookList();
            return Ok(result);
        }

        // get  shared look by id
        [HttpGet]
        [Authorize]
        [Route("get-shared-look-by-id/{id}")]
        public async Task<IActionResult> GetSharedLookById(Guid id)
        {
            var result = await _sharedLookService.GetSharedLookById(id);
            return Ok(result);
        }

        // get users all shared looks
        [HttpGet]
        [Authorize]
        [Route("get-all-shared-looks-by-userId/{userId}")]
        public async Task<IActionResult> GetAllSharedLooksByUserId(Guid userId,int page=1,int pageSize=10)
        {
            var result = await _sharedLookService.GetAllSharedLooksByUserId(userId,page,pageSize);
            return Ok(result);
        }

        //get following users shared looks paginated
        [HttpGet]
        [Authorize]
        [Route("get-shared-looks-user-who-followed-paginated")]
        public async Task<IActionResult> GetSharedLooksUserWhoFollowedPaginated(int page = 1, int pageSize = 10)
        {
            var userId = GetUserId();
            var result = await _sharedLookService.GetSharedLooksUserWhoFollowedPaginated(userId, page, pageSize);
            return Ok(result);
        }

        // create shared look
        [HttpPost]
        [Authorize]
        [Route("create-shared-look")]
        public async Task<IActionResult> CreateSharedLook([FromBody] CreateSharedLookDto sharedLook)
        {
            var result = await _sharedLookService.CreateSharedLook(sharedLook);
            return Ok(result);
        }

        // update shared look
        [HttpPut]
        [Authorize]
        [Route("update-shared-look")]
        public async Task<IActionResult> UpdateSharedLook([FromBody] SharedLook sharedLook)
        {
            var result = await _sharedLookService.UpdateSharedLook(sharedLook);
            return Ok(result);
        }

        // delete shared look
        [HttpDelete]
        [Authorize]
        [Route("delete-shared-look/{id}")]
        public async Task<IActionResult> DeleteSharedLook(Guid id)
        {
            var result = await _sharedLookService.DeleteSharedLook(id);
            return Ok(result);
        }
    }
}
