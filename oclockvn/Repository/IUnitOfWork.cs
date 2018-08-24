using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save any change in database context. Throw exception if any.
        /// </summary>
        /// <returns>Total effected records</returns>
        int SaveChanges();

        /// <summary>
        /// Save any change in database context, with async task
        /// Usage: int saved = await SaveChangesAsync();
        /// </summary>
        /// <returns>Total effected records</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// get repository for specific entity
        /// </summary>
        /// <typeparam name="T">typeof entity</typeparam>
        /// <returns>A repository of type T</returns>
        IRepository<T> Get<T>() where T : class;

        /// <summary>
        /// Execute command queries like insert, update, delete
        /// </summary>
        /// <param name="query">a string of query command</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>Total effected records</returns>
        int ExecuteSql(string query, params object[] args);

        /// <summary>
        /// Execute command queries like insert, update, delete asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string query, params object[] args);

        /// <summary>
        /// Execute queries sort of select, store proc or function
        /// </summary>
        /// <typeparam name="T">typeof return result</typeparam>
        /// <param name="query">a string of select query</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>The collection of query</returns>
        List<T> SqlQuery<T>(string query, params object[] args);

        /// <summary>
        /// Execute queries sort of select, store proc or function asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        Task<List<T>> SqlQueryAsync<T>(string query, params object[] args);
    }
}
