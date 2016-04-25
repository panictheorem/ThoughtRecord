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
    public class Repository<T> : IRepository<T> where T : class
    {
        private ThoughtRecordAppDbContext databaseContext;

        public Repository(ThoughtRecordAppDbContext context)
        {
            databaseContext = context;
        }
        public IEnumerable<T> GetAll()
        {
            return databaseContext.Conn.GetAllWithChildren<T>();
        }
        T IRepository<T>.GetById(int id)
        {
            return databaseContext.Conn.GetWithChildren<T>(id);
        }
        public void Delete(int id)
        {
            databaseContext.Conn.Delete<T>(id);
        }

        public void Insert(T entity)
        {
            databaseContext.Conn.Insert(entity);
        }

        public void Update(T entity)
        {
            databaseContext.Conn.UpdateWithChildren(entity);
        }
    }
}
