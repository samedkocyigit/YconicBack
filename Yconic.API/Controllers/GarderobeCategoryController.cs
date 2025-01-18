using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.GarderobeCategoriesServices;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GarderobeCategoryController:ControllerBase
    {
        protected readonly IGarderobeCategoriesService _garderobeCategoryService;
        public GarderobeCategoryController(IGarderobeCategoriesService garderobeCategoryService)
        {
            _garderobeCategoryService = garderobeCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGarderobeCategories()
        {
            var garderobeCategories = await _garderobeCategoryService.GetAllGarderobeCategories();
            return Ok(garderobeCategories);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGarderobeCategoryById(int id)
        {
            var garderobeCategory = await _garderobeCategoryService.GetGarderobeCategoryById(id);
            return Ok(garderobeCategory);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGarderobeCategory(GarderobeCategories garderobeCategory)
        {
            var createdGarderobeCategory = await _garderobeCategoryService.CreateGarderobeCategory(garderobeCategory);
            return Ok(createdGarderobeCategory);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGarderobeCategory(GarderobeCategories garderobeCategory)
        {
            var updatedGarderobeCategory = await _garderobeCategoryService.UpdateGarderobeCategory(garderobeCategory);
            return Ok(updatedGarderobeCategory);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGarderobeCategory(int id)
        {
            await _garderobeCategoryService.DeleteGarderobeCategory(id);
            return Ok();
        }
    }
}
