using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ReviewDtos
{
    public class CreateSharedLookReviewDto
    {
        public Guid ReviewerUserId { get; set; }
        public Guid ReviewedSharedLookId { get; set; }
        public string Review { get; set; }
    }
}
