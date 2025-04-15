using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.LikeDtos
{
    public class CreateSharedLookLikeDto
    {
        public Guid userId { get; set; }
        public Guid sharedLookId { get; set; }

    }
}
