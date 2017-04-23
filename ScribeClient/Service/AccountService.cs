using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScribeClient.Service
{
    public class AccountService
    {
        /// <summary>
        /// Authenticates the user and retrieves that user's jwt token.
        /// </summary>
        /// <param name="username">Username of the client.</param>
        /// <param name="password">Password of the client.</param>
        /// <returns>true = authenticated
        /// false = failed.</returns>
        public static async Task<bool> Authenticate(string username, string password)
        {
            var disco = await DiscoveryClient.GetAsync(Constants.BASE_URI_AUTH);

            var tokenClient = new TokenClient(disco.TokenEndpoint, Constants.AUTH_CLIENT, Constants.AUTH_SECRET);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(username, password, Constants.API_NAME+ " openid");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return false;
            }
            Constants.AccessToken = tokenResponse.AccessToken;
            return true;
        }

        /// <summary>
        /// Used to test if the auth worked and also to get other info about the user from the user endpoint.
        /// </summary>
        /// <returns>User role.</returns>
        public static async Task<string> GetValues()
        {
            if (Constants.AccessToken == null)
                return null;
            string publicClaims = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI_API);
                client.SetBearerToken(Constants.AccessToken);
                try
                {
                    var response = await client.GetAsync("/api/user");
                    publicClaims = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }

            }

            return publicClaims;
        }
    }
}
