using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yconic.Application.Services.ClotheServices;
using Yconic.Domain.Dtos;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Infrastructure.Repositories.ClotheRepositories;

namespace Yconic.Application.Services.ClothePhotoServices
{
    public class ClothePhotoService:IClothePhotoService
    {
        protected readonly IClothePhotoRepository _clothePhotoRepository;
        protected readonly IClotheRepository _clotheRepository;
        public ClothePhotoService(IClothePhotoRepository clothePhotoRepository , IClotheRepository clotheRepository)
        {
            _clothePhotoRepository = clothePhotoRepository;
            _clotheRepository = clotheRepository;
        }
        public async Task<List<ClothePhoto>> GetAllClothePhotos()
        {
            var clothePhotos =  await _clothePhotoRepository.GetAll();
            return clothePhotos.ToList();
        }
        public async Task<ClothePhoto> GetClothePhotoById(Guid id)
        {
            return await _clothePhotoRepository.GetById(id);
        }
        public async Task<ClothePhoto> CreateClothePhoto(ClothePhoto clothePhoto)
        {
            return await _clothePhotoRepository.Add(clothePhoto);
        }
        public async Task<List<ClothePhoto>> AddClothePhotos(Guid clotheId, AddClothePhotosDto clothePhotos)
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
            return savedPhotos;
        }
        public async Task<ClothePhoto> UpdateClothePhoto(ClothePhoto clothePhoto)
        {
            return await _clothePhotoRepository.Update(clothePhoto);
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