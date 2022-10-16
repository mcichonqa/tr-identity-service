using AutoMapper;
using IdentityService.Contract;
using IdentityService.Entity.Models;

namespace IdentityService.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationInfoDto, User>()
                .ForMember(u => u.Username, m => m.MapFrom(r => r.Username))
                .ForMember(u => u.Password, m => m.MapFrom(r => r.Password))
                .ForMember(u => u.Email, m => m.MapFrom(r => r.Email))
                .ForMember(u => u.GivenName, m => m.MapFrom(r => r.GivenName))
                .ForMember(u => u.Surname, m => m.MapFrom(r => r.Surname))
                .ForMember(u => u.Gender, m => m.MapFrom(r => r.Gender))
                .ForMember(u => u.BirthDate, m => m.MapFrom(r => r.BirthDate))
                .ForMember(u => u.Role, m => m.Ignore());

            CreateMap<User, RegistrationInfoDto>();
        }
    }
}