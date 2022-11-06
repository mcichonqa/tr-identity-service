using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService.Api.IdentityConfiguration
{
    public static class ResourceConfig
    {
        public static readonly IReadOnlyCollection<IdentityResource> IdentityResources = new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}