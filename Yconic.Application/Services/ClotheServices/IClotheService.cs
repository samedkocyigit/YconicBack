using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.ClotheServices
{
    public interface IClotheService
    {
        Task<List<Clothe>> GetAllClothes();
        Task<Clothe> GetClotheById(Guid id);
        Task CreateClothe(AddClotheRequestDto clothe);
        Task<Clothe> UpdateClothe(Clothe clothe);
        Task<Clothe> PatchClothe(Guid id, PatchClotheRequestDto dto);
        Task DeleteClothe(Guid id);
    }
}
