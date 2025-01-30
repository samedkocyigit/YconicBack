using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.Ai
{
    public class Choice
    {
        [JsonPropertyName("message")]
        public AiMessage Message { get; set; }
    }

}
