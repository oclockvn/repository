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
        private readonly DbContext _db;
        private readonly DbSet<T> _table;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public Repository(DbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get all queryable entities
        /// </summary>
        public IQueryable<T> All => _table.AsQueryable();

        /// <inheritdoc />
        /// <summary>
        /// Add entity to local
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>
        /// The added entity
        /// </returns>
        public T Create(T entity) => _table.Add(entity);

        /// <inheritdoc />
        /// <summary>
        /// Delete set of entities with condition
        /// </summary>
        /// <param name="where">The predicate to filter entities to delete</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        public void Delete(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var found = All.Including(includes).Where(where);
            foreach (var item in found)
            {
                _table.Remove(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete entity by primary key
        /// </summary>
        /// <param name="id">The primary key to find entity to delete</param>
        /// <returns>
        /// Found entity with given id, otherwise return default value
        /// </returns>
        public T Delete(object id)
        {
            var entity = Get(id);
            return entity == null ? null : _table.Remove(entity);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="id">The primary key of entity</param>
        /// <returns>
        /// Return entity if found, if not return null
        /// </returns>
        public T Get(object id) => _table.Find(id);

        /// <inheritdoc />
        /// <summary>
        /// Get first entity include navigation properties
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>
        /// First entity if found, otherwise return default value of entity
        /// </returns>
        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) => All.Including(includes).FirstOrDefault(where);

        /// <inheritdoc />
        /// <summary>
        /// Get all entities include navigation properties
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>
        /// The collection of entities.
        /// </returns>
        public List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes) 
            => where == null 
            ? All.AsNoTracking().Including(includes).ToList() 
            : All.AsNoTracking().Including(includes).Where(where).ToList();

        /// <inheritdoc />
        /// <summary>
        /// Get all entities include navigation properties using Task.
        /// Usage:
        /// var list = await GetAllAsync(x =&gt; x.Id == id);
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>
        /// The collection of entities.
        /// </returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes)
            => where == null 
            ? await All.AsNoTracking().Including(includes).ToListAsync() 
            : await All.AsNoTracking().Including(includes).Where(where).ToListAsync();

        /// <inheritdoc />
        /// <summary>
        /// Get entity by primary key using Task.
        /// Usage:
        /// T t = await GetAsync(id);
        /// </summary>
        /// <param name="id">The primary key of entity</param>
        /// <returns>
        /// Return entity if found, if not return null
        /// </returns>
        public async Task<T> GetAsync(object id) 
            => await _table.FindAsync(id);

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) => await All.Including(includes).FirstOrDefaultAsync(where);

        /// <inheritdoc />
        /// <summary>
        /// Attach then update entity, can specify properties to update, or update all except exclude properties
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="updateProperties">if has value, update these properties instead all properties of entity</param>
        /// <param name="excludeProperties">if has value, exclude these properties out of update process</param>
        /// <returns>
        /// The updated entity
        /// </returns>
        public T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null, List<Expression<Func<T, object>>> excludeProperties = null)
        {
            _table.Attach(entity);

            if (updateProperties == null || updateProperties.Count == 0)
            {
                _db.Entry(entity).State = EntityState.Modified;
                if (excludeProperties != null && excludeProperties.Count > 0)
                {
                    excludeProperties.ForEach(p => _db.Entry(entity).Property(p).IsModified = false);
                }
            }
            else
            {
                updateProperties.ForEach(p => _db.Entry(entity).Property(p).IsModified = true);                
            }

            return entity;
        }
    }
}
