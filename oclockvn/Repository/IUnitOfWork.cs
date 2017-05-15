using System;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Tuple<int, Exception> Commit();
        IRepository<TKey, T> Get<TKey, T>() where T : class;
    }
}
