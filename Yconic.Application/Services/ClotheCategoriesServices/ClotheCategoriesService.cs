using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Application.Services.ClotheCategoriesServices
{
    public class ClotheCategoriesService:IClotheCategoriesService
    {
        protected readonly IClotheCategoriesRepository _clotheCategoriesRepository;
        protected readonly IGarderobeRepository _garderobeRepository;
        public ClotheCategoriesService(IClotheCategoriesRepository clotheCategoriesRepository,IGarderobeRepository garderobeRepository)
        {
            _clotheCategoriesRepository = clotheCategoriesRepository;
            _garderobeRepository = garderobeRepository;
        }
        public async Task<List<ClotheCategories>> GetAllClotheCategories()
        {
            var clotheCategories =  await _clotheCategoriesRepository.GetAllClotheCategories();
            return clotheCategories.ToList();
        }
        public async Task<ClotheCategories> GetClotheCategoriesById(Guid id)
        {
            return await _clotheCategoriesRepository.GetById(id);
        }
        public async Task<ClotheCategories> CreateClotheCategories(ClotheCategories clotheCategories)
        {
            var newClotheCategory = await _clotheCategoriesRepository.Add(clotheCategories);
            var garderobe = await _garderobeRepository.GetById(newClotheCategory.GarderobeId);
            if(garderobe.ClothesCategory == null)
            {
                garderobe.ClothesCategory = new List<ClotheCategories>();
            }
            garderobe.ClothesCategory.Add(newClotheCategory);

            await _garderobeRepository.Update(garderobe);
            return newClotheCategory;
        }
        public async Task<ClotheCategories> UpdateClotheCategories(ClotheCategories clotheCategories)
        {
            return await _clotheCategoriesRepository.Update(clotheCategories);
        }
        public async Task DeleteClotheCategories(Guid id)
        {
            await _clotheCategoriesRepository.Delete(id);
        }

    }
}
