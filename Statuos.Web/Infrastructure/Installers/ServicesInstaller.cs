using Castle.MicroKernel.Registration;
using Statuos.Service;
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
            container.Register(Classes.FromAssemblyContaining<CustomerService>()
                                .BasedOn(typeof(BaseService<>))
                                .WithService.DefaultInterfaces()
                                .LifestyleTransient()
                                );
        }
    }
}