using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.ClotheCategoryServices;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClotheCategoryController : ControllerBase
    {
        protected readonly IClotheCategoryService _clotheCategoryService;

        public ClotheCategoryController(IClotheCategoryService clotheCategoryService)
        {
            _clotheCategoryService = clotheCategoryService;
        }

        // get all clothe categories
        [HttpGet]
        [Authorize]
        [Route("get-all-clothe-categories")]
        public async Task<IActionResult> GetAllClotheCategories()
        {
            var garderobeCategories = await _clotheCategoryService.GetAllClotheCategories();
            return Ok(garderobeCategories);
        }

        // get clothe category by id
        [HttpGet]
        [Authorize]
        [Route("get-clothe-category-by-id/{id}")]
        public async Task<IActionResult> GetClotheCategoryById(Guid id)
        {
            var garderobeCategory = await _clotheCategoryService.GetClotheCategoryById(id);
            return Ok(garderobeCategory);
        }

        // create clothe category
        [HttpPost]
        [Authorize]
        [Route("create-clothe-category")]
        public async Task<IActionResult> CreateClotheCategory(CreateClotheCategoryDto clotheCategory)
        {
            var createdClotheCategory = await _clotheCategoryService.CreateClotheCategory(clotheCategory);
            return Ok(createdClotheCategory);
        }

        // update clothe category
        [HttpPut]
        [Authorize]
        [Route("update-clothe-category")]
        public async Task<IActionResult> UpdateClotheCategoryWithPut(ClotheCategory clotheCategory)
        {
            var updatedClotheCategory = await _clotheCategoryService.UpdateClotheCategory(clotheCategory);
            return Ok(updatedClotheCategory);
        }

        // patch clothe category
        [HttpPatch]
        [Authorize]
        [Route("patch-clothe-category/{id}")]
        public async Task<IActionResult> UpdateClotheCategoryWithPatch(Guid id, [FromBody] UpdateClotheCategoryDto dto)
        {
            var updatedCategory = await _clotheCategoryService.UpdateClotheCategoryWithPatch(id, dto);
            return Ok(updatedCategory);
        }

        // delete clothe category
        [HttpDelete]
        [Authorize]
        [Route("delete-clothe-category/{id}")]
        public async Task<IActionResult> DeleteClotheCategory(Guid id)
        {
            await _clotheCategoryService.DeleteClotheCategory(id);
            return Ok();
        }
    }
}