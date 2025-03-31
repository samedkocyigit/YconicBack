using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Dtos.PersonaDtos
{
    public class PersonaDto
    {
        public Guid id { get; set; }
        public Personas usertype { get; set; }
        public Guid userId { get; set; }
    }
}
