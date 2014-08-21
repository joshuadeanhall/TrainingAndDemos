using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;
using NinjectExample.Services;
using Ninject.Extensions.Conventions;

namespace NinjectExample.Ninject
{
    public class CarServiceModule : NinjectModule
    {
        public override void Load()
        {
            //kernel.Bind<ICarService>().To<BmwService>().Named("BmwService");
            //kernel.Bind<ICarService>().To<FordService>().Named("FordService");
            //kernel.Bind<ICarService>().To<GmService>().Named("GmService");

            //kernel.Bind<IFordService>().To<FordService>();
            //kernel.Bind<IBmwService>().To<BmwService>();


            //kernel.Bind(x =>
            //{
            //    x.FromThisAssembly()
            //        .SelectAllClasses()
            //        .BindAllInterfaces()

            //});


            Kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses().InheritedFrom<ICarService>()
                    .BindAllInterfaces()
                    .Configure((b, c) => b.InRequestScope().Named(c.Name));
            });

            Kernel.Bind<ICarServiceFactory>().ToFactory();
        }
    }
}