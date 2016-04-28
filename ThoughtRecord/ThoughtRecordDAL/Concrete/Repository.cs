using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;

namespace ThoughtRecordApp.DAL.Concrete
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entityList;
            using (var conn = ConnectionManager.GetConnection())
            {
                entityList = conn.GetAllWithChildren<T>();
            }
            return entityList;
        }
        T IRepository<T>.GetById(int id)
        {
            T entity;
            using (var conn = ConnectionManager.GetConnection())
            {
                entity = conn.GetWithChildren<T>(id);
            }
            return entity;
        }
        public void Delete(int id)
        {
            using (var conn = ConnectionManager.GetConnection())
            {
                conn.Delete<T>(id);
            }
        }

        public void Insert(T entity)
        {
            using (var conn = ConnectionManager.GetConnection())
            {
                conn.InsertWithChildren(entity);
            }
        }

        public void Update(T entity)
        {
            using (var conn = ConnectionManager.GetConnection())
            {
                conn.UpdateWithChildren(entity);
            }
        }
    }
}
