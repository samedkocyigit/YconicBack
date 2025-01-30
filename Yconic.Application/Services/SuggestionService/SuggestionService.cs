using Microsoft.Extensions.Logging;
using Yconic.Application.Services.AiSuggestionServices;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.SuggestionRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.SuggestionService
{
    public class SuggestionService:ISuggestionService
    {
        protected readonly ISuggestionRepository _suggestionRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly ILogger<SuggestionService> _logger;
        protected readonly IAiSuggestionService _aiSuggestionService;
        public SuggestionService(ISuggestionRepository suggestionRepository,IUserRepository userRepository,ILogger<SuggestionService> logger ,IAiSuggestionService aiSuggestionService)
        {
            _suggestionRepository = suggestionRepository;
            _userRepository= userRepository;
            _logger = logger;
            _aiSuggestionService = aiSuggestionService;
        }
        
        public async Task<List<Suggestions>> GetAllSuggestions()
        {
            var suggestions = await _suggestionRepository.GetAll();
            var listSuggestions = suggestions.ToList();
            return listSuggestions;
        }
        public async Task<Suggestions> GetSuggestionById(Guid id)
        {
            var suggestion = await _suggestionRepository.GetSuggestionById(id);
            return suggestion;
        }
        public async Task<Suggestions> CreateSuggestion(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var suggestedClothes = await _aiSuggestionService.GenerateSuggestedLook(user.UserPersona, user, otherParameters: null);

            var suggestion = new Suggestions
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Description = "AI-Generated Suggestion Look"
            };

            foreach (var item in suggestedClothes)
            {
                suggestion.SuggestedLook.Add(item);
            }

            return await _suggestionRepository.Add(suggestion);
        }

        public async Task<Suggestions> UpdateSuggestion(Suggestions suggestion)
        {
            var updatedSuggestion = await _suggestionRepository.Update(suggestion);
            return updatedSuggestion;
        }
        public async Task DeleteSuggestion(Guid id)
        {
            await _suggestionRepository.Delete(id);
        }
    }
}
