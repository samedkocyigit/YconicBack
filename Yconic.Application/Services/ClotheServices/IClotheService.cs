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
        Task CreateClothe(AddClotheRequest clothe);
        Task<Clothe> UpdateClothe(Clothe clothe);
        Task DeleteClothe(Guid id);
    }
}
