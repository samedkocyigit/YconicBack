using Microsoft.AspNetCore.Mvc;
using Yconic.Application.Services.PersonasServices;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        protected readonly IPersonasService _personaService;
        public PersonaController(IPersonasService personaService)
        {
            _personaService = personaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPersonas()
        {
            var personas = await _personaService.GetAllPersonas();
            return Ok(personas);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPersonaById(Guid id)
        {
            var persona = await _personaService.GetPersonaById(id);
            return Ok(persona);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePersona(Persona persona)
        {
            var createdPersona = await _personaService.CreatePersona(persona);
            return Ok(createdPersona);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePersona(Persona persona)
        {
            var updatedPersona = await _personaService.UpdatePersona(persona);
            return Ok(updatedPersona);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePersona(Guid id)
        {
            await _personaService.DeletePersona(id);
            return Ok();
        }

    }
}
