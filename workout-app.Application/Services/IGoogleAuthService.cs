using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace workout_app.Application.Services
{
    public interface IGoogleAuthService
    {
        Task<string> SendAuthRequest(CancellationToken cancellationToken);
        Task<UserCredential> GetToken(string code, string state, CancellationToken cancellationToken);
    }
}
