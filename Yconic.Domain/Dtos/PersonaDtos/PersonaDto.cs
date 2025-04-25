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
        public string usertype { get; set; }
        public Guid userTypeId { get; set; }
        public string username { get; set; }
        public Guid userId { get; set; }
    }
}