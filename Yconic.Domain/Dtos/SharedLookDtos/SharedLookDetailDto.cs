using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.LikesDtos;
using Yconic.Domain.Dtos.ReviewDtos;
using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.SharedLookDtos
{
    public class SharedLookDetailDto
    {
        public Guid id { get; set; }
        public string mainUrlPhoto { get; set; }
        public string description { get; set; }
        public string username { get; set; }
        public string userPhoto { get; set; }
        public DateTime createdAt { get; set; }
        public List<Clothe> photos { get; set; }
        public int likesCount { get; set; }
        public int reviewCount { get; set; }
        public List<LikeDto> likes { get; set; }
        public List<ReviewDto> reviews { get; set; }
    }
}
