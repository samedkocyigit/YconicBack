using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.SuggestionRepositories
{
    public interface ISuggestionRepository:IGenericRepository<Suggestions>
    {
        Task<List<Suggestions>> GetAllSuggestions();
        Task<Suggestions> CreateSuggestion(User user);
        Task<Suggestions> GetSuggestionById(Guid id);
    }
}
