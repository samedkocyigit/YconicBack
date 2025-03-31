using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yconic.Application.Services.ClotheServices;
using Yconic.Domain.Dtos.ClothePhotoDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Infrastructure.Repositories.ClotheRepositories;

namespace Yconic.Application.Services.ClothePhotoServices
{
    public class ClothePhotoService:IClothePhotoService
    {
        protected readonly IClothePhotoRepository _clothePhotoRepository;
        protected readonly IClotheRepository _clotheRepository;
        protected readonly IMapper _mapper;
        public ClothePhotoService(IClothePhotoRepository clothePhotoRepository , IClotheRepository clotheRepository,IMapper mapper)
        {
            _clothePhotoRepository = clothePhotoRepository;
            _clotheRepository = clotheRepository;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<ClothePhotoDto>>> GetAllClothePhotos()
        {
            var clothePhotos =  await _clothePhotoRepository.GetAll();
            var mappedClothePhotos = _mapper.Map<List<ClothePhotoDto>>(clothePhotos);
            return ApiResult<List<ClothePhotoDto>>.Success(mappedClothePhotos);
        }
        public async Task<ApiResult<ClothePhotoDto>> GetClothePhotoById(Guid id)
        {
            var clothe = await _clothePhotoRepository.GetById(id);
            var mappedClothe = _mapper.Map<ClothePhotoDto>(clothe); 
            return ApiResult<ClothePhotoDto>.Success(mappedClothe);
        }
        public async Task<ApiResult<ClothePhotoDto>> CreateClothePhoto(ClothePhoto clothePhoto)
        {
            var photo = await _clothePhotoRepository.Add(clothePhoto);
            var mappedPhoto = _mapper.Map<ClothePhotoDto>(photo);
            return ApiResult<ClothePhotoDto>.Success(mappedPhoto);
        }
        public async Task<ApiResult<List<ClothePhotoDto>>> AddClothePhotos(Guid clotheId, AddClothePhotosDto clothePhotos)
        {
            var savedPhotos = new List<ClothePhoto>();
            foreach (var photo in clothePhotos.Photos)
            {
                var photoUrl = await SavePhoto(photo);
                var photoObj = new ClothePhoto
                {
                    Url = photoUrl,
                    ClotheId = clotheId
                };
                await _clothePhotoRepository.Add(photoObj);
                savedPhotos.Add(photoObj);
            }
            var mappedPhotos = _mapper.Map<List<ClothePhotoDto>>(savedPhotos);
            return ApiResult<List<ClothePhotoDto>>.Success(mappedPhotos);
        }
        public async Task<ApiResult<ClothePhotoDto>> UpdateClothePhoto(ClothePhoto clothePhoto)
        {
            var photo = await _clothePhotoRepository.Update(clothePhoto);
            var mappedPhoto = _mapper.Map<ClothePhotoDto>(photo);
            return ApiResult<ClothePhotoDto>.Success(mappedPhoto);
        }
        public async Task DeleteClothePhoto(Guid id)
        {
            var photoToDelete = await _clothePhotoRepository.GetById(id);
            if (photoToDelete == null)
                throw new Exception("Photo not found");

            var clotheId = photoToDelete.ClotheId;

            await _clothePhotoRepository.Delete(id);

            var clothe = await _clotheRepository.GetById(clotheId);

            if (clothe.MainPhoto == photoToDelete.Url)
            {
                var remainingPhotos = await _clothePhotoRepository.GetClothePhotosByClotheId(clotheId);

                if (remainingPhotos.Any())
                {
                    clothe.MainPhoto = remainingPhotos.First().Url;
                }
                else
                {
                    clothe.MainPhoto = null;
                }

                await _clotheRepository.Update(clothe);
            }
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
    }
}