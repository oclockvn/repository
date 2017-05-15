using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class
    {
        public IQueryable<T> All
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get(object id)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity, List<Expression<Func<T, object>>> updateProperties = null)
        {
            throw new NotImplementedException();
        }
    }
}
