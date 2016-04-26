using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Abstract;

namespace ThoughtRecordDAL.Concrete
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private static string path = string.Empty;
        private static string DbPath
        {
            get
            {
                if(path == null)
                {
                    path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
                }
                return path;
            }
        }
        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            }
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entityList;
            using (var conn = DbConnection)
            {
                entityList = conn.GetAllWithChildren<T>();
            }
            return entityList;
        }
        T IRepository<T>.GetById(int id)
        {
            T entity;
            using (var conn = DbConnection)
            {
                entity = conn.GetWithChildren<T>(id);
            }
            return entity;
        }
        public void Delete(int id)
        {
            using (var conn = DbConnection)
            {
                conn.Delete<T>(id);
            }
        }

        public void Insert(T entity)
        {
            using (var conn = DbConnection)
            {
                conn.Insert(entity);
            }
        }

        public void Update(T entity)
        {
            using (var conn = DbConnection)
            {
                conn.UpdateWithChildren(entity);
            }
        }
    }
}
