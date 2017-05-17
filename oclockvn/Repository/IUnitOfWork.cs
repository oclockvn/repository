using System;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// save all changes in db context
        /// </summary>
        /// <returns>If success, return a tuple with effected record. Otherwise return tuple with exception</returns>
        Tuple<int, Exception> Commit();

        /// <summary>
        /// get repository with specific entity
        /// </summary>
        /// <typeparam name="TKey">typeof primary key of entity</typeparam>
        /// <typeparam name="T">typeof entity</typeparam>
        /// <returns>A repository of type T</returns>
        IRepository<TKey, T> Get<TKey, T>() where T : class;
    }
}
