using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordDAL.Concrete
{
    public class ThoughtRecordAppDbContext
    {
        private string path;
        public SQLiteConnection Conn { get; }

        public ThoughtRecordAppDbContext()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            Conn = new SQLiteConnection(new SQLitePlatformWinRT(), path);
            Conn.CreateTable<ThoughtRecord>();
            Conn.CreateTable<Situation>();
            Conn.CreateTable<Emotion>();
            Conn.CreateTable<Settings>();
        }
    }
}
