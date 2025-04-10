using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class UserPersonalPatchDto
    {
        public string? username { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? bio { get; set; }

    }
}
