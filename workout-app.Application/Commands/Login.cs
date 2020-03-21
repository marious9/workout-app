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
using workout_app.Application.Mapping.Dto.User;
using workout_app.Core.Domain.Helpers;
using workout_app.Core.Domain.User;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Commands
{
    public static class Login
    {
        public class LoginCommand : IRequest<UserDto>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class LoginHandler : IRequestHandler<LoginCommand, UserDto>
        {
            private readonly WorkoutAppDbContext _dbContext;
            private readonly IConfiguration _configuration;

            public LoginHandler(WorkoutAppDbContext dbContext, IConfiguration configuration)
            {
                _dbContext = dbContext;
                _configuration = configuration;
            }
            public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (user == null)
                    throw new NotFoundRuleValidationException("User does not exist");
                
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    throw new BusinessRuleValidationException("Incorrect username or password");

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

                return new UserDto
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

            }
            
            private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
            {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
                if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
                if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

                using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i]) return false;
                    }
                }

                return true;
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
