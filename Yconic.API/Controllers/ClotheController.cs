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
        [HttpGet]
        public async Task<IActionResult> GetAllClothes()
        {
            var clothes = await _clotheService.GetAllClothes();
            return Ok(clothes);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClotheById(Guid id)
        {
            var clothe = await _clotheService.GetClotheById(id);
            return Ok(clothe);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClothe([FromForm] AddClotheRequestDto request)
        {
            if (request.Photos == null || !request.Photos.Any())
            {
                return BadRequest("At least one photo is required.");
            }

            // Save the clothe object to the database
            //var createdClothe = 
                await _clotheService.CreateClothe(request);

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClothe(Clothe clothe)
        {
            var updatedClothe = await _clotheService.UpdateClothe(clothe);
            return Ok(updatedClothe);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateClotheWithPatch(Guid id, [FromBody] PatchClotheRequestDto dto)
        {
            var updatedClothe = await _clotheService.PatchClothe(id, dto);
            return Ok(updatedClothe);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClothe(Guid id)
        {
            await _clotheService.DeleteClothe(id);
            return Ok();
        }
        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder); 

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }


    }
}
