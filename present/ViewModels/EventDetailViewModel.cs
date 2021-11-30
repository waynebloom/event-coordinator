using System;
using Xamarin.Forms;

using present.Services;
using present.Views;
using present.Models;

namespace present.ViewModels {
    public class EventDetailViewModel : BaseViewModel {
        public Event Item { get; set; }

        public EventDetailViewModel(Event item = null) {
            Title = item?.Title;
            Item = item;

            // callbacks
            MessagingCenter.Subscribe(this, "RetireEvent", (Action<SupervisingEventDetailPage, int>)(async (obj, itemId) => {
                await DbService.DeleteEventAsync(itemId);
            }));

            MessagingCenter.Subscribe(this, "SaveEvent", (Action<SupervisingEventDetailPage, Event>)(async (obj, mItem) => {
                await DbService.SaveItemAsync(mItem);
            }));
        }
    }
}
