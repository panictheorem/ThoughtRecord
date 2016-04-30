using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.DAL.Concrete
{
    public static class ConnectionManager
    {
        public static readonly string FileName = "db.sqlite";
        private static string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformWinRT(), path);
        }
        public static SQLiteAsyncConnection GetAsyncConnection()
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(path, storeDateTimeAsTicks: true)));
            return new SQLiteAsyncConnection(connectionFactory);
        }
    }
}
