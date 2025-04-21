using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all clothe photos
        [HttpGet]
        [Authorize]
        [Route("get-all-clothe-photos")]
        public async Task<IActionResult> GetAllClothePhotos()
        {
            var clothePhotos = await _clothePhotoService.GetAllClothePhotos();
            return Ok(clothePhotos);
        }

        // get clothe photo by id
        [HttpGet]
        [Authorize]
        [Route("get-clothe-photo-by-id/{id}")]
        public async Task<IActionResult> GetClothePhotoById(Guid id)
        {
            var clothePhoto = await _clothePhotoService.GetClothePhotoById(id);
            return Ok(clothePhoto);
        }

        // create clothe photo
        [HttpPost]
        [Authorize]
        [Route("create-clothe-photo")]
        public async Task<IActionResult> CreateClothePhoto(ClothePhoto clothePhoto)
        {
            var createdClothePhoto = await _clothePhotoService.CreateClothePhoto(clothePhoto);
            return Ok(createdClothePhoto);
        }

        // add clothe photos
        [HttpPost]
        [Authorize]
        [Route("add-clothe-photos/{id}")]
        public async Task<IActionResult> AddClothePhotos(Guid id, AddClothePhotosDto clothePhotos)
        {
            var addedClothePhotos = await _clothePhotoService.AddClothePhotos(id, clothePhotos);
            return Ok(addedClothePhotos);
        }

        // update clothe photo
        [HttpPut]
        [Authorize]
        [Route("update-clothe-photo")]
        public async Task<IActionResult> UpdateClothePhoto(ClothePhoto clothePhoto)
        {
            var updatedClothePhoto = await _clothePhotoService.UpdateClothePhoto(clothePhoto);
            return Ok(updatedClothePhoto);
        }

        // delete clothe photo
        [HttpDelete]
        [Authorize]
        [Route("delete-clothe-photo/{id}")]
        public async Task<IActionResult> DeleteClothePhoto(Guid id)
        {
            await _clothePhotoService.DeleteClothePhoto(id);
            return Ok();
        }
    }
}
