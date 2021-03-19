using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resource_api1")
                {
                    Scopes = { "api1.read","api.write" }
                },
                new ApiResource("resource_api2")
                {
                    Scopes = { "api2.read","api2.write" }
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.read"),
                new ApiScope("api1.write"),
                new ApiScope("api2.read"),
                new ApiScope("api2.write"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 web app",
                    ClientSecrets = new List<Secret> { new("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1.read" }
                },
                new Client
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 web app",
                    ClientSecrets = new List<Secret> { new("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1.read", "api2.read", "api2.write" }
                },
            };
        }
    }
}
