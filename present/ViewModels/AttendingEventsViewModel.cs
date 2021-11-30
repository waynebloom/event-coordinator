using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using present.Models;
using present.Views;
using present.Services;

namespace present.ViewModels {

    // I've used MVVM architecture for this project
    // this is a ViewModel that accompanies AttendingEventsPage and some subpages
    // it is responsible for all complex operations pertaining to the data
    public class AttendingEventsViewModel : BaseViewModel {
        public ObservableCollection<Event> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public AttendingEventsViewModel() {
            Title = "Attending Events";
            Items = new ObservableCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<JoinEventPage, string>(this, "JoinEvent", async (obj, code) => {
                Event item = await DbService.GetEventByCodeAsync(code);
                if(item != null) {
                    if(!item.Roster.Contains(App.CurrentUser.UserId)) {
                        item.Roster += string.Format("{0},", App.CurrentUser.UserId);
                        item.UpdateSize();
                        await DbService.SaveItemAsync(item);

                        obj.JoinSuccess(item.Title);

                        if (!IsBusy) {
                            await ExecuteLoadItemsCommand();
                        }
                    }
                    else {
                        obj.JoinFail("You are already on the roster for this event.");
                    }
                }
                else {
                    obj.JoinFail("An event with the entered access code does not exist.");
                }
            });

            MessagingCenter.Subscribe<AttendingEventDetailPage, Event>(this, "LeaveEvent", async (obj, item) => {
                item.Roster = item.Roster.Replace($"{App.CurrentUser.UserId},", "");
                item.UpdateSize();
                await DbService.SaveItemAsync(item);

                if(!IsBusy) {
                    await ExecuteLoadItemsCommand();
                }
            });
        }

        // this Command infrastructure was generated at project creation and I kept
        // it because it is very useful
        async Task ExecuteLoadItemsCommand() {
            IsBusy = true;

            try {
                Items.Clear();
                var items = await DbService.GetAttendingEvents(App.CurrentUser.UserId);
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