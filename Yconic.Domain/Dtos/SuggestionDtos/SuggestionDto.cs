using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.SuggestionDtos
{
    public class SuggestionDto
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public string description { get; set; }
        public string mainUrlPhoto { get; set; }
        public List<Clothe> suggestedLook { get; set; }
    }
}
