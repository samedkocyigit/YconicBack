using Yconic.Domain.Models;

namespace Yconic.Application.Services.PersonasServices
{
    public interface IPersonasService
    {
        Task<List<Persona>> GetAllPersonas();
        Task<Persona> GetPersonaById(Guid id);
        Task<Persona> CreatePersona(Persona persona);
        Task<Persona> UpdatePersona(Persona persona);
        Task DeletePersona(Guid id);
    }
}
