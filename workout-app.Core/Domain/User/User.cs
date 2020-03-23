using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain.User
{
    public class User : IdentityUser
    {
        public ICollection<Training> Trainings { get; set; }
    }
}
