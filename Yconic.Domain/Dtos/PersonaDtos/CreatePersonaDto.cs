using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.PersonaDtos
{
    public class CreatePersonaDto
    {
        public Guid personaTypeId { get; set; }
        public Guid userId { get; set; }
    }
}