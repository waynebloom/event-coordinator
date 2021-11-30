using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using present.Models;
using present.Views;
using present.Services;
using System.Collections.Generic;

namespace present.ViewModels {
    public class SupervisingEventsViewModel : BaseViewModel {
        public ObservableCollection<Event> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SupervisingEventsViewModel() {
            Title = "Supervising Events";
            Items = new ObservableCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            // callback from creating a new event
            MessagingCenter.Subscribe<CreateEventPage, Event>(this, "CreateEvent", (obj, item) => {
                item.SupervisorId = App.CurrentUser.UserId;
                SaveNewEvent(item);
            });
        }

        private async void SaveNewEvent(Event item) {

            // access code handled by global event db
            item.AccessCode = string.Format("{0:000000}", await DbService.GetNextAccessCode());

            Console.WriteLine(item);

            // set item.SupervisorId to current logged in user's id
            // TODO

            // add to db
            await DbService.SaveItemAsync(item);
        }

        async Task ExecuteLoadItemsCommand() {
            IsBusy = true;

            try {
                Items.Clear();
                var items = await DbService.GetSupervisingEvents(App.CurrentUser.UserId);
                foreach (var item in items) {
                    Items.Add(item);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            finally {
                IsBusy = false;
            }
        }
    }
}
