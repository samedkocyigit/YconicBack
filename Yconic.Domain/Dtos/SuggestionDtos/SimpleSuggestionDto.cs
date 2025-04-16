using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.SuggestionDtos
{
    public class SimpleSuggestionDto
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public string mainUrlPhoto { get; set; }
    }
}
