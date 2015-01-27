using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Data
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> All { get; }
        T Find(int id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}
