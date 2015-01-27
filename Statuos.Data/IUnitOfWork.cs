using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IStatuosContext Context { get; }

        int Save();
    }
}
