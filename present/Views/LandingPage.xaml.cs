using System;
using System.Collections.Generic;
using Xamarin.Forms;

using present.Models;
using present.ViewModels;

namespace present.Views {
    public partial class LandingPage : ContentPage {
        public LandingViewModel ViewModel { get; set; }

        public LandingPage() {
            InitializeComponent();

            BindingContext = ViewModel = new LandingViewModel();
        }

        public void LoginSuccess(User user) {
            MessagingCenter.Send(this, "LoginSuccess", user);
            Application.Current.MainPage = new MainPage();
            Console.WriteLine("ALT: login success");
        }

        public async void LoginFail(string message) {
            await DisplayAlert("Login failed", message, "OK");
        }

        void Login_Clicked(object sender, EventArgs e) {
            Console.WriteLine(ViewModel.CurrentLoginAttempt.Email + ": " + ViewModel.CurrentLoginAttempt.Password);
            MessagingCenter.Send(this, "Login", ViewModel.CurrentLoginAttempt);
        }

        async void Register_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage(ViewModel)));
        }
    }
}
