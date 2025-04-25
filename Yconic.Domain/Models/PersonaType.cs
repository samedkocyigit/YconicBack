using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class PersonaType : BaseClass
    {
        public string Name { get; set; }
        public ICollection<Persona>? Personas { get; set; } = new List<Persona>();
    }
}