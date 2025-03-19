using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yconic.Application.Services.ClotheServices;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Dtos.Ai;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.AiSuggestionServices
{
    public class AiSuggestionService : IAiSuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        private readonly IClotheService _clotheService;

        public AiSuggestionService(HttpClient httpClient,IUserService userService,IClotheService clotheService,IMapper mapper)
        {
            _httpClient = httpClient;
            _userService = userService;
            _clotheService = clotheService;
            _mapper = mapper;
            _apiKey = Environment.GetEnvironmentVariable("OpenAiApiKey");
        }

        public async Task<List<Clothe>> GenerateSuggestedLook(Guid userId)
        {
            var user = await _userService.GetUserById(userId);
            var mapperUserDto = _mapper.Map<AiUserDto>(user.Data);

            var clothesCategories = user.Data.garderobe.ClothesCategory;

            var groupedGarderobe = new Dictionary<string, List<object>>();

            foreach (var category in mapperUserDto.userGarderobe.categories)
            {
                var type = clothesCategories.FirstOrDefault(cat => cat.Name == category.Key)?.CategoryType.ToString().ToLower();
                if (type == null) continue;

                if (!groupedGarderobe.ContainsKey(type))
                    groupedGarderobe[type] = new List<object>();

                groupedGarderobe[type].AddRange(category.Value.Select(clothe => new
                {
                    clotheId = clothe.clotheId,
                    image_path = clothe.image_path
                }));
            }

            var payload = new
            {
                userPersona = mapperUserDto.persona.ToString(),
                garderobe = groupedGarderobe
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            Console.WriteLine($"Payload like this {jsonPayload}");

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var requestUrl = "http://ai-service:8000/analyze-garderobe";

            var response = await _httpClient.PostAsync(requestUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"AI service call failed with status: {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var aiResponse = JsonSerializer.Deserialize<AiSuggestionResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (aiResponse == null || aiResponse.suggested_combination == null)
            {
                throw new Exception("Invalid response from AI service");
            }

            var suggestedImageIds = aiResponse.suggested_combination.Select(item => item.clotheId).ToList();

            var suggestedClothes = new List<Clothe>();
            foreach (var id in suggestedImageIds)
            {
                var clothe = await _clotheService.GetClotheById(Guid.Parse(id));
                if (clothe != null)
                {
                    suggestedClothes.Add(clothe);
                }
            }

            return suggestedClothes;
        }
    }
}

