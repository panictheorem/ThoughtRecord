using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordDAL.Concrete
{
    public static class ConnectionManager
    {
        private static string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformWinRT(), path);
        }
    }
}
