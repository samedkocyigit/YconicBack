using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Dtos.User;
using Yconic.Domain.Models;

namespace Yconic.Application.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.surname,opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.weight, opt => opt.MapFrom(src => src.Weight))
                .ForMember(dest => dest.height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.phoneNumeber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.userPersonaId, opt => opt.MapFrom(src => src.UserPersonaId))
                .ForMember(dest => dest.userGarderobeId, opt => opt.MapFrom(src => src.UserGarderobeId))
                .ForMember(dest => dest.garderobe,opt => opt.MapFrom(src => src.UserGarderobe))
                .ForMember(dest => dest.persona, opt => opt.MapFrom(src=> src.UserPersona));
        }
    }
}
