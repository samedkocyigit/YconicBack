using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.Ai;
using Yconic.Domain.Models;

namespace Yconic.Application.Services.AiSuggestionServices
{
    public class AiSuggestionService : IAiSuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiSuggestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("OpenAiApiKey");
        }

        public async Task<List<Clothe>> GenerateSuggestedLook(Persona persona, User user, object otherParameters)
        {
            var clothesInGarderobe = user.UserGarderobe.ClothesCategory
                .SelectMany(cc => cc.Clothes)
                .Select(c => new { c.Id, c.Name, Category = c.Category.Name }) // Send structured data
                .ToList();

            string prompt = $@"
                                Generate a full outfit suggestion (including tops, bottoms, shoes, and accessories) for a user with the persona '{persona.Usertype}'.
                                Use only the provided clothing items by referencing their IDs.  
                                Return only a JSON array of suggested clothing IDs with no extra text.

                                User's available clothes (JSON format): {JsonSerializer.Serialize(clothesInGarderobe)}
                                ";


            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a fashion stylist. Always return a JSON array of clothing IDs." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 150,
                temperature = 0.7
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            requestMessage.Headers.Add("Authorization", $"Bearer {_apiKey}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"OpenAI API error: {response.StatusCode} - {responseContent}");
            }

            var aiResponse = JsonSerializer.Deserialize<AiResponse>(responseContent);
            var aiResponseText = aiResponse.Choices.First().Message.Content.Trim();

            // Ensure the response is valid JSON by removing unexpected characters
            if (aiResponseText.StartsWith("```json"))
            {
                aiResponseText = aiResponseText.Replace("```json", "").Replace("```", "").Trim();
            }

            var suggestedIds = JsonSerializer.Deserialize<List<Guid>>(aiResponseText);

            // Fetch the clothes from DB based on AI's suggestions
            return user.UserGarderobe.ClothesCategory
                .SelectMany(cc => cc.Clothes)
                .Where(c => suggestedIds.Contains(c.Id))
                .ToList();
        }


        private List<Clothe> ParseAiSuggestions(string aiText, List<Clothe> clothesInGarderobe)
        {
            var clothes = new List<Clothe>();

            var normalizedText = aiText.Trim().ToLower();
            var suggestions = normalizedText.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var suggestion in suggestions)
            {
                var suggestedCloth = clothesInGarderobe.FirstOrDefault(c =>
                    suggestion.Contains(c.Name.ToLower(), StringComparison.OrdinalIgnoreCase));

                if (suggestedCloth != null)
                {
                    clothes.Add(suggestedCloth);
                }
            }

            return clothes;
        }
    }
}

