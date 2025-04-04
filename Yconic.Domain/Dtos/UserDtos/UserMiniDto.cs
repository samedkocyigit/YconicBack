using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class UserMiniDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string? profilePhoto { get; set; }
    }

}
