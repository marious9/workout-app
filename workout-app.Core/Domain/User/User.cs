using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain.User
{
    public class User
    {
        public Guid Id { get; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}
