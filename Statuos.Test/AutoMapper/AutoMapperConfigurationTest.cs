using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statuos.Web.Infrastructure.AutoMapper;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Statuos.Domain;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Infrastructure.Installers;

namespace Statuos.Test.AutoMapper
{
    [TestClass]
    public class AutoMapperConfigurationTest
    {
        public AutoMapperConfigurationTest()
        {
            IWindsorContainer container = new WindsorContainer()
                .Install(new AutoMapperProfilesInstaller());
            AutoMapperConfiguration.Configure(container);
        }
        [TestMethod]
        public void CanMapCustomerToCustomerViewModel()
        {
            var customer = new Customer() { Id = 1, Code = "COD", Name = "Customer Code" };
            try
            {
                var customerVM = customer.MapTo<CustomerViewModel>();
            }
            catch
            {
                Assert.Fail("Exception occured mapping customer to customer view model");
            }
        }
    }
}
