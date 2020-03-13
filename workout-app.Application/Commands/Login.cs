using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Core.Domain.Helpers;
using workout_app.Core.Domain.User;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Commands
{
    public static class Login
    {
        public class LoginCommand : IRequest<User>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class LoginHandler : IRequestHandler<LoginCommand, User>
        {
            private readonly WorkoutAppDbContext _dbContext;
            private readonly IConfiguration _configuration;

            public LoginHandler(WorkoutAppDbContext dbContext, IConfiguration configuration)
            {
                _dbContext = dbContext;
                _configuration = configuration;
            }
            public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (user == null)
                    throw new NotFoundRuleValidationException("User does not exist");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:App").ToString());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return user;

            }
        }

        public class LoginCommandValidator : AbstractValidator<LoginCommand>
        {
            public LoginCommandValidator()
            {
                RuleFor(x => x.Username)
                    .Length(8, 25)
                    .NotEmpty();

                //RuleFor(x => x.Password)
                //    .Length(8, 50)
                //    .NotEmpty();
            }
        }
    }
}
