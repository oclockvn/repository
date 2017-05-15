using oclockvn.Extenstions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext db;
        private readonly DbSet<T> table;

        public Repository(DbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public IQueryable<T> All 
            => table.AsQueryable();

        public int Delete(Expression<Func<T, bool>> where)
        {
            var found = All.Where(where);
            foreach (var item in found)
            {
                table.Remove(item);
            }

            return found.Count();
        }

        public T Delete(TKey id) 
            => table.Remove(Get(id));

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        {
            var found = All.Where(where);
            foreach (var item in found)
            {
                table.Remove(item);
            }

            return await found.CountAsync();
        }

        public async Task<T> DeleteAsync(TKey id) 
            => table.Remove(await GetAsync(id));

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get(TKey id) 
            => table.Find(id);

        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) 
            => All.Including(includes).FirstOrDefault(where);

        public List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes) 
            => where == null ? All.Including(includes).ToList() : All.Including(includes).Where(where).ToList();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
            => where == null ? await All.Including(includes).ToListAsync() : await All.Including(includes).Where(where).ToListAsync();

        public async Task<T> GetAsync(TKey id) => await table.FindAsync(id);

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
            => await All.Including(includes).FirstOrDefaultAsync(where);

        public T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null)
        {
            table.Attach(entity);

            if (updateProperties == null || updateProperties.Count == 0)
                db.Entry<T>(entity).State = EntityState.Modified;
            else
                updateProperties.ForEach(p => db.Entry<T>(entity).Property(p).IsModified = true);

            return entity;
        }
    }
}
