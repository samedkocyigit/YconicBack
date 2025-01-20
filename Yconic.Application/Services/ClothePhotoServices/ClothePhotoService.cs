using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Application.Services.ClotheServices;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;

namespace Yconic.Application.Services.ClothePhotoServices
{
    public class ClothePhotoService:IClothePhotoService
    {
        protected readonly IClothePhotoRepository _clothePhotoRepository;
        public ClothePhotoService(IClothePhotoRepository clothePhotoRepository)
        {
            _clothePhotoRepository = clothePhotoRepository;
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
        public async Task<ClothePhoto> UpdateClothePhoto(ClothePhoto clothePhoto)
        {
            return await _clothePhotoRepository.Update(clothePhoto);
        }
        public async Task DeleteClothePhoto(Guid id)
        {
            await _clothePhotoRepository.Delete(id);
        }

    }
}
