using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.GarderobeDtos;
using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Dtos.UserDtos
{
    public class UserPublicDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public bool isPrivate { get; set; }
        public string? profilePhoto { get; set; }
        public decimal? weight { get; set; }
        public decimal? height { get; set; }
        public int? followerCount { get; set; }
        public int? followingCount { get; set; }
        public string? bio { get; set; }
        public GarderobeDto? garderobe { get; set; }
        public List<SharedLookDto>? sharedLooks { get; set; } = new List<SharedLookDto>();
        public List<UserMiniDto>? followers { get; set; } = new List<UserMiniDto>();
        public List<UserMiniDto>? following { get; set; } = new List<UserMiniDto>();
    }
}
