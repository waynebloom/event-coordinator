using System;

namespace present.Models {
    public class LoginAttempt {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginAttempt(string email, string password) {
            Email = email;
            Password = password;
        }

        public LoginAttempt() {
            Email = "";
            Password = "";
        }
    }
}
