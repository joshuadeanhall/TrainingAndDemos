using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Statuos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Infrastructure.Installers
{
    //Might have been better to make a facility for the context, UoW, and repositories
    public class StatuosContextInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IStatuosContext>()
                                .ImplementedBy<StatuosContext>()
                                .LifestylePerWebRequest()
                                );
        }
    }
}