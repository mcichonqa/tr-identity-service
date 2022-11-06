using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService.Api.IdentityConfiguration
{
    public static class ClientConfig
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "dev-user",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 360,
                    IdentityTokenLifetime = 360,
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedScopes = { "openid", "profile", "read", "delete", "write" },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientClaimsPrefix = string.Empty
                }
            };
    }
}