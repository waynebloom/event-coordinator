using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace present.Views {
    public partial class JoinEventPage : ContentPage {
        public string Code { get; set; }

        public JoinEventPage() {
            InitializeComponent();

            Code = "";
            BindingContext = this;
        }

        public async void JoinSuccess(string eventName) {
            await DisplayAlert("Joined event", string.Format("Successfully joined {0}.", eventName), "OK");
            await Navigation.PopModalAsync();
        }

        public async void JoinFail(string message) {
            await DisplayAlert("Join failed", message, "OK");
            CodeEntry.Text = "";
        }

        void Join_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "JoinEvent", Code);
        }

        async void Cancel_Clicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();
        }
    }
}
