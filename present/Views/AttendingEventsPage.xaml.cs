using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using present.Models;
using present.Views;
using present.ViewModels;

namespace present.Views {
    public partial class AttendingEventsPage : ContentPage {
        AttendingEventsViewModel viewModel;

        public AttendingEventsPage() {
            InitializeComponent();

            BindingContext = viewModel = new AttendingEventsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args) {
            var layout = (BindableObject)sender;
            var item = (Event)layout.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new AttendingEventDetailPage(new EventDetailViewModel(item))));
        }

        async void JoinEvent_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new NavigationPage(new JoinEventPage()));
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}