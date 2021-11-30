using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using present.Models;
using present.ViewModels;
using present.Services;

namespace present.Views {
    public partial class AttendingEventDetailPage : ContentPage {
        EventDetailViewModel ViewModel;

        public AttendingEventDetailPage(EventDetailViewModel viewModel) {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;
        }

        public AttendingEventDetailPage() {
            InitializeComponent();

            var item = new Event {
                Title = "Title",
                Day = "Day",
                StartTime = "00:00AM",
                EndTime = "00:00AM",
                Location = "Location"
            };

            ViewModel = new EventDetailViewModel(item);
            BindingContext = ViewModel;
        }

        async void Leave_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "LeaveEvent", ViewModel.Item);
            await DisplayAlert("Left roster", $"You have left the roster of {ViewModel.Item.Title}.", "OK");
            await Navigation.PopModalAsync();
        }
    }
}