using Microsoft.Extensions.Logging;
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

        public SuggestionService(ISuggestionRepository suggestionRepository,IUserRepository userRepository,ILogger<SuggestionService> logger )
        {
            _suggestionRepository = suggestionRepository;
            _userRepository= userRepository;
            _logger = logger;
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
            var newSuggestion = await _suggestionRepository.CreateSuggestion(user);
            return newSuggestion;
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
