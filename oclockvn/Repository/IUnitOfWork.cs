using System;

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
    }
}
