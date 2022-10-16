using AutoMapper;
using IdentityService.Contract;
using IdentityService.Entity.Models;

namespace IdentityService.Api.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationInfoDto, User>();
        }
    }
}