using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        T Get(object id);
        T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);
        T Delete(object id);
        void Delete(Expression<Func<T, bool>> where);
        T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null, List<Expression<Func<T, object>>> excludeProperties = null);

        //
        // async version
        //
        Task<T> GetAsync(object id);
        Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);
    }
}
