using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Core.Domain.Helpers;
using workout_app.Core.Domain.User;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Commands
{
    public static class RegisterUser
    {
        public class RegisterUserCommand : IRequest<User>
        {
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string RepeatedPassword { get; set; }
        }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, User>
        {
            private readonly WorkoutAppDbContext _dbContext;

            public RegisterUserHandler(WorkoutAppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                bool userExists = await _dbContext.Users.AnyAsync(x => x.Username == request.Username, cancellationToken);

                if(userExists)
                    throw new BusinessRuleValidationException("User with this name already exists");

                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                };

                var createdUser = await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return createdUser.Entity;
            }
        }
    }
}
