using IdentityServer4.Validation;
using IdentityService.Api;
using IdentityService.Api.IdentityConfiguration;
using IdentityService.Api.Mapper.Profiles;
using IdentityService.Entity;
using IdentityService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityService", Version = "v1" });
            });

            services.AddRouting(routeOption => routeOption.LowercaseUrls = true);

            services.AddCors(options =>
                options.AddPolicy(
                    "CorsPolicy",
                    b => b.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .Build()));

            services
                    .AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiScopes(ScopeConfig.Scopes)
                    .AddInMemoryClients(ClientConfig.Clients)
                    .AddInMemoryIdentityResources(ResourceConfig.IdentityResources);
            
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Identity")));
            services.AddAutoMapper(typeof(UserProfile));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICustomTokenRequestValidator, CustomTokenRequestValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService v1"));
                //IdentityModelEventSource.ShowPII = true;
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityServer();
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}