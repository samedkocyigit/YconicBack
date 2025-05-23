﻿using Yconic.Domain.Dtos.Ai;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.AiSuggestionServices
{
    public interface IAiSuggestionService
    {
        // Task<List<Clothe>> GenerateSuggestedLook(Persona persona, User user, object otherParameters);
        Task<SuggestedLookResult> GenerateSuggestedLook(Guid userId);

    }
}
