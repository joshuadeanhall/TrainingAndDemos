using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IStatuosContext _context;

        public UnitOfWork(IStatuosContext context)
        {
            _context = context;
        }

        public IStatuosContext Context
        {
            get { return _context; }
        }

        public int Save()
        {
            return _context.Save();
        }

        public void Dispose()
        {
           // _context.Dispose();
        }
    }
}
