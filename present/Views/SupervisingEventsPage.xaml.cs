using System;
using System.Collections.Generic;
using Xamarin.Forms;

using present.Models;
using present.Views;
using present.ViewModels;

namespace present.Views {
    public partial class SupervisingEventsPage : ContentPage {
        SupervisingEventsViewModel viewModel;

        public SupervisingEventsPage() {
            InitializeComponent();

            BindingContext = viewModel = new SupervisingEventsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args) {
            var layout = (BindableObject)sender;
            var item = (Event)layout.BindingContext;
            await Navigation.PushAsync(new SupervisingEventDetailPage(new EventDetailViewModel(item)));
        }

        async void Create_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new CreateEventPage()));
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}
