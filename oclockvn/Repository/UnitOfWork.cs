using System;

namespace oclockvn.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public Tuple<int, Exception> Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
