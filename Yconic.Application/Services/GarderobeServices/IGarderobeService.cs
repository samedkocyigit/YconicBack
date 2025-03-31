using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.GarderobeDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.GarderobeServices
{
    public interface IGarderobeService
    {
        Task<ApiResult<List<GarderobeDto>>> GetAllGarderobes();
        Task<ApiResult<GarderobeDto>> GetGarderobeById(Guid id);
        Task<ApiResult<GarderobeDto>> CreateGarderobe(Garderobe garderobe);
        Task<ApiResult<GarderobeDto>> UpdateGarderobe(Garderobe garderobe);
        Task DeleteGarderobe(Guid id);
    }
}
