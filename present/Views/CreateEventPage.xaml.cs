using System;
using System.Collections.Generic;
using Xamarin.Forms;

using present.Models;

namespace present.Views {
    public partial class CreateEventPage : ContentPage {
        public Event Item { get; set; }

        public CreateEventPage() {
            InitializeComponent();

            Item = new Event();

            BindingContext = this;
        }

        async void Create_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "CreateEvent", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e) {
            await Navigation.PopModalAsync();
        }
    }
}
