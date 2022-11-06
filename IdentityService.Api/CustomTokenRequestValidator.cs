using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityService.Api
{
    public class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomTokenRequestValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            string givenName = _httpContextAccessor.HttpContext.Request.Form["GivenName"].ToString();
            string surname = _httpContextAccessor.HttpContext.Request.Form["Surname"].ToString();
            string role = _httpContextAccessor.HttpContext.Request.Form["Role"].ToString();

            context.Result.ValidatedRequest.ClientClaims.Add(new Claim("given_name", givenName));
            context.Result.ValidatedRequest.ClientClaims.Add(new Claim("surname", surname));
            context.Result.ValidatedRequest.ClientClaims.Add(new Claim("role", role));

            return Task.FromResult(0);
        }
    }
}