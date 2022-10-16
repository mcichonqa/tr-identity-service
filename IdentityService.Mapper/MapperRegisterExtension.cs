using IdentityService.Mapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Mapper
{
    public static class MapperRegisterExtension
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));

            return services;
        }
    }
}