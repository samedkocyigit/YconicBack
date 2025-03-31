using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.ClotheCategoriesServices;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClotheCategoryController:ControllerBase
    {
        protected readonly IClotheCategoriesService _clotheCategoryService;
        public ClotheCategoryController(IClotheCategoriesService clotheCategoryService)
        {
            _clotheCategoryService = clotheCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClotheCategories()
        {
            var garderobeCategories = await _clotheCategoryService.GetAllClotheCategories();
            return Ok(garderobeCategories);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClotheCategoryById(Guid id)
        {
            var garderobeCategory = await _clotheCategoryService.GetClotheCategoriesById(id);
            return Ok(garderobeCategory);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClotheCategory(ClotheCategory clotheCategory)
        {
            var createdClotheCategory = await _clotheCategoryService.CreateClotheCategories(clotheCategory);
            return Ok(createdClotheCategory);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClotheCategoryWithPut(ClotheCategory clotheCategory)
        {
            var updatedClotheCategory = await _clotheCategoryService.UpdateClotheCategories(clotheCategory);
            return Ok(updatedClotheCategory);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateClotheCategoryWithPatch(Guid id,[FromBody] UpdateClotheCategoryDto dto)
        {
            var updatedCategory = await _clotheCategoryService.UpdateClotheCategoryWithPatch(id,dto);
            return Ok(updatedCategory);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClotheCategory(Guid id)
        {
            await _clotheCategoryService.DeleteClotheCategories(id);
            return Ok();
        }
    }
}
