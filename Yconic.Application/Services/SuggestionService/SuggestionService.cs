using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.SuggestionRepositories;

namespace Yconic.Application.Services.SuggestionService
{
    public class SuggestionService:ISuggestionService
    {
        protected readonly ISuggestionRepository _suggestionRepository;
        protected readonly ILogger<SuggestionService> _logger;

        public SuggestionService(ISuggestionRepository suggestionRepository,ILogger<SuggestionService> logger )
        {
            _suggestionRepository = suggestionRepository;
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
            var suggestion = await _suggestionRepository.GetById(id);
            return suggestion;
        }
        public async Task<Suggestions> CreateSuggestion(Suggestions suggestion)
        {
            var newSuggestion = await _suggestionRepository.Add(suggestion);
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
