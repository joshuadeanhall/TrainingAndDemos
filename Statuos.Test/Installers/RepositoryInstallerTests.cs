using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using Statuos.Web.Infrastructure.Installers;
using Statuos.Domain;
using Statuos.Data;

namespace Statuos.Test.Installers
{
    [TestClass]
    public class RepositoryInstallerTests
    {
        [TestMethod]
        public void CanResolveRepository()
        {
            //IWindsorContainer container = new WindsorContainer()
            //   .Install(new RepositoryInstaller(), new StatuosContextInstaller(), new UnitOfWorkInstaller());

            //container.Resolve<IRepository<Customer>>();

            //container.Resolve<AutoMapperProfileInstallerTests>();

        }
    }
}
