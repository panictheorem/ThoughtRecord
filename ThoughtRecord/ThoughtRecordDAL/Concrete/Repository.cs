
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
            var conn = ConnectionManager.GetAsyncConnection();
            var entities = await conn.GetAllWithChildrenAsync<T>();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var conn = ConnectionManager.GetAsyncConnection();
            var entity = await conn.GetWithChildrenAsync<T>(id);
            return entity;
        }

        public async Task InsertAsync(T entity)
        {
            var conn = ConnectionManager.GetAsyncConnection();
            await conn.InsertWithChildrenAsync(entity);
        }

        public async Task InsertOrUpdateAsync(T entity)
        {
            var conn = ConnectionManager.GetAsyncConnection();
            await conn.InsertOrReplaceWithChildrenAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var conn = ConnectionManager.GetAsyncConnection();
            await conn.UpdateWithChildrenAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var conn = ConnectionManager.GetAsyncConnection();
            await conn.DeleteAsync<T>(id);
        }
    }
}
