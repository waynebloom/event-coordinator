using System;
using SQLite;

namespace present.Models {
    public class User {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string password, string firstName, string lastName) {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public User() {
            Email = "";
            Password = "";
            FirstName = "";
            LastName = "";
        }
    }
}
