using System;
using System.Collections.Generic;
using Xamarin.Forms;

using present.ViewModels;
using present.Models;
namespace present.Views {
    public partial class RegisterPage : ContentPage {
        public LandingViewModel ViewModel { get; set; }

        public RegisterPage(LandingViewModel viewModel) {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;
        }

        public void RegisterSuccess(User user) {
            MessagingCenter.Send(this, "RegisterSuccess", user);
            Application.Current.MainPage = new MainPage();
            Console.WriteLine("ALT: register success");
        }

        public async void RegisterFail(string message) {
            await DisplayAlert("Registration failed", message, "OK");
        }

        void Register_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "Register", ViewModel.CurrentRegisterAttempt);
        }
    }
}
