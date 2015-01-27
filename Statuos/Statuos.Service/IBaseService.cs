using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Service
{
    public interface IBaseService<T> where T: class
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

    }
}
