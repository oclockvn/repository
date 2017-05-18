using oclockvn.Extenstions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext db;
        private readonly DbSet<T> table;

        public Repository(DbContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public IQueryable<T> All => table.AsQueryable();

        public T Create(T entity) => table.Add(entity);

        public void Delete(Expression<Func<T, bool>> where)
        {
            var found = All.Where(where);
            foreach (var item in found)
            {
                table.Remove(item);
            }
        }

        public T Delete(object id)
        {
            var entity = Get(id);
            return entity == null ? null : table.Remove(entity);
        }

        public T Get(object id) => table.Find(id);

        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) => All.Including(includes).FirstOrDefault(where);

        public List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes) 
            => where == null ? All.AsNoTracking().Including(includes).ToList() : All.AsNoTracking().Including(includes).Where(where).ToList();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
            => where == null ? await All.AsNoTracking().Including(includes).ToListAsync() : await All.AsNoTracking().Including(includes).Where(where).ToListAsync();

        public async Task<T> GetAsync(object id) => await table.FindAsync(id);

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) => await All.Including(includes).FirstOrDefaultAsync(where);

        public T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null, List<Expression<Func<T, object>>> excludeProperties = null)
        {
            table.Attach(entity);

            if (updateProperties == null || updateProperties.Count == 0)
            {
                db.Entry<T>(entity).State = EntityState.Modified;
                if (excludeProperties != null && excludeProperties.Count > 0)
                {
                    excludeProperties.ForEach(p => db.Entry<T>(entity).Property(p).IsModified = false);
                }
            }
            else
            {
                updateProperties.ForEach(p => db.Entry<T>(entity).Property(p).IsModified = true);                
            }

            return entity;
        }
    }
}
