using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class UserAccountPatchDto
    {
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime? birthday  { get; set; }
        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        public int? personaType { get; set; }
    }
}
