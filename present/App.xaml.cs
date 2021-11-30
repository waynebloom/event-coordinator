using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using present.Views;
using present.Models;

namespace present {
    public partial class App : Application {
        public static User CurrentUser { get; set; }

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
            CurrentUser = new User();

            MessagingCenter.Subscribe<LandingPage, User>(this, "LoginSuccess", (obj, user) => {
                CurrentUser = user;
            });

            MessagingCenter.Subscribe<RegisterPage, User>(this, "RegisterSuccess", (obj, user) => {
                CurrentUser = user;
            });
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
