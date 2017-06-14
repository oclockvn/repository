using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// save all changes in db context and return total effected records.
        /// 
        /// Using Commit if you dont want any exception thow, if your framework already willing to handle exception, use SaveChanges below
        /// </summary>
        /// <returns>If success, return a tuple with effected record. Otherwise return tuple with exception</returns>
        Tuple<int, Exception> Commit();
        Task<Tuple<int, Exception>> CommitAsync();

        /// <summary>
        /// Save any change in database context. Throw exception if any.
        /// </summary>
        /// <returns>Total effected records</returns>
        int SaveChanges();

        /// <summary>
        /// Save any change in database context, with async task
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
        /// Execute command query
        /// </summary>
        /// <param name="query">a string of query command</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>Total effected records</returns>
        int ExecuteSql(string query, params object[] args);
        Task<int> ExecuteSqlAsync(string query, params object[] args);

        /// <summary>
        /// Execute select query
        /// </summary>
        /// <typeparam name="T">typeof return result</typeparam>
        /// <param name="query">a string of select query</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>The collection of query</returns>
        List<T> SqlQuery<T>(string query, params object[] args);
        Task<List<T>> SqlQueryAsync<T>(string query, params object[] args);
    }
}
