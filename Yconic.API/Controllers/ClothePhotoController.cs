using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.ClothePhotoServices;
using Yconic.Domain.Dtos.ClothePhotoDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClothePhotoController:ControllerBase
    {
        protected readonly IClothePhotoService _clothePhotoService;
        public ClothePhotoController(IClothePhotoService clothePhotoService)
        {
            _clothePhotoService = clothePhotoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClothePhotos()
        {
            var clothePhotos = await _clothePhotoService.GetAllClothePhotos();
            return Ok(clothePhotos);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClothePhotoById(Guid id)
        {
            var clothePhoto = await _clothePhotoService.GetClothePhotoById(id);
            return Ok(clothePhoto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClothePhoto(ClothePhoto clothePhoto)
        {
            var createdClothePhoto = await _clothePhotoService.CreateClothePhoto(clothePhoto);
            return Ok(createdClothePhoto);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddClothePhotos(Guid id, AddClothePhotosDto clothePhotos)
        {
            var addedClothePhotos = await _clothePhotoService.AddClothePhotos(id, clothePhotos);
            return Ok(addedClothePhotos);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClothePhoto(ClothePhoto clothePhoto)
        {
            var updatedClothePhoto = await _clothePhotoService.UpdateClothePhoto(clothePhoto);
            return Ok(updatedClothePhoto);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClothePhoto(Guid id)
        {
            await _clothePhotoService.DeleteClothePhoto(id);
            return Ok();
        }
    }
}
