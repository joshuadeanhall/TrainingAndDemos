using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statuos.Web.Infrastructure.Interceptors.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuos.Web.Infrastructure.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient()
                                );
            //Must also register controller from elmah mvc
            container.Register(Classes.FromAssemblyContaining<Elmah.Mvc.ElmahController>()
                .BasedOn<IController>()
                .LifestyleTransient()
                );
        }
    }
}