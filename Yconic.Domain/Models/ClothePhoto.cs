using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class ClothePhoto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid ClotheId { get; set; } 
        [JsonIgnore]
        public Clothe? Clothe { get; set; } 
    }

}
