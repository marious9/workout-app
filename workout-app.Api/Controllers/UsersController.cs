using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using workout_app.Application.Commands;
using workout_app.Application.Queries.Google;
using workout_app.Application.Services;
using System.Threading;

namespace workout_app.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IGoogleAuthService _googleAuthService;

        public UsersController(IMediator mediator, IGoogleAuthService googleAuthService)
        {
            _mediator = mediator;
            _googleAuthService = googleAuthService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login.LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser.RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpGet("google")]
        public async Task<IActionResult> Google(CancellationToken cancellationToken)
        {
            //var query = new SendAuthRequest.SendAuthRequestQuery();
            //var result = await _mediator.Send(query);

            var result = await _googleAuthService.SendAuthRequest(cancellationToken)

            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpGet("getToken")]
        public async Task<IActionResult> GetToken(string error, string state, string code, CancellationToken cancellationToken)
        {
            //var query = new GetToken.GetTokenQuery(code, state);
            //var result = await _mediator.Send(query);

            var result = await _googleAuthService.GetToken(code, state, cancellationToken);

            return Ok(result);
        }
    }
}
