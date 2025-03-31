using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;


namespace Yconic.Application.Services.PersonasServices
{
    public class PersonasService:IPersonasService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IPersonaRepository _personaRepository;
        protected readonly IMapper _mapper;
        public PersonasService(IPersonaRepository personaRepository,IUserRepository userRepository,IMapper mapper)
        {
            _personaRepository = personaRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<List<PersonaDto>>> GetAllPersonas()
        {
            var  personas = await _personaRepository.GetAll();
            var mappedPersonas = _mapper.Map<List<PersonaDto>>(personas);
            return ApiResult<List<PersonaDto>>.Success(mappedPersonas);
        }
        public async Task<ApiResult<PersonaDto>> GetPersonaById(Guid id)
        {
            var persona = await _personaRepository.GetById(id);
            var mappedPersona = _mapper.Map<PersonaDto>(persona);
            return ApiResult<PersonaDto>.Success(mappedPersona);
        }
        public async Task<ApiResult<PersonaDto>> CreatePersona(Persona persona)
        {

            var newPersona = await _personaRepository.Add(persona);
            var user = await _userRepository.GetById(persona.UserId);
            user.UserPersonaId = newPersona.Id;
            user.UpdatedAt = DateTime.UtcNow;
            var updated = await _userRepository.Update(user);
            var mappedPersona = _mapper.Map<PersonaDto>(newPersona);
            return ApiResult<PersonaDto>.Success(mappedPersona);
        }
        public async Task<ApiResult<PersonaDto>> UpdatePersona(Persona persona)
        {
            var personaUpdate=await _personaRepository.Update(persona);
            var mappedPersona = _mapper.Map<PersonaDto>(personaUpdate);
            return ApiResult<PersonaDto>.Success(mappedPersona);

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
