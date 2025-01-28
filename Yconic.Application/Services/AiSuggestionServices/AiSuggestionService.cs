using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.AiSuggestionServices
{
    public class AiSuggestionService : IAiSuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiSuggestionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["AppSettings:OpenAiApiKey"];
        }

        public async Task<List<Clothe>> GenerateSuggestedLook(Persona persona, User user, object otherParameters)
        {
            // Extract all clothes from the user's garderobe
            var clothesInGarderobe = user.UserGarderobe.ClothesCategory
                .SelectMany(cc => cc.Clothes)  // Flatten the list of clothes from all categories
                .ToList();

            // Create a string of available clothes in the garderobe for the prompt
            var clothesList = string.Join(", ", clothesInGarderobe.Select(c => $"{c.Name} ({c.Category.Name})"));

            // Create the prompt for the AI
            string prompt = $"Generate a clothing suggestion for a user with the persona '{persona.Usertype}' and additional parameters: {JsonSerializer.Serialize(otherParameters)}. " +
                            $"The user has the following clothes in their garderobe: {clothesList}. " +
                            "Provide clothing suggestions based on what they already own.";

            var request = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 100,
                temperature = 0.7
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
            requestMessage.Headers.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var aiResponse = JsonSerializer.Deserialize<AiResponse>(responseContent);

            // Parse the AI response into clothing suggestions
            var suggestedClothes = ParseAiSuggestions(aiResponse.Choices.First().Text, clothesInGarderobe);
            return suggestedClothes;
        }



        private List<Clothe> ParseAiSuggestions(string aiText, List<Clothe> clothesInGarderobe)
        {
            var clothes = new List<Clothe>();

            // Split the AI response into individual suggestions
            var suggestions = aiText.Split("\n");
            foreach (var suggestion in suggestions)
            {
                var parts = suggestion.Split(" - ");
                if (parts.Length == 2)
                {
                    var suggestedClothName = parts[0].Trim();
                    // Check if the suggested clothing is in the user's garderobe
                    var suggestedCloth = clothesInGarderobe.FirstOrDefault(c => c.Name.Equals(suggestedClothName, StringComparison.OrdinalIgnoreCase));

                    if (suggestedCloth != null)
                    {
                        clothes.Add(suggestedCloth);
                    }
                }
            }

            return clothes;
        }


    }
}
