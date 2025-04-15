using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ReviewDtos
{
    public class PatchSharedLookReviewDto
    {
        public Guid id { get; set; }
        public string  newReview { get; set; }
    }
}
