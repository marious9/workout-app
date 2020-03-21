using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Drive.v3;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace workout_app.Application.Queries.Google
{
    public static class SendAuthRequest
    {
        public class SendAuthRequestQuery : IRequest<string>
        {
            
        }
        
        public class SendAuthRequestHandler : IRequestHandler<SendAuthRequestQuery, string>
        {
            private readonly IConfiguration _configuration;

            public SendAuthRequestHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<string> Handle(SendAuthRequestQuery request, CancellationToken cancellationToken)
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
                        Scopes =  new[] { DriveService.Scope.Drive, PlusService.Scope.UserinfoEmail },
                        DataStore = new FileDataStore("WorkoutApp")
                    });
                var redirectUri = string.Format($"https://localhost:44391/user/Token");
                
                var res = await new AuthorizationCodeWebApp(flow, redirectUri, redirectUri).AuthorizeAsync("user", CancellationToken.None);

                return res.RedirectUri;
            }
        }
    }
}