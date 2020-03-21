using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Drive.v3;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace workout_app.Application.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private static IAuthorizationCodeFlow flow;
        private readonly IConfiguration _configuration;
        private readonly string redirectUri = string.Format($"https://localhost:44391/users/getToken");

        public GoogleAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public async Task<string> SendAuthRequest(CancellationToken cancellationToken)
        {
            IConfigurationSection googleAuthNSection =
                    _configuration.GetSection("Authentication:Google");

            var clientSecrets = new ClientSecrets
            {
                ClientId = googleAuthNSection["ClientId"],
                ClientSecret = googleAuthNSection["ClientSecret"]
            };

            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = clientSecrets,
                    Scopes = new[] { DriveService.Scope.Drive, PlusService.Scope.UserinfoEmail },
                    DataStore = new FileDataStore("WorkoutApp")
                });

            var res = await new AuthorizationCodeWebApp(flow, redirectUri, redirectUri).AuthorizeAsync("user", cancellationToken);

            return res.RedirectUri;
        }

        public async Task<UserCredential> GetToken(string code, string state, CancellationToken cancellationToken)
        {            

            string user = state.Substring(37);

            if(flow == null)
            {
                IConfigurationSection googleAuthNSection =
                    _configuration.GetSection("Authentication:Google");

                var clientSecrets = new ClientSecrets
                {
                    ClientId = googleAuthNSection["ClientId"],
                    ClientSecret = googleAuthNSection["ClientSecret"]
                };

                flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = clientSecrets,
                    Scopes = new[] { DriveService.Scope.Drive, PlusService.Scope.UserinfoEmail },
                    DataStore = new FileDataStore("WorkoutApp")
                });
            }

            var token = await flow.ExchangeCodeForTokenAsync(user, code, redirectUri, cancellationToken);
            UserCredential userCredential = new UserCredential(flow, user, token);
            return userCredential;
        }
    }
}
