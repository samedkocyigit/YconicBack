using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.ClotheServices;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClotheController:ControllerBase
    {
        protected readonly IClotheService _clotheService;
        public ClotheController(IClotheService clotheService)
        {
            _clotheService = clotheService;
        }

        // get all clothes
        [HttpGet]
        [Authorize]
        [Route("get-all-clothes")]
        public async Task<IActionResult> GetAllClothes()
        {
            var clothes = await _clotheService.GetAllClothes();
            return Ok(clothes);
        }

        // get clothe by id
        [HttpGet]
        [Authorize]
        [Route("get-clothe-by-id/{id}")]
        public async Task<IActionResult> GetClotheById(Guid id)
        {
            var clothe = await _clotheService.GetClotheById(id);
            return Ok(clothe);
        }

        // create clothe
        [HttpPost]
        [Authorize]
        [Route("create-clothe")]
        public async Task<IActionResult> CreateClothe([FromForm] AddClotheRequestDto request)
        {
            if (request.Photos == null || !request.Photos.Any())
            {
                return BadRequest("At least one photo is required.");
            }

                await _clotheService.CreateClothe(request);

            return Ok();
        }

        // update clothe
        [HttpPut]
        [Authorize]
        [Route("update-clothe")]
        public async Task<IActionResult> UpdateClothe(Clothe clothe)
        {
            var updatedClothe = await _clotheService.UpdateClothe(clothe);
            return Ok(updatedClothe);
        }

        [HttpPatch]
        [Authorize]
        [Route("update-clothe/{id}")]
        public async Task<IActionResult> UpdateClotheWithPatch(Guid id, [FromBody] PatchClotheRequestDto dto)
        {
            var updatedClothe = await _clotheService.PatchClothe(id, dto);
            return Ok(updatedClothe);
        }

        // delete clothe
        [HttpDelete]
        [Authorize]
        [Route("delete-clothe/{id}")]
        public async Task<IActionResult> DeleteClothe(Guid id)
        {
            await _clotheService.DeleteClothe(id);
            return Ok();
        }
    }
}
