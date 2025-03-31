﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Application.Services.ClotheCategoriesServices
{
    public class ClotheCategoriesService:IClotheCategoriesService
    {
        protected readonly IClotheCategoriesRepository _clotheCategoriesRepository;
        protected readonly IGarderobeRepository _garderobeRepository;
        protected readonly IMapper _mapper;
        public ClotheCategoriesService(IClotheCategoriesRepository clotheCategoriesRepository,IGarderobeRepository garderobeRepository,IMapper mapper)
        {
            _clotheCategoriesRepository = clotheCategoriesRepository;
            _garderobeRepository = garderobeRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<ClotheCategoryDto>>> GetAllClotheCategories()
        {
            var clotheCategories =  await _clotheCategoriesRepository.GetAllClotheCategories();
            var mappedCategories = _mapper.Map<List<ClotheCategoryDto>>(clotheCategories);
            return ApiResult<List<ClotheCategoryDto>>.Success(mappedCategories);
        }
        public async Task<ApiResult<ClotheCategoryDto>> GetClotheCategoriesById(Guid id)
        {
            var category=  await _clotheCategoriesRepository.GetById(id);
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(category);
            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }
        public async Task<ApiResult<ClotheCategoryDto>> CreateClotheCategories(ClotheCategory clotheCategories)
        {
            var newClotheCategory = await _clotheCategoriesRepository.Add(clotheCategories);
            var garderobe = await _garderobeRepository.GetById(newClotheCategory.GarderobeId);
            if(garderobe.ClothesCategory == null)
            {
                garderobe.ClothesCategory = new List<ClotheCategory>();
            }
            garderobe.ClothesCategory.Add(newClotheCategory);

            await _garderobeRepository.Update(garderobe);
            
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(newClotheCategory);

            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }
        public async Task<ApiResult<ClotheCategoryDto>> UpdateClotheCategories(ClotheCategory clotheCategories)
        {
            var category =  await _clotheCategoriesRepository.Update(clotheCategories);
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

            if (dto.CategoryType.HasValue)
                existing.CategoryType = (CategoryTypes)dto.CategoryType.Value;

            var updatedCategory = await _clotheCategoriesRepository.Update(existing);
            var mappedCategory = _mapper.Map<ClotheCategoryDto>(updatedCategory);
            
            return ApiResult<ClotheCategoryDto>.Success(mappedCategory);
        }

        public async Task DeleteClotheCategories(Guid id)
        {
            await _clotheCategoriesRepository.Delete(id);
        }

    }
}
