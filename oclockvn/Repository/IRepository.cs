using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all queryable entities
        /// </summary>
        IQueryable<T> All { get; }

        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="id">The primary key of entity</param>
        /// <returns>Return entity if found, if not return null</returns>
        T Get(object id);

        /// <summary>
        /// Get first entity include navigation properties
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>First entity if found, otherwise return default value of entity</returns>
        T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get all entities include navigation properties
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>The collection of entities.</returns>
        List<T> GetAll(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Delete entity by primary key
        /// </summary>
        /// <param name="id">The primary key to find entity to delete</param>
        /// <returns>Found entity with given id, otherwise return default value</returns>
        T Delete(object id);

        /// <summary>
        /// Delete set of entities with condition
        /// </summary>
        /// <param name="where">The predicate to filter entities to delete</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Attach then update entity, can specify properties to update, or update all except exclude properties
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="updateProperties">if has value, update these properties instead all properties of entity</param>
        /// <param name="excludeProperties">if has value, exclude these properties out of update process</param>
        /// <returns>The updated entity</returns>
        T Update(T entity, List<Expression<Func<T, object>>> updateProperties = null, List<Expression<Func<T, object>>> excludeProperties = null);

        //
        // async version
        //

        /// <summary>
        /// Get entity by primary key using Task.
        /// 
        /// Usage: 
        /// T t = await GetAsync(id);
        /// </summary>        
        /// <param name="id">The primary key of entity</param>
        /// <returns>Return entity if found, if not return null</returns>
        Task<T> GetAsync(object id);

        /// <summary>
        /// Get first entity include navigation properties using Task.
        /// 
        /// Usage: 
        /// T t = await GetAsync(x => x.Id == id);
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>First entity if found, otherwise return default value of entity</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get all entities include navigation properties using Task.
        /// 
        /// Usage: 
        /// List<T> list = await GetAllAsync(x => x.Id == id);
        /// </summary>
        /// <param name="where">The predicate to filter entities return</param>
        /// <param name="includes">Expression params to include navigation property.</param>
        /// <returns>The collection of entities.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includes);
    }
}
