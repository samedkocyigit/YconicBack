using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SuggestionRepositories
{
    public interface ISuggestionRepository:IGenericRepository<Suggestion>
    {
        Task<List<Suggestion>> GetAllSuggestions();
        Task<Suggestion> CreateSuggestion(User user);
        Task<Suggestion> GetSuggestionById(Guid id);
    }
}
