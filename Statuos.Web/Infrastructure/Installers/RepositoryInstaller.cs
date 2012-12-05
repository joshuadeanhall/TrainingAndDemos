using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statuos.Domain;
using Statuos.Data;
using Statuos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<>))
                                .ImplementedBy(typeof(Repository<>))
                                .LifestylePerWebRequest()                                                
                                );            
        }
    }
}