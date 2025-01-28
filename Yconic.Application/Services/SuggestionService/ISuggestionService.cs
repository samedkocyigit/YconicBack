using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.SuggestionService
{
    public interface ISuggestionService
    {
        public Task<List<Suggestions>> GetAllSuggestions();
        public Task<Suggestions> GetSuggestionById(Guid id);
        public Task<Suggestions> CreateSuggestion(Guid id);
        public Task<Suggestions> UpdateSuggestion(Suggestions suggestion);
        public Task DeleteSuggestion(Guid id);
    }
}
