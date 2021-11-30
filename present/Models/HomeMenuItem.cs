using System;
using System.Collections.Generic;
using System.Text;

namespace present.Models {
    public enum MenuItemType {
        Attending,
        Supervising
    }
    public class HomeMenuItem {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
