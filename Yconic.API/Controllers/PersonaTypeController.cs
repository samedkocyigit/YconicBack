using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.PersonaTypeServices;
using Yconic.Domain.Dtos.PersonaTypeDtos;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaTypeController : ControllerBase
    {
        private readonly IPersonaTypeService _personaTypeService;

        public PersonaTypeController(IPersonaTypeService personaTypeService)
        {
            _personaTypeService = personaTypeService;
        }

        // Get all persona types
        [HttpGet]
        [Authorize]
        [Route("get-all-persona-types")]
        public async Task<IActionResult> GetAllPersonaTypes()
        {
            var personaTypes = await _personaTypeService.GetAllPersonaTypes();
            return Ok(personaTypes);
        }

        // Get persona type by ID
        [HttpGet]
        [Authorize]
        [Route("get-persona-type-by-id/{id}")]
        public async Task<IActionResult> GetPersonaTypeById(Guid id)
        {
            var personaType = await _personaTypeService.GetPersonaTypeById(id);
            return Ok(personaType);
        }

        // Create persona type
        [HttpPost]
        [Authorize]
        [Route("create-persona-type")]
        public async Task<IActionResult> CreatePersonaType(CreatePersonaTypeDto personaType)
        {
            var createdPersonaType = await _personaTypeService.CreatePersonaType(personaType);
            return Ok(createdPersonaType);
        }

        // Update persona type
        [HttpPut]
        [Authorize]
        [Route("update-persona-type")]
        public async Task<IActionResult> UpdatePersonaType(PersonaTypeDto personaType)
        {
            var updatedPersonaType = await _personaTypeService.UpdatePersonaType(personaType);
            return Ok(updatedPersonaType);
        }

        // Delete persona type
        [HttpDelete]
        [Authorize]
        [Route("delete-persona-type/{id}")]
        public async Task<IActionResult> DeletePersonaType(Guid id)
        {
            var result = await _personaTypeService.DeletePersonaType(id);
            if (result)
            {
                return Ok(new { message = "Persona type deleted successfully." });
            }
            return NotFound(new { message = "Persona type not found." });
        }
    }
}