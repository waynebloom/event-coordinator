using System;
using System.IO;

namespace present {
    public static class Constants {
        public const string DbFilename = "db.db3";
        public static string DbPath {
            get {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DbFilename);
            }
        }

        public const SQLite.SQLiteOpenFlags DbFlags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;
    }
}
