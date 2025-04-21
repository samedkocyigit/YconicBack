using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.GarderobeServices;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GarderobeController:ControllerBase
    {
        protected readonly IGarderobeService _garderobeService;
        public GarderobeController(IGarderobeService garderobeService)
        {
            _garderobeService = garderobeService;
        }

        // get all garderobes
        [HttpGet]
        [Authorize]
        [Route("get-all-garderobes")]
        public async Task<IActionResult> GetAllGarderobes()
        {
            var garderobes = await _garderobeService.GetAllGarderobes();
            return Ok(garderobes);
        }

        // get garderobe by id
        [HttpGet]
        [Authorize]
        [Route("get-garderobe-by-id/{id}")]
        public async Task<IActionResult> GetGarderobeById(Guid id)
        {
            var garderobe = await _garderobeService.GetGarderobeById(id);
            return Ok(garderobe);
        }

        // create garderobe
        [HttpPost]
        [Authorize]
        [Route("create-garderobe")]
        public async Task<IActionResult> CreateGarderobe(Garderobe garderobe)
        {
            var createdGarderobe = await _garderobeService.CreateGarderobe(garderobe);
            return Ok(createdGarderobe);
        }

        // update garderobe
        [HttpPut]
        [Authorize]
        [Route("update-garderobe")]
        public async Task<IActionResult> UpdateGarderobe(Garderobe garderobe)
        {
            var updatedGarderobe = await _garderobeService.UpdateGarderobe(garderobe);
            return Ok(updatedGarderobe);
        }

        // delete garderobe
        [HttpDelete]
        [Authorize]
        [Route("delete-garderobe/{id}")]
        public async Task<IActionResult> DeleteGarderobe(Guid id)
        {
            await _garderobeService.DeleteGarderobe(id);
            return Ok();
        }
    }
}
