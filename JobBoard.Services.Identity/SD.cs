using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace JobBoard.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("jobBoard", "JobBoard Server"),
                new ApiScope(name: "read", "Read all data"),
                new ApiScope(name: "write", "Write all data"),
                new ApiScope(name: "delete", "Delete all data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"read", "write", "profile"}
                },
                new Client
                {
                    ClientId="jobBoard",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={"https://localhost:44317/signin-oidc"},
                    PostLogoutRedirectUris={"https://localhost:44317/signout-callback-oidc"},
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "jobBoard"
                    }
                },
            };
    }
}
