using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Application.Services.GarderobeServices
{
    public class GarderobeService:IGarderobeService
    {
        protected readonly IGarderobeRepository _garderobeRepository;
        public GarderobeService(IGarderobeRepository garderobeRepository)
        {
            _garderobeRepository = garderobeRepository;
        }
        public async Task<List<Garderobe>> GetAllGarderobes()
        {

           var  garderobe = await _garderobeRepository.GetAllGarderobes();
            return garderobe.ToList();
        }
        public async Task<Garderobe> GetGarderobeById(Guid id)
        {
            return await _garderobeRepository.GetById(id);
        }
        public async Task<Garderobe> CreateGarderobe(Garderobe garderobe)
        {
            return await _garderobeRepository.Add(garderobe);
        }
        public async Task<Garderobe> UpdateGarderobe(Garderobe garderobe)
        {
            return await _garderobeRepository.Update(garderobe);
        }
        public async Task DeleteGarderobe(Guid id)
        {
            await _garderobeRepository.Delete(id);
        }
    }   
}

