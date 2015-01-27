using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Statuos.Service;
using Statuos.Web.Infrastructure.Interceptors.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {

            container.Register(Component.For<IInterceptor>()
                .ImplementedBy<ServiceLoggerInterceptor>()
                .Named("ServiceLoggingInteceptor"));

            container.Register(Classes.FromAssemblyContaining<CustomerService>()
                                .BasedOn(typeof(BaseService<>))
                                .WithService.DefaultInterfaces()
                                .LifestyleTransient()
                                .Configure(delegate(ComponentRegistration c) { var x = c.Interceptors(InterceptorReference.ForKey("ServiceLoggingInteceptor")).Anywhere; })
                                );
        }
    }
}