using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Dtos.ReviewDtos
{
    public class ReviewDto
    {
        public Guid id { get; set; }
        public Guid reviewerUserId { get; set; }
        public string review { get; set; }
        public string userPhoto { get; set; }
        public string username { get; set; }
        public DateTime createdAt { get; set; }
    }
}
