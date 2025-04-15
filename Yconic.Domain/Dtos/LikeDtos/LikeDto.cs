using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.LikesDtos
{
    public class LikeDto
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public string userPhoto { get; set; }
        public string username { get; set; }
    }
}
