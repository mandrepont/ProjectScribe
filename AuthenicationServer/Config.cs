using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenicationServer
{
    /// <summary>
    /// Most of the code is based off of the samples in the IdenityServer4.Examples github.
    /// Slightly modified to fit my needs. 
    /// </summary>
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// The authentication server needs to know what api are open to authenticate against.
        /// Supports large multi-project/api systems.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("note_api", "Note API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("SuperSecretKey".Sha256())
                    },
                    AllowedScopes = { "note_api","openid","profile"}
                }
            };
        }

        /// <summary>
        /// Hard coded members stored in memory is just a bad idea all together, however there is a large amount of
        /// IdenityServer code in order to use external resources. For now TestUsers are suffice.
        /// </summary>
        /// <returns>All users available.</returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Michael",
                    Password = "testing",
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Admin",
                    Password = "sudopassword",
                }
            };
        }
    }
}
