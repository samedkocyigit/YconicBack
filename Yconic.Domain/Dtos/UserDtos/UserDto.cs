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
        public string username { get; set; }
        public string? profilePhoto { get; set; }
        public string? name { get; set; }
        public string? bio { get; set; }
        public string? surname { get; set; }
        public bool isPrivate { get; set; }
        public int? followerCount { get; set; }
        public int? followingCount { get; set; }
    }
}