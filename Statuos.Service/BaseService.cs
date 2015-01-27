using Statuos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected IRepository<T> _repository;
        protected IUnitOfWork _uow;
        public BaseService(IRepository<T> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            _uow.Save();
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _uow.Save();
        }

        public void Edit(T entity)
        {
            _repository.Edit(entity);
            _uow.Save();
        }
    }
}
