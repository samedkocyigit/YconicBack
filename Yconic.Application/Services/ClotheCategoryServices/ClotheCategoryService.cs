using AutoMapper;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.ClotheCategoryRepositories;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Application.Services.ClotheCategoryServices
{
    public class ClotheCategoryService : IClotheCategoryService
    {
        protected readonly IClotheCategoryRepository _clotheCategoriesRepository;
        protected readonly IGarderobeRepository _garderobeRepository;
        protected readonly IMapper _mapper;

        public ClotheCategoryService(IClotheCategoryRepository clotheCategoriesRepository, IGarderobeRepository garderobeRepository, IMapper mapper)
        {
            _clotheCategoriesRepository = clotheCategoriesRepository;
            _garderobeRepository = garderobeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ClotheCategoryDto>>> GetAllClotheCategories()
        {
            var clotheCategories = await _clotheCategoriesRepository.GetAllClotheCategories();
            var mappedCategories = _mapper.Map<List<ClotheCategoryDto>>(clotheCategories);
            return ApiResult<List<ClotheCategoryDto>>.Success(mappedCategories);
        }

        public async Task<ApiResult<ClotheCategoryDto>> GetClotheCategoryById(Guid id)
        {
            var category = await _clotheCategoriesRepository.GetById(id);
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(category);
            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }

        public async Task<ApiResult<ClotheCategoryDto>> CreateClotheCategory(CreateClotheCategoryDto clotheCategory)
        {
            var mappedCategory = _mapper.Map<ClotheCategory>(clotheCategory);
            var newClotheCategory = await _clotheCategoriesRepository.Add(mappedCategory);

            var garderobe = await _garderobeRepository.GetById(newClotheCategory.GarderobeId);
            if (garderobe.ClothesCategory == null)
            {
                garderobe.ClothesCategory = new List<ClotheCategory>();
            }
            garderobe.ClothesCategory.Add(newClotheCategory);

            await _garderobeRepository.Update(garderobe);

            var mappedCategoryDto = _mapper.Map<ClotheCategoryDto>(newClotheCategory);

            return ApiResult<ClotheCategoryDto>.Success(mappedCategoryDto);
        }

        public async Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategory(ClotheCategory clotheCategories)
        {
            var category = await _clotheCategoriesRepository.Update(clotheCategories);
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(category);
            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }

        public async Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategoryWithPatch(Guid id, UpdateClotheCategoryDto dto)
        {
            var existing = await _clotheCategoriesRepository.GetById(id);
            if (existing == null)
                throw new Exception("Category not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))

                existing.Name = dto.Name;

            if (dto.CategoryTypeId.HasValue)
                existing.ClotheCategoryTypeId = dto.CategoryTypeId.Value;

            var updatedCategory = await _clotheCategoriesRepository.Update(existing);
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(updatedCategory);

            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }

        public async Task DeleteClotheCategory(Guid id)
        {
            var clotheCategory = await _clotheCategoriesRepository.GetClotheCategoryById(id);

            if (clotheCategory.Clothes.Count > 0)
            {
                foreach (var clothe in clotheCategory.Clothes)
                {
                    foreach (var photo in clothe.Photos)
                    {
                        DeletePhotoFile(photo.Url);
                    }
                }
            }
            await _clotheCategoriesRepository.Delete(id);
        }

        private void DeletePhotoFile(string photoUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl.TrimStart('/'));
            if (File.Exists(filePath)) { File.Delete(filePath); }
        }
    }
}