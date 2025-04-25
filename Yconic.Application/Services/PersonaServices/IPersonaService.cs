using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.PersonaServices
{
    public interface IPersonaService
    {
        Task<ApiResult<List<PersonaDto>>> GetAllPersonas();

        Task<ApiResult<PersonaDto>> GetPersonaById(Guid id);

        Task<ApiResult<PersonaDto>> CreatePersona(CreatePersonaDto persona);

        Task<ApiResult<PersonaDto>> UpdatePersona(Persona persona);

        Task DeletePersona(Guid id);
    }
}