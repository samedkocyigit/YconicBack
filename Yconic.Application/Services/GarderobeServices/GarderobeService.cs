using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Dtos.GarderobeDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Application.Services.GarderobeServices
{
    public class GarderobeService:IGarderobeService
    {
        protected readonly IGarderobeRepository _garderobeRepository;
        protected readonly IMapper _mapper;
        public GarderobeService(IGarderobeRepository garderobeRepository, IMapper mapper)
        {
            _garderobeRepository = garderobeRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<GarderobeDto>>> GetAllGarderobes()
        {

            var  garderobe = await _garderobeRepository.GetAllGarderobes();
            var mappedGarderobe = _mapper.Map<List<GarderobeDto>>(garderobe);
            return ApiResult<List<GarderobeDto>>.Success(mappedGarderobe);
        }
        public async Task<ApiResult<GarderobeDto>> GetGarderobeById(Guid id)
        {
            var garderobe = await _garderobeRepository.GetById(id);
            var mappedGarderobe = _mapper.Map<GarderobeDto>(garderobe);
            return ApiResult<GarderobeDto>.Success(mappedGarderobe);
        }
        public async Task<ApiResult<GarderobeDto>> CreateGarderobe(Garderobe garderobe)
        {
            var newGarderobe = await _garderobeRepository.Add(garderobe);
            var mappedGarderobe = _mapper.Map<GarderobeDto>(newGarderobe);
            return ApiResult<GarderobeDto>.Success(mappedGarderobe);
        }
        public async Task<ApiResult<GarderobeDto>> UpdateGarderobe(Garderobe garderobe)
        {
            var updatedGarderobe = await _garderobeRepository.Update(garderobe);
            var mappedGarderobe = _mapper.Map<GarderobeDto>(updatedGarderobe);
            return ApiResult<GarderobeDto>.Success(mappedGarderobe);
        }
        public async Task DeleteGarderobe(Guid id)
        {
            var garderobe = await _garderobeRepository.GetById(id);
            foreach( var category in garderobe.ClothesCategory)
            {
                foreach (var clothe in category.Clothes)
                {
                    foreach (var photo in clothe.Photos)
                    {
                        DeletePhotoFile(photo.Url);
                    }
                }
            }
            await _garderobeRepository.Delete(id);
        }

        private void DeletePhotoFile(string photoUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl.TrimStart('/'));
            if (File.Exists(filePath)) { File.Delete(filePath); }
        }
    }   
}

