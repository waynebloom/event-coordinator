using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using present.Models;

namespace present.Views {
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage() {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Attending, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id) {
            if (!MenuPages.ContainsKey(id)) {
                switch (id) {
                    case (int)MenuItemType.Attending:
                        MenuPages.Add(id, new NavigationPage(new AttendingEventsPage()));
                        break;
                    case (int)MenuItemType.Supervising:
                        MenuPages.Add(id, new NavigationPage(new SupervisingEventsPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage) {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}