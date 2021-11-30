using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

using present.Views;
using present.Services;
using present.Models;

namespace present.ViewModels {
    public class LandingViewModel : BaseViewModel {
        public LoginAttempt CurrentLoginAttempt { get; set; }
        public RegisterAttempt CurrentRegisterAttempt { get; set; }

        public LandingViewModel() {
            CurrentLoginAttempt = new LoginAttempt();
            CurrentRegisterAttempt = new RegisterAttempt();

            MessagingCenter.Subscribe<LandingPage, LoginAttempt>(this, "Login", async (obj, login) => {
                User user = await DbService.FindLogin(login.Email);
                if(user != null) {
                    if (user.Password == login.Password)
                        obj.LoginSuccess(user);
                    else
                        obj.LoginFail("Incorrect password.");
                }
                else {
                    obj.LoginFail("A user with that email does not exist.");
                }
            });

            MessagingCenter.Subscribe<RegisterPage, RegisterAttempt>(this, "Register", async (obj, register) => {
                User user = await DbService.FindLogin(register.Email);

                // if user with entered email is not found and the passwords match, create a new user in the db
                // else registration fails
                if (user == null) {
                    if (register.Password.Equals(register.ConfirmPassword)) {
                        user = new User(
                            register.Email,
                            register.Password,
                            register.FirstName,
                            register.LastName
                        );

                        user.UserId = string.Format("{0:000000}", await DbService.GetNextUserId());
                        await DbService.SaveItemAsync(user);

                        obj.RegisterSuccess(user);
                    }
                    else {
                        obj.RegisterFail("Password entries do not match.");
                    }
                }
                else {
                    obj.RegisterFail("A user with that email already exists.");
                }
            });
        }
    }
}
