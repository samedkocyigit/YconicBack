using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class Clothe : BaseClass
    {
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MainPhoto { get; set; }

        public Guid CategoryId { get; set; }
        public ClotheCategory? Category { get; set; }

        public ICollection<ClothePhoto>? Photos { get; set; } = new List<ClothePhoto>();
    }
}