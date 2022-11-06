using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService.Api.IdentityConfiguration
{
    public static class ScopeConfig
    {
        public static IEnumerable<ApiScope> Scopes =>
            new List<ApiScope>()
            {
                new ApiScope(name: "read"),
                new ApiScope(name: "write"),
                new ApiScope(name: "delete")
            };
    }
}