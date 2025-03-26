using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Yconic.Domain.Dtos;
using Yconic.Domain.Models;
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
        public ClotheService(IClotheRepository clotheRepository ,IClothePhotoRepository clothePhotoRepository,IClotheCategoriesRepository clotheCategoriesRepository)
        {
            _clotheRepository = clotheRepository;
            _clothePhotoRepository = clothePhotoRepository;
            _clotheCategoriesRepository = clotheCategoriesRepository;
        }

        public async Task<List<Clothe>> GetAllClothes()
        {
            var clothes =  await _clotheRepository.GetAllClothes();
            return clothes.ToList();
        }
        public async Task<Clothe> GetClotheById(Guid id)
        {
            var clothe = await _clotheRepository.GetById(id);
            return clothe;
        }
        public async Task CreateClothe(AddClotheRequest clothe)
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
        public async Task DeleteClothe(Guid id)
        {
            await _clotheRepository.Delete(id);
        }
    }
}
