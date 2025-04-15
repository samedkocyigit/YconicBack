using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetAllSharedLooks")]
        public async Task<IActionResult> GetAllSharedLooks()
        {
            var result = await _sharedLookService.GetAllSharedLookList();
            return Ok(result);
        }
        
        [HttpGet("GetSharedLookById/{id}")]
        public async Task<IActionResult> GetSharedLookById(Guid id)
        {
            var result = await _sharedLookService.GetSharedLookById(id);
            return Ok(result);
        }
     
        [HttpGet("GetAllSharedLooksByUserId/{userId}")]
        public async Task<IActionResult> GetAllSharedLooksByUserId(Guid userId)
        {
            var result = await _sharedLookService.GetAllSharedLooksByUserId(userId);
            return Ok(result);
        }
     
        [HttpPost("CreateSharedLook")]
        public async Task<IActionResult> CreateSharedLook([FromBody] CreateSharedLookDto sharedLook)
        {
            var result = await _sharedLookService.CreateSharedLook(sharedLook);
            return Ok(result);
        }

        [HttpPut("UpdateSharedLook")]
        public async Task<IActionResult> UpdateSharedLook([FromBody] SharedLook sharedLook)
        {
            var result = await _sharedLookService.UpdateSharedLook(sharedLook);
            return Ok(result);
        }

        [HttpDelete("DeleteSharedLook/{id}")]
        public async Task<IActionResult> DeleteSharedLook(Guid id)
        {
            var result = await _sharedLookService.DeleteSharedLook(id);
            return Ok(result);
        }
    }
}
