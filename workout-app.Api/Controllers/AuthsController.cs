using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using workout_app.Core.Domain.User;

namespace workout_app.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;

        public AuthsController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> LoginViaGoogle(string returnUrl)
        {
            var redirectUrl = Url.Action(string.Empty, "Exercises", new { ReturnUrl = returnUrl });
            string provider = "Google";
            var providers = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> SigninGoogle(string state, string code, string error)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect("exercises");
            }

            return BadRequest();
        }
    }
}