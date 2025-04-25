using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheCategoryTypeDtos;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.ClotheCategoryTypeRepositories;

namespace Yconic.Application.Services.ClotheCategoryTypeServices
{
    public class ClotheCategoryTypeService
    {
        private readonly IClotheCategoryTypeRepository _clotheCategoryTypeRepo;
        private readonly IMapper _mapper;

        public ClotheCategoryTypeService(IClotheCategoryTypeRepository clotheCategoryTypeRepo, IMapper mapper)
        {
            _clotheCategoryTypeRepo = clotheCategoryTypeRepo;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ClotheCategoryTypeDto>>> GetAllClotheCategoryTypes()
        {
            var clotheCategoryTypes = await _clotheCategoryTypeRepo.GetAll();
            var mappedClotheCategoryTypes = _mapper.Map<List<ClotheCategoryTypeDto>>(clotheCategoryTypes);
            return ApiResult<List<ClotheCategoryTypeDto>>.Success(mappedClotheCategoryTypes);
        }

        public async Task<ApiResult<ClotheCategoryTypeDto>> GetClotheCategoryTypeById(Guid id)
        {
            var clotheCategoryType = await _clotheCategoryTypeRepo.GetById(id);
            var mappedClotheCategoryType = _mapper.Map<ClotheCategoryTypeDto>(clotheCategoryType);
            return ApiResult<ClotheCategoryTypeDto>.Success(mappedClotheCategoryType);
        }

        public async Task<ApiResult<ClotheCategoryTypeDto>> CreateClotheCategoryType(ClotheCategoryTypeDto clotheCategoryTypeDto)
        {
            var newClotheCategoryType = _mapper.Map<Domain.Models.ClotheCategoryType>(clotheCategoryTypeDto);
            var createdClotheCategoryType = await _clotheCategoryTypeRepo.Add(newClotheCategoryType);
            var mappedCreatedClotheCategoryType = _mapper.Map<ClotheCategoryTypeDto>(createdClotheCategoryType);
            return ApiResult<ClotheCategoryTypeDto>.Success(mappedCreatedClotheCategoryType);
        }

        public async Task<ApiResult<ClotheCategoryTypeDto>> UpdateClotheCategoryType(Guid id, ClotheCategoryTypeDto clotheCategoryTypeDto)
        {
            var existingClotheCategoryType = await _clotheCategoryTypeRepo.GetById(id);
            if (existingClotheCategoryType == null)
            {
                return ApiResult<ClotheCategoryTypeDto>.Fail("Clothe category type not found");
            }
            var updatedClotheCategoryType = _mapper.Map(clotheCategoryTypeDto, existingClotheCategoryType);
            var updatedEntity = await _clotheCategoryTypeRepo.Update(updatedClotheCategoryType);
            var mappedUpdatedEntity = _mapper.Map<ClotheCategoryTypeDto>(updatedEntity);
            return ApiResult<ClotheCategoryTypeDto>.Success(mappedUpdatedEntity);
        }

        public async Task<bool> DeleteClotheCategoryType(Guid id)
        {
            var clotheCategoryType = await _clotheCategoryTypeRepo.GetById(id);
            if (clotheCategoryType == null)
            {
                return false;
            }
            await _clotheCategoryTypeRepo.Delete(clotheCategoryType.Id);
            return true;
        }
    }
}