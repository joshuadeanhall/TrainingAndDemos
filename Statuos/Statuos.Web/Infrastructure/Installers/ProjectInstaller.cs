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
    public class ProjectInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<Project>()
                                .BasedOn<Project>()
                                .WithService.Base()
                                .LifestyleTransient()
                                );
            //This is mostly being used to get all types of projectviewmodels for creating new projects
            container.Register(Classes.FromAssemblyContaining<ProjectViewModel>()
                                .BasedOn<ProjectViewModel>()
                                .WithService.Base()
                                .LifestyleTransient()
                                );
        }
    }
}