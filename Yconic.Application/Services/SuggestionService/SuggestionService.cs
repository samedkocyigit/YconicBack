using AutoMapper;
using Microsoft.Extensions.Logging;
using Yconic.Application.Services.AiSuggestionServices;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
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
        protected readonly IMapper _mapper;
        public SuggestionService(ISuggestionRepository suggestionRepository,IUserRepository userRepository,ILogger<SuggestionService> logger ,IAiSuggestionService aiSuggestionService,IMapper mapper)
        {
            _suggestionRepository = suggestionRepository;
            _userRepository= userRepository;
            _logger = logger;
            _aiSuggestionService = aiSuggestionService;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<List<SuggestionDto>>> GetAllSuggestions()
        {
            var suggestions = await _suggestionRepository.GetAllSuggestions();
            var mappedSuggestions = _mapper.Map<List<SuggestionDto>>(suggestions);
            return ApiResult<List<SuggestionDto>>.Success(mappedSuggestions);
        }
        public async Task<ApiResult<SuggestionDto>> GetSuggestionById(Guid id)
        {
            var suggestion = await _suggestionRepository.GetSuggestionById(id);
            var mappedSuggestion = _mapper.Map<SuggestionDto>(suggestion);
            return ApiResult<SuggestionDto>.Success(mappedSuggestion);
        }
        
        public async Task<ApiResult<SuggestionDto>> CreateSuggestion(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var result = await _aiSuggestionService.GenerateSuggestedLook(user.Id);
            var suggestion = new Suggestion
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Description = "AI-Generated Suggestion Look",
                Image = result.MainImageUrl, 
                SuggestedLook = result.Clothes
            };

            var newSug = await _suggestionRepository.Add(suggestion);
            var mappedSug = _mapper.Map<SuggestionDto>(newSug);
            return ApiResult<SuggestionDto>.Success(mappedSug);
        }


        public async Task<ApiResult<SuggestionDto>> UpdateSuggestion(Suggestion suggestion)
        {
            var updatedSuggestion = await _suggestionRepository.Update(suggestion);
            var mappedSuggestion = _mapper.Map<SuggestionDto>(updatedSuggestion);
            return ApiResult<SuggestionDto>.Success(mappedSuggestion);
        }
        public async Task DeleteSuggestion(Guid id)
        {
            await _suggestionRepository.Delete(id);
        }
    }
}
