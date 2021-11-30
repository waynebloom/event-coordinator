using System;
using System.Collections.Generic;
using SQLite;

namespace present.Models {
    public class Event {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string AccessCode { get; set; }
        public string CheckInCode { get; set; }
        public string SupervisorId { get; set; }
        public string Day { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        //[Ignore]
        //public List<AttendanceSheet> PastAttendanceSheets { get; set; }
        //[Ignore]
        //public List<User> Roster { get; set; }
        public string Roster { get; set; }
        public int Size { get; set; }
        public string StartTime { get; set; }
        public string Title { get; set; }

        public Event() {
            AccessCode = "";
            CheckInCode = "";
            EndTime = "";
            Roster = "";
            Location = "";
            Title = "";
            Size = 0;
            StartTime = "";
            SupervisorId = "";
        }

        override public string ToString() {
            return string.Format("Id:{0}, AccessCode:{1}, CheckInCode:{2}, SupervisorId:{3}, Day:{4}\n" +
                "EndTime:{5}, ExpectedStudents:{6}, Location:{7},\n" +
                "Size:{8}, StartTime:{9}, Title:{10}", Id, AccessCode, CheckInCode, SupervisorId,
                Day, EndTime, Roster, Location, Size, StartTime,
                Title);
        }

        public void UpdateSize() {
            string[] roster = Roster.Split(',');
            Console.WriteLine($"ROSTER: {roster.Length}");
            Console.WriteLine($"ROSTER: {roster[0]}");
            Size = roster.Length - 1;
        }
    }
}