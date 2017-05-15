using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
