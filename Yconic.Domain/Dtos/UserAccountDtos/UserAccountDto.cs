using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserAccountDtos
{
    public class UserAccountDto
    {
        public int id { get; set; }
        public Guid userId { get; set; }
        public string? phoneNumber { get; set; }
        public bool isPrivate { get; set; }
        public bool emailVerified { get; set; }
        public bool phoneVerified { get; set; }
    }
}