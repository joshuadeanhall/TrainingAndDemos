using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IStatuosContext _context;
        public Repository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<T> All
        {
            get { return _context.Set<T>(); }
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
           // _context.Dispose();
        }
    }
}
