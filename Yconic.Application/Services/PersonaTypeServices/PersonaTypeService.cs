using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.PersonaTypeDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.PersonaTypeRopositories;

namespace Yconic.Application.Services.PersonaTypeServices
{
    public class PersonaTypeService : IPersonaTypeService
    {
        private readonly IPersonaTypeRepository _personaTypeRepository;
        private readonly IMapper _mapper;

        public PersonaTypeService(IPersonaTypeRepository personaTypeRepository, IMapper mapper)
        {
            _personaTypeRepository = personaTypeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<PersonaTypeDto>>> GetAllPersonaTypes()
        {
            var personaTypes = await _personaTypeRepository.GetAll();
            var mappedPersonaTypes = _mapper.Map<List<PersonaTypeDto>>(personaTypes);
            return ApiResult<List<PersonaTypeDto>>.Success(mappedPersonaTypes);
        }

        public async Task<ApiResult<PersonaTypeDto>> GetPersonaTypeById(Guid id)
        {
            var personaType = await _personaTypeRepository.GetById(id);
            var mappedPersonaType = _mapper.Map<PersonaTypeDto>(personaType);
            return ApiResult<PersonaTypeDto>.Success(mappedPersonaType);
        }

        public async Task<ApiResult<PersonaTypeDto>> CreatePersonaType(CreatePersonaTypeDto personaTypeDto)
        {
            var newPersonaType = _mapper.Map<PersonaType>(personaTypeDto);
            var createdPersonaType = await _personaTypeRepository.Add(newPersonaType);
            var mappedCreatedPersonaType = _mapper.Map<PersonaTypeDto>(createdPersonaType);
            return ApiResult<PersonaTypeDto>.Success(mappedCreatedPersonaType);
        }

        public async Task<ApiResult<PersonaTypeDto>> UpdatePersonaType(PersonaTypeDto personaTypeDto)
        {
            var existingPersonaType = await _personaTypeRepository.GetById(personaTypeDto.id);
            if (existingPersonaType == null)
            {
                return ApiResult<PersonaTypeDto>.Fail("Persona type not found");
            }
            var updatedPersonaType = _mapper.Map(personaTypeDto, existingPersonaType);
            var updatedEntity = await _personaTypeRepository.Update(updatedPersonaType);
            var mappedUpdatedEntity = _mapper.Map<PersonaTypeDto>(updatedEntity);
            return ApiResult<PersonaTypeDto>.Success(mappedUpdatedEntity);
        }

        public async Task<bool> DeletePersonaType(Guid id)
        {
            var personaType = await _personaTypeRepository.GetById(id);
            if (personaType == null)
            {
                return false;
            }
            await _personaTypeRepository.Delete(personaType.Id);
            return true;
        }
    }
}