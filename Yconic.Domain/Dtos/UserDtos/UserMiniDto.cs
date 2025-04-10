using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class UserMiniDto
    {
        public required Guid id { get; set; }
        public required string username { get; set; }
        public required bool isPrivate { get; set; }
        public string? profilePhoto { get; set; }
    }

}
