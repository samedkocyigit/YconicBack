﻿using Microsoft.AspNetCore.Http;
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

            var newClothe = await _clotheRepository.Add(clotheObj);
            clotheCategory.Clothes.Add(newClothe);

            foreach (var photo in clothe.Photos)
            {
                var photoUrl = await SavePhoto(photo);

                var photoObj = new ClothePhoto
                {
                    Url = photoUrl,
                    ClotheId = newClothe.Id
                };

                await _clothePhotoRepository.Add(photoObj);
            }
        }

        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
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
