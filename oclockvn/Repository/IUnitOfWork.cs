using System;

namespace oclockvn.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Tuple<int, Exception> Commit();
    }
}
