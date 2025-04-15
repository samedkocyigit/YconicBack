using Yconic.Domain.Dtos.GarderobeDtos;
using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Dtos.SharedLookDtos;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public UserRoles role { get; set; }
        public bool isPrivate { get; set; }
        public int? age { get; set; }
        public string? profilePhoto { get; set; }
        public decimal? weight { get; set; }
        public decimal? height { get; set; }
        public string? phoneNumber { get; set; }
        public int? followerCount { get; set; }
        public int? followingCount { get; set; }
        public string? bio { get; set; }
        public DateTime? birthday {get; set;}
        public Guid? userPersonaId { get; set; }
        public Guid userGarderobeId { get; set; }
        public GarderobeDto? garderobe { get; set; }
        public PersonaDto? persona { get; set; }
        public List<SimpleSuggestionDto>? suggestions {get; set;} = new List<SimpleSuggestionDto>();
        public List<SharedLookDto>? sharedLooks { get; set; } = new List<SharedLookDto>();
        public List<UserMiniDto>? followers { get; set; } = new List<UserMiniDto>();
        public List<UserMiniDto>? following { get; set; } = new List<UserMiniDto>();
        public List<UserMiniDto>? recievedFollowRequest {get; set;} =new List<UserMiniDto>();
        public List<UserMiniDto>? sentFollowRequest {get; set;} =new List<UserMiniDto>();
    }
}
