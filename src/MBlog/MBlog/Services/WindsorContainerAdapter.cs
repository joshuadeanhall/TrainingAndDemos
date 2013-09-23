using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using ServiceStack.Configuration;

namespace MBlog.Services
{
    public class WindsorContainerAdapter : IContainerAdapter
    {
        private readonly IWindsorContainer _container;

        public WindsorContainerAdapter(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public T Resolve<T>()
        {
           return _container.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            return _container.Kernel.HasComponent(typeof (T)) ? _container.Resolve<T>() : default(T);

        }
    }
}