using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.PersonaTypeDtos;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.PersonaTypeServices
{
    public interface IPersonaTypeService
    {
        Task<ApiResult<List<PersonaTypeDto>>> GetAllPersonaTypes();

        Task<ApiResult<PersonaTypeDto>> GetPersonaTypeById(Guid id);

        Task<ApiResult<PersonaTypeDto>> CreatePersonaType(CreatePersonaTypeDto personaTypeDto);

        Task<ApiResult<PersonaTypeDto>> UpdatePersonaType(PersonaTypeDto personaTypeDto);

        Task<bool> DeletePersonaType(Guid id);
    }
}