using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.Ai
{
    public class AiResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }

}



