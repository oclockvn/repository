using System;
using System.Collections.Generic;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// save all changes in db context and return total effected records.
        /// </summary>
        /// <returns>If success, return a tuple with effected record. Otherwise return tuple with exception</returns>
        Tuple<int, Exception> Commit();

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

        /// <summary>
        /// Execute select query
        /// </summary>
        /// <typeparam name="T">typeof return result</typeparam>
        /// <param name="query">a string of select query</param>
        /// <param name="args">a list of sql parameters</param>
        /// <returns>The collection of query</returns>
        List<T> ExecuteSql<T>(string query, params object[] args);
    }
}
