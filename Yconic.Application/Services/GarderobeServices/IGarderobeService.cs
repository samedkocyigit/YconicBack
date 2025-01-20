using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.GarderobeServices
{
    public interface IGarderobeService
    {
        Task<List<Garderobe>> GetAllGarderobes();
        Task<Garderobe> GetGarderobeById(Guid id);
        Task<Garderobe> CreateGarderobe(Garderobe garderobe);
        Task<Garderobe> UpdateGarderobe(Garderobe garderobe);
        Task DeleteGarderobe(Guid id);
    }
}
