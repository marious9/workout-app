using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using MediatR;
using workout_app.Core.Domain.Helpers;

namespace workout_app.Application.Queries.Google
{
    public static class GetToken
    {
        public class GetTokenQuery : IRequest<UserCredential>
        {
            public string Code { get; set; }
            public string State { get; set; }

            public GetTokenQuery(string code, string state)
            {
                Code = code;
                State = state;
            }
        }
        
        public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, UserCredential>
        {
            private static IAuthorizationCodeFlow _flow;           
            
            
            public async Task<UserCredential> Handle(GetTokenQuery request, CancellationToken cancellationToken)
            {                                              
                var redirectUri = string.Format($"https://localhost:44391/exercises");

                string user = request.Code.Substring(38);
                
                var token = await _flow.ExchangeCodeForTokenAsync(user, request.Code, redirectUri, cancellationToken);
                UserCredential userCredential = new UserCredential(_flow, user, token);
                return userCredential;
            }
        }
    }
}