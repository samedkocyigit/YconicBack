using AutoMapper;
using Yconic.Domain.Dtos.Ai;
using Yconic.Domain.Dtos.ClotheCategoryDtos;
using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Dtos.ClothePhotoDtos;
using Yconic.Domain.Dtos.GarderobeDtos;
using Yconic.Domain.Dtos.PersonaDtos;
using Yconic.Domain.Dtos.SuggestionDtos;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Dtos.UserDtos;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;

namespace Yconic.Application.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {

            // For ai
            CreateMap<Clothe, ClotheItemDto>()
                .ForMember(dest => dest.clotheId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.image_path, opt => opt.MapFrom(src => src.MainPhoto))
                .ForMember(dest => dest.categoryId,opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<Garderobe, AiGarderobeDto>()
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src =>
                    src.ClothesCategory
                        .OrderByDescending(c=> c.CreatedAt)
                        .ToDictionary(
                            cat => cat.Name,
                            cat => cat.Clothes
                                .OrderByDescending(cl=> cl.CreatedAt)
                                .ToList()
                    )
                ));

            CreateMap<UserDto, AiUserDto>()
                .ForMember(dest => dest.persona, opt => opt.MapFrom(src => src.persona.usertype))
                .ForMember(dest => dest.userGarderobe, opt => opt.MapFrom(src => src.garderobe))
                .ReverseMap();

            //For user
            CreateMap<User, UserMiniDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.isPrivate, opt=> opt.MapFrom(src=> src.IsPrivate))
                .ForMember(dest => dest.profilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto));

            CreateMap<User, UserPublicDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.profilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto))
                .ForMember(dest => dest.bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.isPrivate, opt => opt.MapFrom(src => src.IsPrivate))
                .ForMember(dest => dest.weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.followerCount, opt => opt.MapFrom(src => src.Followers.Count(f => f.IsFollowing)))
                .ForMember(dest => dest.followingCount, opt => opt.MapFrom(src => src.Following.Count(f => f.IsFollowing)))
                .ForMember(dest => dest.garderobe, opt => opt.MapFrom(src => src.UserGarderobe))
                .ForMember(dest => dest.suggestions, opt => opt.MapFrom(src => src.Suggestions.OrderByDescending(s => s.CreatedAt)))
                .ForMember(dest => dest.followers, opt => 
                                            opt.MapFrom(src => src.Followers
                                                .Where(f => f.IsFollowing)
                                                .Select(f => f.Follower)))
                .ForMember(dest => dest.following, opt =>
                                            opt.MapFrom(src => src.Following
                                                .Where(f => f.IsFollowing)
                                                .Select(f => f.Followed)));

            CreateMap<User,UserDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.surname,opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.birthday, opt => opt.MapFrom(opt => opt.Birthday))
                .ForMember(dest => dest.weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.profilePhoto, opt => opt.MapFrom(src => src.ProfilePhoto))
                .ForMember(dest => dest.bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.isPrivate, opt => opt.MapFrom(src => src.IsPrivate))
                .ForMember(dest => dest.followerCount,opt => opt.MapFrom(src => src.Followers.Count(f => f.IsFollowing)))
                .ForMember(dest => dest.followingCount,opt => opt.MapFrom(src => src.Following.Count(f => f.IsFollowing)))
                .ForMember(dest => dest.followers, opt => 
                                            opt.MapFrom(src => src.Followers
                                                .Where(f => f.IsFollowing)
                                                .Select(f => f.Follower)))
                .ForMember(dest => dest.following, opt =>
                                            opt.MapFrom(src => src.Following
                                                .Where(f => f.IsFollowing)
                                                .Select(f => f.Followed)))
                .ForMember(dest => dest.recievedFollowRequest, opt => opt.MapFrom(src => src.FollowRequestsReceived.Where(f=> f.RequestStatus == RequestStatus.Pending).Select(f=> f.Requester)))
                .ForMember(dest => dest.sentFollowRequest, opt => opt.MapFrom(src => src.FollowRequestsSent.Where(f=>f.RequestStatus == RequestStatus.Pending).Select(f => f.TargetUser)))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.userPersonaId, opt => opt.MapFrom(src => src.UserPersonaId))
                .ForMember(dest => dest.userGarderobeId, opt => opt.MapFrom(src => src.UserGarderobeId))
                .ForMember(dest => dest.garderobe,opt => opt.MapFrom(src => src.UserGarderobe))
                .ForMember(dest => dest.persona, opt => opt.MapFrom(src=> src.UserPersona))
                .ForMember(dest => dest.suggestions, opt => opt.MapFrom(src => src.Suggestions.OrderByDescending(s=> s.CreatedAt)));

            //For garderobe
            CreateMap<Garderobe, GarderobeDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src => src.ClothesCategory.OrderByDescending(cl=> cl.CreatedAt)))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            //For clothe-category
            CreateMap<ClotheCategory, ClotheCategoryDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.garderobeId, opt => opt.MapFrom(src => src.GarderobeId))
                .ForMember(dest => dest.categoryType, opt => opt.MapFrom(src => src.CategoryType))
                .ForMember(dest => dest.clothes, opt => opt.MapFrom(src => src.Clothes.OrderByDescending(cl => cl.CreatedAt)))
                .ReverseMap();

            //For clothe
            CreateMap<Clothe,ClotheDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.mainPhoto, opt => opt.MapFrom(src => src.MainPhoto))
                .ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.photos, opt => opt.MapFrom(src => src.Photos.OrderByDescending(p => p.CreatedAt)))
                .ReverseMap();

            //For clothe-photo
            CreateMap<ClothePhoto, ClothePhotoDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.clotheId, opt => opt.MapFrom(src => src.ClotheId))
                .ReverseMap();

            //For persona
            CreateMap<Persona, PersonaDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.usertype, opt => opt.MapFrom(src => src.Usertype))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            //For suggestion
            CreateMap<Suggestion, SuggestionDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.suggestedLook, opt => opt.MapFrom(src => src.SuggestedLook.OrderByDescending(cl => cl.CreatedAt)))
                .ReverseMap();
        }
    }
}
