using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Infrastructure.Repositories.ClotheRepositories;

namespace Yconic.Application.Services.ClotheServices
{
    public class ClotheService:IClotheService
    {
        protected readonly IClotheRepository _clotheRepository;
        protected readonly IClothePhotoRepository _clothePhotoRepository;
        protected readonly IClotheCategoriesRepository _clotheCategoriesRepository;
        protected readonly IMapper _mapper;
        public ClotheService(IClotheRepository clotheRepository ,IClothePhotoRepository clothePhotoRepository,IClotheCategoriesRepository clotheCategoriesRepository,IMapper mapper)
        {
            _clotheRepository = clotheRepository;
            _clothePhotoRepository = clothePhotoRepository;
            _clotheCategoriesRepository = clotheCategoriesRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ClotheDto>>> GetAllClothes()
        {
            var clothes =  await _clotheRepository.GetAllClothes();
            var mappedClothes = _mapper.Map<List<ClotheDto>>(clothes);
            return ApiResult<List<ClotheDto>>.Success(mappedClothes);
        }
        public async Task<ApiResult<ClotheDto>> GetClotheById(Guid id)
        {
            var clothe = await _clotheRepository.GetClotheById(id);
            var mappedClothe = _mapper.Map<ClotheDto>(clothe);
            return ApiResult<ClotheDto>.Success(mappedClothe);
        }
        public async Task CreateClothe(AddClotheRequestDto clothe)
        {
            var clotheObj = new Clothe
            {
                Name = clothe.Name,
                Brand = clothe.Brand ?? "Unknown",
                CategoryId = clothe.CategoryId
            };
            var clotheCategory = await _clotheCategoriesRepository.GetById(clothe.CategoryId);

            if (clothe.Photos == null || !clothe.Photos.Any())
            {
                throw new ArgumentException("At least one photo must be provided.");
            }

            var newClothe = await _clotheRepository.Add(clotheObj);

            foreach (var photo in clothe.Photos)
            {
                var photoUrl = await SavePhoto(photo);
                if (newClothe.MainPhoto == null)
                    newClothe.MainPhoto = photoUrl;

                var photoObj = new ClothePhoto
                {
                    Url = photoUrl,
                    ClotheId = newClothe.Id
                };

                await _clothePhotoRepository.Add(photoObj);
            }
            await _clotheRepository.Update(newClothe);

            clotheCategory.Clothes.Add(newClothe);
            await _clotheCategoriesRepository.Update(clotheCategory);
        }
        private string NormalizeFileName(string fileName)
        {
            var normalized = fileName
                .ToLowerInvariant()
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Replace(" ", "_");

            return Regex.Replace(normalized, @"[^a-zA-Z0-9_\.\-]", "");
        }



        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder);

            var originalName = Path.GetFileNameWithoutExtension(photo.FileName);
            var extension = Path.GetExtension(photo.FileName);
            var normalized = NormalizeFileName(originalName);
            var uniqueFileName = $"{Guid.NewGuid()}_{normalized}{extension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }


        public async Task<Clothe> UpdateClothe(Clothe clothe)
        {
            await _clotheRepository.Update(clothe);
            return clothe;
        }

        public async Task<ApiResult<ClotheDto>> PatchClothe(Guid id, PatchClotheRequestDto dto)
        {
            var clothe = await _clotheRepository.GetById(id);
            if (dto.Name != null)
                clothe.Name = dto.Name;
            if (dto.Brand != null)
                clothe.Brand = dto.Brand;
            if(dto.Description != null)
                clothe.Description = dto.Description;
            var updated =await _clotheRepository.Update(clothe);
            var mapped = _mapper.Map<ClotheDto>(updated);
            return ApiResult<ClotheDto>.Success(mapped);
        }
        public async Task DeleteClothe(Guid id)
        {
            await _clotheRepository.Delete(id);
        }
    }
}
