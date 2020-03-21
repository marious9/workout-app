using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using workout_app.Application.Mapping.Dto.User;
using workout_app.Core.Domain.Helpers;
using workout_app.Core.Domain.User;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Commands
{
    public static class RegisterUser
    {
        public class RegisterUserCommand : IRequest<UserDto>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string RepeatedPassword { get; set; }
        }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
        {
            private readonly WorkoutAppDbContext _dbContext;

            public RegisterUserHandler(WorkoutAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                User dbUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

                if (dbUser != null)
                {
                    if (dbUser.Username.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase))
                        throw new BusinessRuleValidationException("User with this name already exists");
                    if (dbUser.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase))
                        throw new BusinessRuleValidationException("User with this email already exists");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var createdUser = await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new UserDto
                {
                    Username = createdUser.Entity.Username,
                    FirstName = createdUser.Entity.FirstName,
                    LastName = createdUser.Entity.LastName
                };
            }
            
            private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
        }

        public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
        {
            public RegisterUserCommandValidator()
            {
                RuleFor(x => x.Username)
                    .Length(8, 25)
                    .NotEmpty();

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("Invalid email format");
                    

                RuleFor(x => x.FirstName)
                    .Length(2, 50)
                    .NotEmpty();
                
                RuleFor(x => x.LastName)
                    .Length(2, 50)
                    .NotEmpty();

                RuleFor(x => x.Password)
                    .Length(8, 50)
                    .NotEmpty();

                RuleFor(x => x.Password)
                    .Equal(x => x.RepeatedPassword).WithMessage("Passwords do not match");
            }
        }
    }
}
