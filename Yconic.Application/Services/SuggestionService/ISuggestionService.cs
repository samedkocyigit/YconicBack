using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;

namespace Yconic.Application.Services.SuggestionService
{
    public interface ISuggestionService
    {
        public Task<ApiResult<List<SuggestionDto>>> GetAllSuggestions();
        public Task<ApiResult<SuggestionDto>> GetSuggestionById(Guid id);
        public Task<ApiResult<SuggestionDto>> CreateSuggestion(Guid id);
        public Task<ApiResult<SuggestionDto>> UpdateSuggestion(Suggestion suggestion);
        public Task DeleteSuggestion(Guid id);
    }
}
