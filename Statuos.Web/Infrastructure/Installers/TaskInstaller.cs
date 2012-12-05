using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statuos.Domain;
using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.Installers
{
    //This installer is primarly used for creating new tasks.
    public class TaskInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<Task>()
                                .BasedOn<Task>()
                                .WithService.Base()
                                .LifestyleTransient()
                                );
            container.Register(Classes.FromAssemblyContaining<TaskViewModel>()
                                .BasedOn<TaskViewModel>()
                                .WithService.Base()
                                .LifestyleTransient()
                                );
        }
    }
}