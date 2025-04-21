using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yconic.Application.Services.AiSuggestionServices;
using Yconic.Application.Services.SuggestionService;
using Yconic.Domain.Models;

namespace Yconic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionController:ControllerBase
    {
        protected readonly ISuggestionService _suggestionService;
        public SuggestionController(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // get all suggestions
        [HttpGet]
        public async Task<IActionResult> GetAllSuggestions()
        {
            var suggestions = await _suggestionService.GetAllSuggestions();
            return Ok(suggestions);
        }

        // get suggestion by id
        [HttpGet]
        [Route("get-suggestion-by-id/{id}")]
        public async Task<IActionResult> GetSuggestionById(Guid id)
        {
            var suggestion = await _suggestionService.GetSuggestionById(id);
            return Ok(suggestion);
        }

        //create suggestion
        [HttpPost]
        [Authorize]
        [Route("create-suggestion")]
        public async Task<IActionResult> CreateSuggestion()
        {
            var userId = GetUserId();
            var createdSuggestion = await _suggestionService.CreateSuggestion(userId);
            return Ok(createdSuggestion);
        }

        // update suggestion
        [HttpPut]
        [Authorize]
        [Route("update-suggestion")]
        public async Task<IActionResult> UpdateSuggestion(Suggestion suggestion)
        {
            var updatedSuggestion = await _suggestionService.UpdateSuggestion(suggestion);
            return Ok(updatedSuggestion);
        }

        // delete suggestion
        [HttpDelete]
        [Authorize]
        [Route("delete-suggestion/{id}")]
        public async Task<IActionResult> DeleteSuggestion(Guid id)
        {
            await _suggestionService.DeleteSuggestion(id);
            return Ok();
        }
    }
}
