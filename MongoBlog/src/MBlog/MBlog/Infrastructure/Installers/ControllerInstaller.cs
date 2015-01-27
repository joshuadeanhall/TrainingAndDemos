﻿using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MBlog.Infrastructure.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IController>()
                                      .LifestyleTransient());
            container.Register(Classes.FromAssemblyContaining<Elmah.Mvc.ElmahController>()
                .BasedOn<IController>()
                .LifestyleTransient()
                );
        }
    }
}