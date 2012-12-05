using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statuos.Web.Infrastructure.Installers;
using Castle.Windsor;
using AutoMapper;
using System.Linq;

namespace Statuos.Test.Installers
{
    [TestClass]
    public class AutoMapperProfileInstallerTests
    {
        [TestMethod]
        public void CanResolveProfiles()
        {
            IWindsorContainer container = new WindsorContainer()
               .Install(new AutoMapperProfilesInstaller());

            Assert.IsTrue(container.ResolveAll<Profile>().Count() > 0);
        }
    }
}
