using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;


namespace Yconic.Application.Services.PersonasServices
{
    public class PersonasService:IPersonasService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IPersonaRepository _personaRepository;
        public PersonasService(IPersonaRepository personaRepository,IUserRepository userRepository)
        {
            _personaRepository = personaRepository;
            _userRepository = userRepository;
        }
        
        public async Task<List<Persona>> GetAllPersonas()
        {
            var  personas = await _personaRepository.GetAll();
            return personas.ToList();
        }
        public async Task<Persona> GetPersonaById(Guid id)
        {
            return await _personaRepository.GetById(id);
        }
        public async Task<Persona> CreatePersona(Persona persona)
        {

            var newPersona = await _personaRepository.Add(persona);
            var user = await _userRepository.GetById(persona.UserId);
            user.UserPersonaId = newPersona.Id;
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.Update(user);
            return newPersona;
        }
        public async Task<Persona> UpdatePersona(Persona persona)
        {
            return await _personaRepository.Update(persona);
        }
        public async Task DeletePersona(Guid id)
        {
            var persona = await _personaRepository.GetById(id);
            var userPersona = await _userRepository.GetById(persona.UserId);
            userPersona.UserPersonaId = null;
            userPersona.UpdatedAt = DateTime.UtcNow;
            await _userRepository.Update(userPersona);
            await _personaRepository.Delete(id);
        }

    }
}
