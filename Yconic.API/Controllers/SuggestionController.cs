using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllSuggestions()
        {
            var suggestions = await _suggestionService.GetAllSuggestions();
            return Ok(suggestions);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSuggestionById(Guid id)
        {
            var suggestion = await _suggestionService.GetSuggestionById(id);
            return Ok(suggestion);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSuggestion(Guid userId)
        {
            var createdSuggestion = await _suggestionService.CreateSuggestion(userId);
            return Ok(createdSuggestion);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSuggestion(Suggestions suggestion)
        {
            var updatedSuggestion = await _suggestionService.UpdateSuggestion(suggestion);
            return Ok(updatedSuggestion);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSuggestion(Guid id)
        {
            await _suggestionService.DeleteSuggestion(id);
            return Ok();
        }
    }
}
