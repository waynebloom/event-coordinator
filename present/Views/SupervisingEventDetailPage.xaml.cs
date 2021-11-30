using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using present.Models;
using present.ViewModels;
using present.Services;

namespace present.Views {
    public partial class SupervisingEventDetailPage : ContentPage {
        EventDetailViewModel ViewModel;

        public SupervisingEventDetailPage(EventDetailViewModel viewModel) {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;
        }

        public SupervisingEventDetailPage() {
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

        async void Save_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "SaveEvent", ViewModel.Item);
            await Navigation.PopAsync();
        }

        async void Retire_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "RetireEvent", ViewModel.Item.Id);
            await Navigation.PopAsync();
        }
    }
}