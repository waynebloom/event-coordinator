using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using present.Models;

namespace present.Services {

    // this class wraps all calls to the db and includes operations needed for specific cases in the app
    public class DbService {
        static SQLiteAsyncConnection Database;

        public static async Task Init() {
            if (Database != null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DbPath, Constants.DbFlags);
            await Database.CreateTableAsync<Event>();
            await Database.CreateTableAsync<User>();
        }

        // methods for a specific purpose
        public static async Task<int> GetNextAccessCode() {
            int NextAccessCode = 0;

            await Init();

            List<Event> events = (List<Event>) await GetEventsAsync();
            if (events.Count > 0) {
                int.TryParse(events[events.Count - 1].AccessCode, out NextAccessCode);
                NextAccessCode++;
            }
            return NextAccessCode;
        }

        public static async Task<int> GetNextUserId() {
            int NextUserId = 0;

            await Init();

            List<User> users = (List<User>)await GetUsersAsync();
            if (users.Count > 0) {
                int.TryParse(users[users.Count - 1].UserId, out NextUserId);
                NextUserId++;
            }
            return NextUserId;
        }

        public static async Task<User> FindLogin(string email) {
            await Init();
            return await Database.Table<User>().Where(i => i.Email == email).FirstOrDefaultAsync();
        }

        public static async Task<IEnumerable<Event>> GetSupervisingEvents(string supervisorId) {
            return await Database.Table<Event>().Where(i => i.SupervisorId == supervisorId).ToListAsync();
        }

        public static async Task<IEnumerable<Event>> GetAttendingEvents(string userId) {
            return await Database.Table<Event>().Where(i => i.Roster.Contains(userId)).ToListAsync();
        }

        public static async Task<Event> GetEventByCodeAsync(string code) {
            await Init();
            return await Database.Table<Event>().Where(i => i.AccessCode == code).FirstOrDefaultAsync();
        }

        // methods for general actions
        // get all items of a type
        public static async Task<IEnumerable<Event>> GetEventsAsync() {
            await Init();
            return await Database.Table<Event>().ToListAsync();

        }

        public static async Task<IEnumerable<User>> GetUsersAsync() {
            await Init();
            return await Database.Table<User>().ToListAsync();

        }

        // get a single item of a type
        public static async Task<Event> GetEventAsync(int id) {
            await Init();
            return await Database.Table<Event>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<User> GetUserAsync(int id) {
            await Init();
            return await Database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        // save an item
        public static async Task SaveItemAsync(Event item) {
            await Init();
            if (item.Id != 0) {
                await Database.UpdateAsync(item);
            }
            else {
                await Database.InsertAsync(item);
            }
        }

        public static async Task SaveItemAsync(User item) {
            await Init();
            if (item.Id != 0) {
                await Database.UpdateAsync(item);
            }
            else {
                await Database.InsertAsync(item);
            }
        }

        // delete an item
        public static async Task DeleteEventAsync(int id) {
            await Init();
            await Database.DeleteAsync<Event>(id);
        }

        public static async Task DeleteUserAsync(int id) {
            await Init();
            await Database.DeleteAsync<User>(id);
        }
    }
}
