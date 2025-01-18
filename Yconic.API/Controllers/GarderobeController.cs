using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetAllGarderobes()
        {
            var garderobes = await _garderobeService.GetAllGarderobes();
            return Ok(garderobes);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGarderobeById(int id)
        {
            var garderobe = await _garderobeService.GetGarderobeById(id);
            return Ok(garderobe);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGarderobe(Garderobe garderobe)
        {
            var createdGarderobe = await _garderobeService.CreateGarderobe(garderobe);
            return Ok(createdGarderobe);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGarderobe(Garderobe garderobe)
        {
            var updatedGarderobe = await _garderobeService.UpdateGarderobe(garderobe);
            return Ok(updatedGarderobe);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGarderobe(int id)
        {
            await _garderobeService.DeleteGarderobe(id);
            return Ok();
        }
    }
}
