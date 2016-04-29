using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.DAL.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void InsertOrUpdate(T entity);
        Task InsertOrUpdateAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
