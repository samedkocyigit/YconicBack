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
        public string Url { get; set; } // URL or file path of the photo
        public Guid ClotheId { get; set; } // Foreign key
        [JsonIgnore]
        public Clothe? Clothe { get; set; } // Navigation property
    }

}
