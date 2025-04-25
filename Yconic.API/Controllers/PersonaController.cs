using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.PersonaServices;
using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        protected readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all personas
        [HttpGet]
        [Route("get-all-personas")]
        [Authorize]
        public async Task<IActionResult> GetAllPersonas()
        {
            var personas = await _personaService.GetAllPersonas();
            return Ok(personas);
        }

        // get persona by id
        [HttpGet]
        [Authorize]
        [Route("get-persona-by-id/{id}")]
        public async Task<IActionResult> GetPersonaById(Guid id)
        {
            var persona = await _personaService.GetPersonaById(id);
            return Ok(persona);
        }

        // create persona
        [HttpPost]
        [Authorize]
        [Route("create-persona")]
        public async Task<IActionResult> CreatePersona(CreatePersonaDto persona)
        {
            var createdPersona = await _personaService.CreatePersona(persona);
            return Ok(createdPersona);
        }

        // update persona
        [HttpPut]
        [Authorize]
        [Route("update-persona")]
        public async Task<IActionResult> UpdatePersona(Persona persona)
        {
            var updatedPersona = await _personaService.UpdatePersona(persona);
            return Ok(updatedPersona);
        }

        // delete persona
        [HttpDelete]
        [Authorize]
        [Route("delete-persona/{id}")]
        public async Task<IActionResult> DeletePersona(Guid id)
        {
            await _personaService.DeletePersona(id);
            return Ok();
        }
    }
}