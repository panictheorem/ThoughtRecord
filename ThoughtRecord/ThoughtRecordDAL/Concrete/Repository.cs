
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Abstract;
using SQLiteNetExtensionsAsync.Extensions;
using SQLite.Net;
using System.Threading;
using SQLite.Net.Async;

namespace ThoughtRecordApp.DAL.Concrete
{
    /// <summary>
    /// Generic class for performing CRUD operations on an object
    /// </summary>
    internal class Repository<T> : IRepository<T> where T : class
    {
        private SQLiteAsyncConnection asyncConn;

        public Repository(SQLiteAsyncConnection conn)
        {
            asyncConn = conn;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entityList;
            using (var conn = ConnectionManager.GetConnection())
            {
                entityList = conn.GetAllWithChildren<T>();
            }
            return entityList;
        }

        public T GetById(int id)
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

        public void InsertOrUpdate(T entity)
        {
            using (var conn = ConnectionManager.GetConnection())
            {
                conn.InsertOrReplaceWithChildren(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await asyncConn.GetAllWithChildrenAsync<T>();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //this method throws an InvalidOperationException for some reason
            //if the id is for an item that was previously deleted
            var entity = await asyncConn.GetWithChildrenAsync<T>(id);
            return entity;
        }

        public async Task InsertAsync(T entity)
        {
            await asyncConn.InsertWithChildrenAsync(entity);
        }

        public async Task InsertOrUpdateAsync(T entity)
        {
            await asyncConn.InsertOrReplaceWithChildrenAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await asyncConn.UpdateWithChildrenAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await asyncConn.DeleteAsync<T>(id);
        }
    }
}
