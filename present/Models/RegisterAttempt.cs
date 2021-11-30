using System;

namespace present.Models {
    public class RegisterAttempt {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RegisterAttempt(string email, string password, string confirmPassword, string firstName, string lastName) {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            FirstName = firstName;
            LastName = lastName;
        }

        public RegisterAttempt() {
            Email = "";
            Password = "";
            ConfirmPassword = "";
            FirstName = "";
            LastName = "";
        }
    }
}
