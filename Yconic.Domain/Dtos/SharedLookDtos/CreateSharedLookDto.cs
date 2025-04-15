using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.SharedLookDtos
{
    public class CreateSharedLookDto
    {
        public Guid UserId { get; set; }
        public Guid SuggestionId { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
